import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  ViewContainerRef,
} from '@angular/core';
import {
  CompositeFilterDescriptor,
  SortDescriptor,
} from '@progress/kendo-data-query';
import readXlsxFile from 'read-excel-file';
import { Map } from 'read-excel-file/types';
import { Observable } from 'rxjs';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { ResourceManager } from 'src/app/common/resource-manager';
import { ActionType } from 'src/app/common/types';
import { BaseService } from 'src/app/model/base/BaseService';
import { DmTemplate } from 'src/app/model/system/dm-template/dm-template';
import { DmTemplateDetail } from 'src/app/model/system/dm-template/dm-template-detail';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';
import { FtsFileSaveService } from '../fts-file-save/fts-file-save.service';
import { FtsColumn } from '../fts-grid/fts-grid.component';
import { MaskLoadService } from '../mask-load/mask-load.service';
import { ImportExcelTemplateComponent } from './import-excel-template/import-excel-template.component';

@Component({
  selector: 'fts-import-excel',
  templateUrl: './fts-import-excel.component.html',
  styleUrls: ['./fts-import-excel.component.scss'],
})

/**
 * Component Import Excel
 * Create by : TAN.VU
 */
export class FtsImportExcelComponent implements OnInit {
  imported: boolean = false;

  /**
   * Biến lưu trạng thái component
   */
  $vm: {
    isShow: boolean;
    isLoading: boolean;
    columns: Array<any>;
    datas: any[];
  } = {
    isShow: false,
    isLoading: false,
    columns: [],
    datas: [],
  };

  vm1$!: Observable<{
    sortable: boolean;
    filterable: boolean;
    dataBinding: Array<any>;
    columns: Array<any>;
    filter: CompositeFilterDescriptor;
    sort: Array<SortDescriptor>;
  }>;

  /**
   * Tiêu đề chức năng
   */
  title!: string;
  /**
   * Danh sách template
   */
  dmTemplate: DmTemplate[] = [];

  /**
   *  *dmTemplateDetail
   *
   */
  dmTemplateDetail: DmTemplateDetail[] = [];
  /**
   * templateId
   */
  templateSelected!: DmTemplate;
  /**
   * id component
   */
  id = commonFunction.newGuid();

  /**
   * Service
   */
  @Input() service!: BaseService<any>;

  /**
   * tranId chứng từ
   */
  @Input() tranId!: string;

  /**
   * tableName
   */
  @Input() tableName!: string;

  /**
   * selectedItem
   */
  selectedTemplate!: DmTemplate;

  /**
   * selectedItemDetail
   */
  selectedTemplateDetail!: DmTemplateDetail[];

  @ViewChild('importExcelTemplate')
  importExcelTemplate!: ImportExcelTemplateComponent;
  /**
   *
   * @param resourceManager
   * @param _ftsDialog
   * @param _eventManager
   */
  constructor(
    public resourceManager: ResourceManager,
    private _ftsDialog: FtsDialogService,
    private _eventManager: EventManager,
    private _ftsMain: FTSMain,
    private maskLoadService: MaskLoadService,
    private viewContainerRef: ViewContainerRef,
    private _fileSaveService: FtsFileSaveService
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
   * open component import excel
   * create by: TAN.VU
   * @returns
   */
  open() {
    this.title = commonFunction.getPageTitle() + ' - Import excel';
    if (!this.service) {
      this._ftsDialog.alert.show({
        icon: 'warning',
        content: 'Bạn chưa truyền service vào component!',
      });
      return;
    }

    if (!this.tranId && !this.tableName) {
      this._ftsDialog.alert.show({
        icon: 'warning',
        content: 'Bạn chưa truyền tranId hoặc tableName vào component!',
      });
      return;
    }
    this.loadData();
    this.imported = false;
    this.$vm.isShow = true;
    this.handleKeyDown();
  }

  /**
   * Load danh sách template import
   * Created by: TAN.VU
   */
  loadData() {
    /**
     * Kiểm tra không có mẫu in mới load.
     */
    if (!this.dmTemplate?.length) {
      this.mask(true);
      this.service
        .getImportTemplate(this.tranId, this.tableName)
        .then((data) => {
          this.dmTemplate = data.DM_TEMPLATE;
          this.dmTemplateDetail = data.DM_TEMPLATE_DETAIL;
          if (this.dmTemplate?.length) {
            this.selectedTemplate = this.dmTemplate[0];
            this.tempateValueChange(this.selectedTemplate);
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

  @Output() importedEvent: EventEmitter<boolean> = new EventEmitter<boolean>();

  /**
   * Đóng componet import excel
   * create by: TAN.VU
   */
  close() {
    this.$vm.isShow = false;
    this._eventManager.UnSubcriberKeyDown(this.id);
    if (this.imported) {
      this.importedEvent.emit(this.imported);
    }
  }

  addTemplate() {
    this.importExcelTemplate?.show('ADD');
  }

  viewTemplate() {
    if (this.importExcelTemplate) {
      this.importExcelTemplate?.show(
        'VIEW',
        { ...(this.selectedTemplate || {}) },
        [...(this.selectedTemplateDetail || [])]
      );
    }
  }

  /**
   * Xử lý các phím tắt
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
   * toolbarActionHandler
   * create by : TAN.VU
   * @param actionType
   */
  toolbarActionHandler(actionType: ActionType) {
    switch (actionType) {
      case 'CLOSE':
        this.close();
        break;
      case 'VIEW':
        this.viewTemplate();
        break;
      case 'EXPORT_EXCEL':
        this.btnCreateExcelFile_Click();
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

  public getColumnType(dataType: string) {
    switch (dataType) {
      case 'DATE':
        return 'date';
      case 'BOOLEAN':
        return 'boolean';
      case 'DECIMAL':
      case 'INT':
      case 'MONEY':
        return 'numeric';
    }
    return 'default';
  }

  public tempateValueChange(selectedItem: DmTemplate): void {
    this.$vm.columns = [];
    this.$vm.datas = [];
    this.selectedTemplateDetail = this.dmTemplateDetail.filter(
      (obj) => obj.FR_KEY === selectedItem.PR_KEY
    );
    //Add các cột được config
    this.selectedTemplateDetail.forEach((detail) => {
      let col = {
        FieldId: detail.DATA_COLUMN_NAME,
        Text: detail.EXCEL_COLUMN_NO,
        ColumnType: this.getColumnType(detail.DATA_TYPE),
        Width: 200,
      } as FtsColumn;
      this.$vm.columns.push(col);
    });
    //Add thêm cột STATUS để xem tình trạng
    //#region add các cột xem status import
    let colStatus = {
      FieldId: 'IMPORT_STATUS',
      Text: 'Import Status',
      Width: 120,
    } as FtsColumn;
    this.$vm.columns.push(colStatus);
    let colImportMsg = {
      FieldId: 'IMPORT_MSG',
      Text: 'Import Message',
      Width: 250,
    } as FtsColumn;
    this.$vm.columns.push(colImportMsg);
    //#endregion
  }

  public ddlTemplate_valueChange(value: any): void {
    this.tempateValueChange(value);
  }

  public ddlTemplate_selectionChange(value: any): void {
    //console.log('selectionChange', value);
  }

  getMapingExcelData(): Map {
    let map: Map = {};
    if (this.selectedTemplateDetail?.length) {
      this.selectedTemplateDetail.forEach((item) => {
        map[item.EXCEL_COLUMN_NO] = item.DATA_COLUMN_NAME;
      });
    }
    return map;
  }

  /**
   * uploadFile
   * create by : TAN.VU
   * @param event
   */
  uploadFile(event: Event) {
    const element = event.currentTarget as HTMLInputElement;
    let fileList: FileList | null = element.files;
    if (fileList?.item(0)) {
      const fileItem: File = fileList?.item(0) as File;
      readXlsxFile(fileItem, { map: this.getMapingExcelData() })
        .then(({ rows, errors }) => {
          if (rows?.length) {
            rows.forEach((item: any) => {
              this.selectedTemplateDetail.forEach(
                (templateDetail: DmTemplateDetail) => {
                  switch (templateDetail.DATA_TYPE.toUpperCase()) {
                    case 'DATE':
                      item[templateDetail.DATA_COLUMN_NAME] = new Date(
                        item[templateDetail.DATA_COLUMN_NAME]
                      );
                      break;
                    case 'BOOLEAN':
                      item[templateDetail.DATA_COLUMN_NAME] =
                        item[templateDetail.DATA_COLUMN_NAME] == true ||
                        item[templateDetail.DATA_COLUMN_NAME] == 1
                          ? true
                          : false;
                      break;
                    case 'DECIMAL':
                    case 'INT':
                    case 'MONEY':
                      item[templateDetail.DATA_COLUMN_NAME] = Number(
                        item[templateDetail.DATA_COLUMN_NAME]
                      );
                      break;
                  }
                }
              );
              //Add thêm cột STATUS để xem tình trạng
              item['IMPORT_STATUS'] = '';
              item['IMPORT_MSG'] = '';
            });
            this.$vm.datas = rows;
          } else {
            this.$vm.datas = [];
          }
        })
        .catch((error) => {
          console.log(error);
        });
    }
    //Clear selected
    element.value = '';
  }

  /**
   * btnUpload_Click
   * create by : TAN.VU
   */
  btnUpload_Click() {
    try {
      let DM_TEMPLATE: DmTemplate[] = [];
      DM_TEMPLATE.push(this.selectedTemplate);

      const datas = {
        DM_TEMPLATE: DM_TEMPLATE,
        DM_TEMPLATE_DETAIL: this.selectedTemplateDetail,
        EXCEL_DATA: this.$vm.datas,
      };
      const importData = { data: JSON.stringify(datas) };
      this.maskLoadService.show(this.viewContainerRef);
      this.service
        .ImportExcel(importData)
        .then((datas) => {
          this.$vm.datas = datas;
          this.imported = true;
          this.maskLoadService.hide();
        })
        .catch((err) => {
          this.maskLoadService.hide();
          this._ftsDialog.alert.show({
            content: this._ftsMain.ExceptionManager.processException(err),
            icon: 'warning',
          });
        });
    } catch (err) {
      this._ftsDialog.alert.show({
        content: err as string,
        icon: 'warning',
      });
    }
  }

  /**
   * btnCreateExcelFile_Click
   * create by : TAN.VU
   */
  btnCreateExcelFile_Click() {
    try {
      let DM_TEMPLATE: DmTemplate[] = [];
      DM_TEMPLATE.push(this.selectedTemplate);

      const datas = {
        DM_TEMPLATE: DM_TEMPLATE,
        DM_TEMPLATE_DETAIL: this.selectedTemplateDetail,
        //EXCEL_DATA: this.$vm.datas,
      };
      const importData = { data: JSON.stringify(datas) };
      this.maskLoadService.show(this.viewContainerRef);
      this.service
        .CreateExcelFile(importData)
        .then((urlExcelFile) => {
          this._fileSaveService.saveAsByUrl(urlExcelFile);
          this.maskLoadService.hide();
        })
        .catch((err) => {
          this.maskLoadService.hide();
          this._ftsDialog.alert.show({
            content: this._ftsMain.ExceptionManager.processException(err),
            icon: 'warning',
          });
        });
    } catch (err) {
      this._ftsDialog.alert.show({
        content: err as string,
        icon: 'warning',
      });
    }
  }

  onTemplateChange(state: any) {
    const {
      ActionType,
      Data: { dmTemplate, dmTemplateDetail },
    } = state;
    if (ActionType == 'ADD') {
      this.dmTemplateDetail = [...this.dmTemplateDetail, ...dmTemplateDetail];
      this.dmTemplate = [...this.dmTemplate, dmTemplate];
      this.selectedTemplate = dmTemplate;
      this.tempateValueChange(dmTemplate);
    } else if (ActionType == 'EDIT') {
      this.dmTemplateDetail = this.dmTemplateDetail.filter(
        (x) => x.FR_KEY !== dmTemplate.PR_KEY
      );
      this.dmTemplate = this.dmTemplate.filter(
        (x) => x.PR_KEY !== dmTemplate.PR_KEY
      );

      this.dmTemplateDetail = [...this.dmTemplateDetail, ...dmTemplateDetail];
      this.dmTemplate = [...this.dmTemplate, dmTemplate];

      this.selectedTemplate = dmTemplate;
      this.tempateValueChange(dmTemplate);
    } else if (ActionType == 'DELETE') {
      this.dmTemplateDetail = this.dmTemplateDetail.filter(
        (x) => x.FR_KEY !== dmTemplate.PR_KEY
      );
      this.dmTemplate = this.dmTemplate.filter(
        (x) => x.PR_KEY !== dmTemplate.PR_KEY
      );
      this.selectedTemplate = {} as DmTemplate;
      this.tempateValueChange({} as DmTemplate);
    }
  }
}
