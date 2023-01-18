import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { ResourceManager } from 'src/app/common/resource-manager';
import { PrintFile } from 'src/app/model/other/PrintFile';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';
import { FtsFileSaveService } from '../fts-file-save/fts-file-save.service';
import { FtsWindowComponent } from '../fts-window/fts-window.component';

@Component({
  selector: 'fts-preview',
  templateUrl: './fts-preview.component.html',
  styleUrls: ['./fts-preview.component.scss'],
})
/**
 * Component Preview.
 * Created by: MTLUC - 09/12/2021
 */
export class FtsPreviewComponent implements OnInit {
  ftsWindow!: FtsWindowComponent;

  @ViewChild('window', { static: false }) set ftsWindowOnHtml(
    ftsWindow: FtsWindowComponent
  ) {
    if (!!ftsWindow) {
      this.ftsWindow = ftsWindow;
    }
  }
  // @ViewChild('window', { static: true }) ftsWindow!: FtsWindowComponent;

  /**
   * trạng thái show window preview.
   * Created by: MTLUC - 09/12/2021
   */
  @Input() show: boolean = false;

  /**
   * Thông tin path file in (pdf,excel,word).
   * Created by: MTLUC - 09/12/2021
   */
  @Input() printFile!: PrintFile;

  /**
   * Trạng thái loading.
   * Created by: MTLUC - 09/12/2021
   */
  isLoading: boolean = false;

  /**
   * Id window.
   * Created by: MTLUC - 09/12/2021
   */
  id = commonFunction.newGuid();

  /**
   * Đường dẫn file pdf view.
   * Không dùng trực tiếp path trong printFile vì cần chuyển sang dạng base64 để tránh lỗi cross origin browser.
   * Created by: MTLUC - 09/12/2021
   */
  fileUrl: string = '';

  @Input() defaultMaximum: boolean = true;

  /**
   * Lấy các nút Export.
   * Nếu có path trong printFile thì mới hiển thị nút đó.
   * Created by: MTLUC - 09/12/2021
   */
  get splitbuttonExport() {
    if (!this.printFile) {
      return [];
    }
    return this._splitbuttonExport.filter((x) => (this.printFile as any)[x.id]);
  }

  /**
   * Thiết lập các nút export.
   * Kendo chưa hỗ trợ khai báo trong html nên phải khai data ở đây.
   * Created by: MTLUC - 09/12/2021
   */
  private _splitbuttonExport: Array<any> = [
    {
      id: 'PDF_FILE',
      text: 'Pdf',
      iconClass: 'k-icon k-i-file-pdf',
      click: this.exportPdf.bind(this),
    },
    {
      id: 'EXCEL_FILE',
      text: 'Excel',
      iconClass: 'k-icon k-i-file-excel',
      click: this.exportExcel.bind(this),
    },
    {
      id: 'DOC_FILE',
      text: 'Word',
      iconClass: 'k-icon k-i-file-word',
      click: this.exportWord.bind(this),
    },
  ];

  /**
   * ctor
   * @param resourceManager obj quản lý resource
   * @param _eventManager  obj quản lý window event
   * @param _ftsDialog   dialog thông báo
   * @param _fileSaveService  service xử lý save file
   * Created by: MTLUC - 09/12/2021
   */
  constructor(
    public resourceManager: ResourceManager,
    private _eventManager: EventManager,
    private _ftsDialog: FtsDialogService,
    private _fileSaveService: FtsFileSaveService,
    private _ftsMain: FTSMain
  ) {}

  ngOnInit(): void {}

  /**
   * Export file pdf.
   * Created by: MTLUC - 09/12/2021
   */
  exportPdf() {
    if (this.printFile.PDF_FILE) {
      this._fileSaveService.saveAsByUrl(this.printFile.PDF_FILE);
    }
  }

  /**
   * Export file excel.
   * Created by: MTLUC - 09/12/2021
   */
  exportExcel() {
    if (this.printFile.EXCEL_FILE) {
      this._fileSaveService.saveAsByUrl(this.printFile.EXCEL_FILE);
    }
  }

  /**
   * Export file Word.
   * Created by: MTLUC - 09/12/2021
   */
  exportWord() {
    if (this.printFile.DOC_FILE) {
      this._fileSaveService.saveAsByUrl(this.printFile.DOC_FILE);
    }
  }

  /**
   * Đăng ký event keydown trên window.
   * Created by: MTLUC - 09/12/2021
   */
  handleKeyDown() {
    const that = this;
    that._eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      const strKey = e.key.toUpperCase();
      let stopEvt = false;

      //esc
      if (strKey == 'ESCAPE') {
        this.close();
        stopEvt = true;
      }

      //
      if (stopEvt) {
        e.preventDefault();
      }
    });
  }

  /**
   * Xử lý ẩn hiện trạng thái loading.
   * @param show trạng thái loading (true: hiện, false: ẩn).
   * Created by: MTLUC - 09/12/2021
   */
  mask(show: boolean) {
    this.isLoading = show;
  }

  /**
   * Show window preview.
   * Created by: MTLUC - 09/12/2021
   */
  open() {
    let file =
      this.printFile.PDF_FILE ||
      this.printFile.DOC_FILE ||
      this.printFile.EXCEL_FILE ||
      this.printFile.ORTHER_FILE;
    this.show = true;
    setTimeout(() => {
      if (this.defaultMaximum) {
        this.ftsWindow?.btnMaximizeRef?.nativeElement?.click();
      }
      this.handleKeyDown();
      this.mask(true);
      this._fileSaveService
        .getBase64File(file || '')
        .then((base64Data) => {
          this.fileUrl = base64Data;
        })
        .catch((err) => {
          this._ftsDialog.alert.show({
            icon: 'warning',
            maxWidth: 300,
            content: this._ftsMain.ExceptionManager.processException(err),
          });
        })
        .finally(() => {
          this.mask(false);
        });
    }, 100);
  }

  /**
   * Đóng window preview.
   * Created by: MTLUC - 09/12/2021
   */
  close() {
    this.show = false;
    this._eventManager.UnSubcriberKeyDown(this.id);
  }
}
