// ----------------------------------------------------------------------------------------
// Author:                    Nguyen Van Phu
// Company:                   FTS Company
// Assembly version:          1.0.*
// Date:                      12/28/2006
// Time:                      22:47
// Project Name:              Base
// Project Filename:          Base.csproj
// Project Item Name:         ObjectBaseCollection.cs
// Project Item Filename:     ObjectBaseCollection.cs
// Project Item Kind:         Code
// Purpose:                   
// ----------------------------------------------------------------------------------------

#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace FTS.Base.Business {
    [Serializable] public class FTSCollection<T> : ICollection<T> {
        private ArrayList mInnerArray;
        private bool mIsReadOnly = true;

        public FTSCollection() {
            this.mInnerArray = new ArrayList();
        }

        public void Add(T item) {
            this.mInnerArray.Add(item);
        }

        public void Insert(int index, T item) {
            this.mInnerArray.Insert(index, item);
        }

        public bool Remove(T item) {
            bool result = false;
            for (int i = 0; i < this.mInnerArray.Count; i++) {
                T obj = (T) this.mInnerArray[i];
                if (obj.Equals(item)) {
                    this.mInnerArray.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Contains(T item) {
            return this.mInnerArray.Contains(item);
        }

        public void Clear() {
            this.mInnerArray.Clear();
        }

        public IEnumerator<T> GetEnumerator() {
            return new FTSEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new FTSEnumerator<T>(this);
        }

        public int IndexOf(T item) {
            return this.mInnerArray.IndexOf(item);
        }

        public void CopyTo(T[] array, int index) {
            this.mInnerArray.CopyTo(array, index);
        }

        public T this[int index] {
            get { return (T) this.mInnerArray[index]; }
            set { this.mInnerArray[index] = value; }
        }

        public int Count {
            get { return this.mInnerArray.Count; }
        }

        public bool IsReadOnly {
            get { return this.mIsReadOnly; }
        }
    }

    internal class FTSEnumerator<T> : IEnumerator<T> {
        private FTSCollection<T> mCollection;
        private int mIndex;
        private T mCurrent;

        public FTSEnumerator() {}

        public FTSEnumerator(FTSCollection<T> collection) {
            this.mCollection = collection;
            this.mIndex = -1;
            this.mCurrent = default(T);
        }

        public T Current {
            get { return this.mCurrent; }
        }

        object IEnumerator.Current {
            get { return this.mCurrent; }
        }

        public void Dispose() {
            this.mCollection = null;
            this.mCurrent = default(T);
            this.mIndex = -1;
        }

        public bool MoveNext() {
            if (++this.mIndex >= this.mCollection.Count) {
                return false;
            } else {
                this.mCurrent = this.mCollection[this.mIndex];
            }
            return true;
        }

        public void Reset() {
            this.mCurrent = default(T);
            this.mIndex = -1;
        }
    }
}