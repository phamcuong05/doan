import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ColumnComponent } from '@progress/kendo-angular-grid';
import { TooltipDirective } from '@progress/kendo-angular-tooltip';
import { take } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { ResourceManager } from 'src/app/common/resource-manager';
import { ActionType } from 'src/app/common/types';
import { DmTemplate } from 'src/app/model/system/dm-template/dm-template';
import { DmTemplateDetail } from 'src/app/model/system/dm-template/dm-template-detail';
import {
  DmTemplateManager,
  DmTemplateService,
} from 'src/app/model/system/dm-template/dm-template-service';
import { FtsDialogService } from '../../fts-dialog/fts-dialog.service';
import { FtsColumn, FtsGridComponent } from '../../fts-grid/fts-grid.component';

@Component({
  selector: 'import-excel-template',
  templateUrl: './import-excel-template.component.html',
  styleUrls: ['./import-excel-template.component.scss'],
})
export class ImportExcelTemplateComponent implements OnInit, OnDestroy {
  id: string = commonFunction.newGuid();
  isShow: boolean = false;
  showMask: boolean = false;

  formMode: ActionType = 'VIEW';

  dataType: { ID: string }[] = [
    {
      ID: 'STRING',
    },
    {
      ID: 'DATE',
    },
    {
      ID: 'BOOLEAN',
    },
    {
      ID: 'DECIMAL',
    },
    {
      ID: 'INT',
    },
    {
      ID: 'MONEY',
    },
  ];

  constructor(
    public resourceManager: ResourceManager,
    private _formBuilder: FormBuilder,
    private _eventManager: EventManager,
    private _ftsMain: FTSMain,
    private _ftsDialogService: FtsDialogService,
    public dmTempalteService: DmTemplateService,
    public el: ElementRef
  ) {}

  @ViewChild('grdDetail') grdDetail!: FtsGridComponent;

  @ViewChild(TooltipDirective) tooltipDir!: TooltipDirective;

  /**
   * tranId chứng từ
   */
  @Input() tranId!: string;

  /**
   * tableName
   */
  @Input() tableName!: string;

  @Input() template: DmTemplate = {} as DmTemplate;
  @Input() templateDetails: DmTemplateDetail[] = [];

  @Output() templateChange: EventEmitter<{
    ActionType: ActionType;
    Data: DmTemplateManager;
  }> = new EventEmitter();

  detailColumns: FtsColumn[] = [
    { FieldId: 'EXCEL_COLUMN_NO', Width: 220 },
    { FieldId: 'DATA_COLUMN_NAME', Width: 220 },
    {
      FieldId: 'LIST_ORDER',
      ColumnType: 'numeric',
      Width: 100,
      Format: 'n0',
    } as FtsColumn,
    {
      FieldId: 'IS_PR_KEY',
      ColumnType: 'boolean',
      Width: 100,
    },
    {
      FieldId: 'DATA_TYPE',
      ColumnType: 'combo',
      Data: this.dataType,
      ValueField: 'ID',
      TextField: 'ID',
      Width: 120,
    },
  ] as FtsColumn[];

  getNewRecord(): object {
    return {
      PR_KEY: Math.floor(Date.now() * Math.random()).toString(),
      FR_KEY: this.template.PR_KEY,
      EXCEL_COLUMN_NO: '',
      DATA_COLUMN_NAME: '',
      LIST_ORDER: 0,
      DATA_TYPE: 'STRING',
      IS_PR_KEY: false,
    };
  }

  formGroup!: FormGroup;

  get itemDetailSelected(): any {
    return this.grdDetail.itemSelected;
  }

  public formGroupDetail!: FormGroup;

  initDetailsFormGroup(): void {
    this.formGroupDetail = this.createEditFormGroup();
  }
  createEditFormGroup = (): FormGroup => {
    const that = this;
    let form = that._formBuilder.group({
      EXCEL_COLUMN_NO: ['', [Validators.required, Validators.maxLength(50)]],
      DATA_COLUMN_NAME: ['', [Validators.required, Validators.maxLength(50)]],
      LIST_ORDER: [0, [Validators.min(0)]],
      IS_PR_KEY: [''],
      DATA_TYPE: ['', [Validators.required]],
    });
    return form;
  };

  initFormGroup(): void {
    this.formGroup = this._formBuilder.group({
      TEMPLATE_NAME: ['', [Validators.required, Validators.maxLength(100)]],
    });
  }

  ngOnInit(): void {
    this.initFormGroup();
    this.initDetailsFormGroup();
  }

  ngAfterViewInit(): void {}

  ngOnDestroy(): void {
    this._eventManager.UnSubcriberKeyDown(this.id);
  }

  onAddTemplate() {
    this.template = {
      PR_KEY: Math.floor(Date.now() * Math.random()).toString(),
      TRAN_ID: 'TEMPLATE',
      TEMPLATE_NAME: '',
      TABLE_NAME: this.tableName || '',
      TRAN_ID_NAME: this.tranId || '',
      IS_FIRST_ROW_DATA: false,
      ACTIVE: true,
      USER_ID: this._ftsMain.UserInfo.UserID,
    } as DmTemplate;

    this.templateDetails = [
      {
        PR_KEY: Math.floor(Date.now() * Math.random()).toString(),
        FR_KEY: this.template.PR_KEY,
        EXCEL_COLUMN_NO: '',
        DATA_COLUMN_NAME: '',
        LIST_ORDER: 0,
        DATA_TYPE: 'STRING',
        IS_PR_KEY: false,
      },
    ];
    this.formMode = 'ADD';
  }

  onDuplicateTemplate() {
    this.template = {
      ...this.template,
      PR_KEY: Math.floor(Date.now() * Math.random()).toString(),
      USER_ID: this._ftsMain.UserInfo.UserID,
    };

    for (let i = 0; i < this.templateDetails.length; i++) {
      const item = this.templateDetails[i];
      item.PR_KEY = Math.floor(Date.now() * Math.random()).toString();
      item.FR_KEY = this.template.PR_KEY;
    }

    this.formMode = 'ADD';
  }

  show(
    actionType: 'ADD' | 'VIEW',
    template?: DmTemplate,
    templateDetails?: DmTemplateDetail[]
  ) {
    this.handleKeyDown();

    if (actionType == 'ADD') {
      this.template = {
        PR_KEY: Math.floor(Date.now() * Math.random()).toString(),
        TRAN_ID: 'TEMPLATE',
        TEMPLATE_NAME: '',
        TABLE_NAME: this.tableName,
        TRAN_ID_NAME: this.tranId,
        IS_FIRST_ROW_DATA: false,
        ACTIVE: true,
        USER_ID: this._ftsMain.UserInfo.UserID,
      } as DmTemplate;
      this.templateDetails = [
        {
          PR_KEY: Math.floor(Date.now() * Math.random()).toString(),
          FR_KEY: this.template.PR_KEY,
          EXCEL_COLUMN_NO: '',
          DATA_COLUMN_NAME: '',
          LIST_ORDER: 0,
          DATA_TYPE: 'STRING',
          IS_PR_KEY: false,
        },
      ];
      this.formMode = 'ADD';
      this.isShow = true;
    } else {
      if (template) {
        this.template = template;
        this.templateDetails = templateDetails || [];
        this.formMode = 'VIEW';
        this.isShow = true;
      }
    }
  }

  close() {
    this.isShow = false;
    this._eventManager.UnSubcriberKeyDown(this.id);
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

      //tổ hợp phím ctr
      if (e.ctrlKey) {
        switch (strKey) {
          case 'E':
            if (this.formMode == 'VIEW') {
              this.formMode = 'EDIT';
            }
            stopEvt = true;
            break;
          case 'S':
            if (this.formMode == 'ADD' || this.formMode == 'EDIT') {
              this.submitChange();
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
   * show thông báo lỗi validate theo control
   */
  showValidateMessage(control: AbstractControl, controlEl: any): void {
    if (control && controlEl) {
      let message = '';
      if (control?.errors?.required) {
        message = `Trường này không được để trống`;
      } else if (control?.errors?.maxlength) {
        message = `Trường này tối đa ${control?.errors?.maxlength.requiredLength} ký tự`;
      } else if (control?.errors?.minlength) {
        message = `Trường này tối thiểu ${control?.errors?.minlength.requiredLength} ký tự`;
      } else if (control?.errors?.min) {
        message = `Trường này phải lớn hơn hoặc bằng ${control?.errors?.min.min}`;
      } else if (control?.errors?.max) {
        message = `Trường này phải nhỏ hơn hoặc bằng ${control?.errors?.max.max}`;
      } else if (control?.errors?.email) {
        message = `Email không đúng định dạng`;
      }

      controlEl.setAttribute('data-tooltip', message);
      const _tooltipDir = this.tooltipDir;

      if (controlEl.timeout) {
        clearTimeout(controlEl.timeout);
      }
      controlEl.timeout = setTimeout(() => {
        _tooltipDir?.toggle(controlEl, false);
      }, 3000);

      _tooltipDir?.hide();
      _tooltipDir?.show(controlEl);
    }
  }

  focusFirstControlInvalid() {
    const controlEls: Array<any> = this.el.nativeElement.querySelectorAll(
      '[controlname],[formcontrolname]'
    );
    if (controlEls) {
      for (let i = 0; i < controlEls.length; i++) {
        const controlEl = controlEls[i];
        const controlName =
          controlEl.getAttribute('controlName') ||
          controlEl.getAttribute('formControlName');
        const control: AbstractControl = this.formGroup.controls[controlName];
        if (!control?.valid && control?.errors) {
          let inputEl: any = null;
          if (controlEl.tagName == 'INPUT') {
            inputEl = controlEl;
          } else {
            inputEl = controlEl.querySelector('input');
          }

          setTimeout(() => {
            inputEl?.focus();
            this.showValidateMessage(control, controlEl);
          }, 100);
          break;
        }
      }
    }
  }

  validateGrid(createEditFormGroup: () => FormGroup): boolean {
    const grid = this.grdDetail;
    if (grid) {
      grid.closeCell();
      const datas = grid.getDatas() || [];
      const cols = grid.grid.columnList.filter((x) => x.isVisible);
      for (let i = 0; i < datas.length; i++) {
        const row = datas[i];
        const formGroup = createEditFormGroup();
        formGroup.reset(row);
        if (formGroup.invalid) {
          for (let j = 0; j < cols.length; j++) {
            const col = cols[j] as ColumnComponent;
            if (col.field) {
              const formControl = formGroup.get(col.field);
              if (formControl?.invalid) {
                grid.itemSelected = row;
                grid.editCell(datas.indexOf(row), col);
                setTimeout(() => {
                  const controlEl = grid.el.nativeElement.querySelector(
                    `.k-grid-edit-cell [controlname="${col?.field}"],.k-grid-edit-cell [formcontrolname="${col?.field}"]`
                  );
                  if (controlEl) {
                    this.showValidateMessage(formControl, controlEl);
                  }
                }, 100);
                return false;
              }
            }
          }
        }
      }
    }
    return true;
  }

  validateBeforSave(data: DmTemplateManager): boolean {
    return this.validateGrid(this.createEditFormGroup);
  }

  submitChange(e?: Event) {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.invalid) {
      this.focusFirstControlInvalid();
      return;
    }

    let data: DmTemplateManager = {
      dmTemplate: { ...this.template },
      dmTemplateDetail: [...this.grdDetail.getDatas()],
    } as DmTemplateManager;

    if (this.validateBeforSave(data)) {
      this.showMask = true;
      this.dmTempalteService
        .Update(data)
        .then((data) => {
          this.templateChange.emit({
            ActionType: this.formMode,
            Data: data,
          });
          this.formMode = 'VIEW';
        })
        .catch((error) => {
          this._ftsDialogService.alert.show({
            content: this._ftsMain.ExceptionManager.processException(error),
            icon: 'warning',
          });
        })
        .finally(() => {
          this.showMask = false;
        });
    }
  }

  onDeleteTemplate() {
    this._ftsDialogService.confirm
      .show({
        icon: 'question',
        content: commonFunction.stringFormat(
          this.resourceManager.CommonResource.MyResource.ConfirmBeforDelete,
          `${this.template.TEMPLATE_NAME}`
        ),
      })
      .pipe(take(1))
      .subscribe((state) => {
        if (state == 'yes') {
          this.showMask = true;
        }

        this.dmTempalteService
          .DeleteData(this.template.PR_KEY)
          .then(() => {
            this.templateChange.emit({
              ActionType: 'DELETE',
              Data: {
                FieldName: '',
                dmTemplate: { ...this.template },
                dmTemplateDetail: [...this.templateDetails],
              },
            });
            this.formMode = 'VIEW';
            this.close();
          })
          .catch((error) => {
            this._ftsDialogService.alert.show({
              content: this._ftsMain.ExceptionManager.processException(error),
              icon: 'warning',
            });
          })
          .finally(() => {
            this.showMask = false;
          });
      });
  }
}
