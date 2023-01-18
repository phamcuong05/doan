#region LICENSE

/*
 * ZipBuilder - A zip package creation utility for collections of large files.
 *
 * Copyright (c) 2009, Christopher Hahn <chahn.chris@gmail.com>
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *  * Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 *  * Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *  * Neither the name of the <organization> nor the
 *    names of its contributors may be used to endorse or promote products
 *    derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY <copyright holder> ''AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL <copyright holder> BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 */

#endregion

using System;
using System.IO;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace FTS.Base.Utilities {
    public class ZipBuilder {
        public int CompressionLevel { get; set; }
        public int BlockSize { get; set; } // in bytes

        //
        // these handlers can be optionally specified to receive notification
        // when various things are happening inside the packer.  yes, these could
        // be events, but full event dispatching doesn't buy much here, and it requires
        // extra declaration overhead.  If you really need multiple observers for this,
        // feel free to convert these callbacks into real events :)
        //
        public Action<string> FileStarted { get; set; }
        public Action<string> FileFinished { get; set; }
        public Action<long, long> CrcProgress { get; set; }
        public Action<long, long> FileProgress { get; set; }

        public const int DefaultCompressionLevel = 9;
        public const int DefaultBlockSize = 1048576*64; // default block size is 64MB

        public ZipBuilder() {
            this.CompressionLevel = ZipBuilder.DefaultCompressionLevel;
            this.BlockSize = ZipBuilder.DefaultBlockSize;
        }

        public ZipBuilder(int compressionLevel) {
            this.CompressionLevel = compressionLevel;
            this.BlockSize = ZipBuilder.DefaultBlockSize;
        }

        public ZipBuilder(int compressionLevel, int blockSize) {
            this.CompressionLevel = compressionLevel;
            this.BlockSize = blockSize;
        }

        public void CreatePackage(string outputFile, string inputSpec) {
            if (String.IsNullOrEmpty(outputFile)) {
                throw new ArgumentException("outputFile cannot be empty");
            }

            if (String.IsNullOrEmpty(inputSpec)) {
                throw new ArgumentException("inputSpec cannot be empty");
            }

            //
            // attempt to extract the working folder from the input path
            //
            string packageFolder = Path.GetDirectoryName(inputSpec);

            //
            // if we can't, then we assume the current working directory
            //
            if (String.IsNullOrEmpty(packageFolder)) {
                packageFolder = Directory.GetCurrentDirectory();
            }

            string fileSpec = Path.GetFileName(inputSpec);

            //
            // finally, call the more raw CreatePackage with the working folder and
            // just the file spec portion of the input spec
            //
            this.CreatePackage(outputFile, packageFolder, fileSpec);
        }

        /// <summary>
        ///   Creates a new zip package using the contents of all of the files in the
        ///   input folder that match the fileSpec.
        ///
        ///   This allows you to perform zip operations like "zip c:\\windows\\inf\\*.inf"
        ///   The outputFile should contain the full path to the file to create.  The
        ///   packageFolder argument only applies to what is being packed.
        /// </summary>
        public void CreatePackage(string outputFile, string packageFolder, string fileSpec) {
            //
            // get all of the files in the input foler that match the input spec
            // e.g. (packageFolder = 'c:\example', fileSpec = '*.txt') 
            //
            string[] files = Directory.GetFiles(packageFolder, fileSpec);

            ZipOutputStream s = new ZipOutputStream(File.Create(outputFile));
            s.SetLevel(this.CompressionLevel);

            for (int i = 0; i < files.Length; i++) {
                //
                // determine the name of just the file
                //
                string file = Path.GetFileName(files[i]);
                string filePath = Path.Combine(packageFolder, file);

                //
                // dispatch the file started event
                //
                if (this.FileStarted != null) {
                    this.FileStarted(filePath);
                }

                //
                // calculate the CRC for the file we're about to compress, this requires
                // reading through the whole file once.
                //
                var crc = this.CalculateFileCrc(filePath);

                //
                // open the file to begin the compression operation
                //
                FileStream fs = File.OpenRead(filePath);

                //
                // generate a ZIP entry for the file that we can store into the final
                // archive
                //
                var entry = this.CreateZipEntry(file, fs.Length, i + 1, crc);

                //
                // add the zip entry to the file and copy the contents of the
                // memory buffer into the file
                //
                s.PutNextEntry(entry);

                //
                // copy the bytes from the file into the compression stream
                //
                this.CopyStream(fs, s);

                //
                // be sure to close the file once we're done with it
                //
                fs.Close();

                //
                // dispatch the file finished event
                //
                if (this.FileFinished != null) {
                    this.FileFinished(filePath);
                }
            }

            //
            // finally, close the zip output stream
            //
            s.Finish();
            s.Close();
        }

        private ZipEntry CreateZipEntry(string file, long size, int index, long crc) {
            var entry = new ZipEntry(file) {DateTime = DateTime.Now, Comment = String.Empty, Size = size, ZipFileIndex = index, Crc = crc};

            return entry;
        }

        private long CalculateFileCrc(string filePath) {
            FileStream fs = File.OpenRead(filePath);

            var crc = new Crc32Accumulator();

            crc.BeginAccumulation();

            long bytesProcessed = 0;
            this.ChunkStream(fs, (byte[] b, int p, int len) => {
                crc.Accumulate(b, p, len);

                //
                // dispatch the block progress event if we have one
                //
                if (this.CrcProgress != null) {
                    bytesProcessed += len;
                    this.CrcProgress(bytesProcessed, fs.Length);
                }
            });

            crc.EndAccumulation();

            fs.Close();

            return crc.Value;
        }

        private void ChunkStream(Stream s, Action<byte[], int, int> chunk) {
            //
            // since this is a whole stream operation, if the stream supports seeking
            // we rewind to the beginning before starting the operation.
            //
            if (s.CanSeek) {
                s.Seek(0, SeekOrigin.Begin);
            }

            //
            // figure out how many bytes to read / write at a time
            //
            int blockSize = this.GetBlockSize(s.Length);

            //
            // assume the bytes read equals the blockSize so we drop into the loop
            //
            int read = blockSize;
            while ((read == blockSize) && (read > 0)) {
                //
                // allocate the block
                //
                byte[] nextData = new byte[blockSize];

                //
                // read the bytes from the source stream
                //
                read = s.Read(nextData, 0, blockSize);

                //
                // call the input chunk operation on the block, but only if we actually
                // have any new data.
                //
                if (read > 0) {
                    chunk(nextData, 0, read);
                }
            }
        }

        private void CopyStream(Stream src, Stream dest) {
            //
            // make sure we're at the start of the destination stream if this stream
            // supports seeking
            //
            if (dest.CanSeek) {
                dest.Seek(0, SeekOrigin.Begin);
            }

            long bytesProcessed = 0;

            this.ChunkStream(src, (byte[] b, int p, int len) => {
                dest.Write(b, p, len);

                //
                // dispatch the block progress event if we have one
                //
                if (this.FileProgress != null) {
                    bytesProcessed += len;
                    this.FileProgress(bytesProcessed, src.Length);
                }
            });
        }

        private int GetBlockSize(long streamSize) {
            //
            // if the size of the entire stream is less than the block size, then we can
            // chunk the whole stream in one shot, so we report that the block size to use
            // is the stream size.
            //
            return streamSize < this.BlockSize ? (int) streamSize : this.BlockSize;
        }
    }
}