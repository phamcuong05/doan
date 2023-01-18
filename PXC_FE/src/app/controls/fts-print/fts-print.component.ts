import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { ResourceManager } from 'src/app/common/resource-manager';
import { ActionType } from 'src/app/common/types';
import { BaseService } from 'src/app/model/base/BaseService';
import { PrintFile } from 'src/app/model/other/PrintFile';
import { PrintTemplate } from 'src/app/model/other/PrintTemplates';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';
import { FtsFileSaveService } from '../fts-file-save/fts-file-save.service';
import { FtsPreviewComponent } from '../fts-preview/fts-preview.component';

@Component({
  selector: 'fts-print',
  templateUrl: './fts-print.component.html',
  styleUrls: ['./fts-print.component.scss'],
})
/**
 * Component In.
 */
export class FtsPrintComponent implements OnInit {
  /**
   * Biến lưu trạng thái component.
   */
  $vm: {
    isShow: boolean;
    isLoading: boolean;
  } = {
    isShow: false,
    isLoading: false,
  };

  /**
   * Danh sách mẫu in.
   */
  printTemplates: PrintTemplate[] = [];

  /**
   * Thông tin path file in.
   */
  printFile!: PrintFile | undefined;

  /**
   * Template đang được chọn.
   */
  templateSelected!: PrintTemplate;

  /**
   * id component.
   */
  id = commonFunction.newGuid();

  /**
   * Service.
   */
  @Input() service!: BaseService<any>;

  /**
   * tranId chứng từ.
   */
  @Input() tranId!: string;

  /**
   * key chứng từ.
   */
  @Input() prKey!: string;

  /**
   * Preview component ref.
   */
  @ViewChild(FtsPreviewComponent) ftsPreview!: FtsPreviewComponent;

  /**
   * ctor
   * @param resourceManager obj quản lý resource
   * @param _eventManager obj quản lý event window
   * @param _ftsDialog service dialog
   * @param _fileSaveService service lưu file
   */
  constructor(
    public resourceManager: ResourceManager,
    private _eventManager: EventManager,
    private _ftsDialog: FtsDialogService,
    private _fileSaveService: FtsFileSaveService,
    private _ftsMain: FTSMain
  ) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this._eventManager.UnSubcriberKeyDown(this.id);
  }

  /**
   * Xử lý ẩn hiện trạng thái loading.
   * @param show true - hiện, false - ẩn
   */
  mask(show: boolean) {
    this.$vm.isLoading = show;
  }

  /**
   * Load mẫu in.
   */
  loadTemplate() {
    /**
     * Kiểm tra không có mẫu in mới load.
     */
    if (!this.printTemplates?.length) {
      this.mask(true);
      this.service
        .getPrintTemplates(this.tranId)
        .then((data) => {
          this.printTemplates = data;
          if (data?.length) {
            this.templateSelected = data[0];
          }
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
    }
  }

  /**
   * Show window.
   * @returns
   */
  open() {
    if (!this.service) {
      this._ftsDialog.alert.show({
        icon: 'warning',
        content: 'Bạn chưa truyền service vào component!',
      });
      return;
    }

    if (!this.tranId) {
      this._ftsDialog.alert.show({
        icon: 'warning',
        content: 'Bạn chưa truyền tranId vào component!',
      });
      return;
    }

    if (!this.prKey) {
      this._ftsDialog.alert.show({
        icon: 'warning',
        content: 'Bạn chưa truyền prKey vào component!',
      });
      return;
    }
    this.$vm.isShow = true;
    this.handleKeyDown();
    this.loadTemplate();
    this.printFile = undefined;
  }

  /**
   * Đóng
   */
  close() {
    this.$vm.isShow = false;
    this._eventManager.UnSubcriberKeyDown(this.id);
  }

  /**
   * Sự kiện click vào mẫu in.
   * @param item
   */
  template_Click(item: PrintTemplate) {
    if (item != this.templateSelected) {
      this.printFile = undefined;
      this.templateSelected = item;
    }
  }

  /**
   * Đăng ký event key up trên window
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

      // Crtl +
      if (e.ctrlKey) {
        switch (strKey) {
          case 'P':
            if (!this.$vm.isLoading) {
              this.toolbarActionHandler('PRINT');
            }
            stopEvt = true;
            break;
        }
      }
      //
      if (stopEvt) {
        e.preventDefault();
      }
    });
  }

  /**
   * Load thông in file in.
   * @returns
   */
  loadPrintFile() {
    return new Promise<void>((resolve, rejects) => {
      if (this.printFile) {
        return resolve();
      }

      if (this.templateSelected) {
        this.mask(true);
        this.service
          .getPrint(this.templateSelected.PR_KEY, this.prKey)
          .then((printFile) => {
            this.printFile = printFile;
            return resolve();
          })
          .catch((err) => {
            this._ftsDialog.alert.show({
              icon: 'warning',
              maxWidth: 300,
              content: this._ftsMain.ExceptionManager.processException(err),
            });
            rejects(err);
          })
          .finally(() => {
            this.mask(false);
          });
      } else {
        this._ftsDialog.alert.show({
          icon: 'warning',
          maxWidth: 300,
          content:
            this.resourceManager.CommonResource.MyResource
              .MessageSelectPrintTeplate,
        });
        rejects(
          this.resourceManager.CommonResource.MyResource
            .MessageSelectPrintTeplate
        );
      }
    });
  }

  /**
   * Xử lý show preview.
   */
  preview() {
    this.loadPrintFile().then(() => {
      if (this.printFile) {
        this.ftsPreview.printFile = this.printFile;
        this.ftsPreview.open();
      }
    });
  }

  /**
   * Xử lý in.
   */
  print() {
    this.loadPrintFile().then(() => {
      if (this.printFile) {
        let file =
          this.printFile.PDF_FILE ||
          this.printFile.DOC_FILE ||
          this.printFile.EXCEL_FILE ||
          this.printFile.ORTHER_FILE;
        this.mask(true);
        this._fileSaveService
          .getBlobFile(file || '')
          .then((blobData) => {
            commonFunction.printFile(blobData);
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
      }
    });
  }

  /**
   * toolbar click.
   * @param actionType
   */
  toolbarActionHandler(actionType: ActionType) {
    switch (actionType) {
      case 'VIEW':
        this.preview();
        break;
      case 'PRINT':
        this.print();
        break;
      case 'CLOSE':
        this.close();
        break;
      default:
        this._ftsDialog.alert.show({
          icon: 'warning',
          content:
            this.resourceManager.CommonResource.MyResource
              .MessageFunctionUnderDevelop,
        });
        break;
    }
  }
}
