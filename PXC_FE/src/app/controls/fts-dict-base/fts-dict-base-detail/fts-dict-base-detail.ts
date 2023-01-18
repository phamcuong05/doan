import {
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import { TooltipDirective } from '@progress/kendo-angular-tooltip';
import { Subject } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { FtsException } from 'src/app/base/fts-exception';
import { commonFunction } from 'src/app/common/commonFunction';
import { EnumLoadingState } from 'src/app/common/enum';
import { ActionType } from 'src/app/common/types';
import { BaseService } from 'src/app/model/base/BaseService';
import { DictBaseDetailState } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { FtsDictBaseDetailInject } from './fts-dict-base-detail-inject';
import { FtsDictBaseDetailComponent } from './fts-dict-base-detail.component';

@Component({
  template: '',
  encapsulation: ViewEncapsulation.None,
})
export abstract class FTSDictBaseDetail implements OnInit, OnDestroy {
  //#region prop
  id: string = commonFunction.newGuid();

  @ViewChild(FtsDictBaseDetailComponent)
  baseDetailComponent!: FtsDictBaseDetailComponent;

  get tooltipDir(): TooltipDirective {
    return this.baseDetailComponent.tooltipDir;
  }

  height!: number;

  abstract get formTitle(): string;
  abstract formGroup: FormGroup;
  abstract idField: string;
  abstract nameField: string;
  abstract width: number;

  private saveAndClose: boolean = false;
  defaultRecord: any;

  private onDestroy$ = new Subject<void>();
  //#endregion

  //#region ctor
  constructor(
    public myService: BaseService<any>,
    public myInject: FtsDictBaseDetailInject
  ) {}
  //#endregion

  //#region lifecycle
  ngOnInit(): void {
    this.initFormGroup();

    /**
     * combine state title window, ẩn hiện các nút task bar, enable controls
     */
    this.myInject.detailStore.actionType$
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((actionType) => {
        let title: string = this.formTitle;
        switch (actionType) {
          case 'ADD':
            title = `${this.myInject.resourceManager.CommonResource.MyResource.Add} - ${title}`;
            this.myInject.detailStore.setCurrentRow(this.getNewRecord());
            break;
          case 'DUPLICATE':
            title = `${this.myInject.resourceManager.CommonResource.MyResource.Add} - ${title}`;
            this.setDataDuplicate(this.getCurrentRow());
            break;
          case 'EDIT':
            title = `${this.myInject.resourceManager.CommonResource.MyResource.Edit} - ${title}`;
            break;
        }

        this.myInject.detailStore.setTitle(title);

        setTimeout(() => {
          this.setEnableFormControls(actionType);
          this.focusFirstControl();
        }, 1);
      });

    this.myInject.detailStore.actionClick
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((actionType) => {
        let currentRow = this.getCurrentRow();
        switch (actionType) {
          case 'CLOSE':
            this.close();
            break;
          case 'DELETE':
            this.delete(currentRow);
            break;
          case 'ADD':
            this.add();
            break;
          case 'DUPLICATE':
            this.duplicate(currentRow);
            break;
          case 'EDIT':
            this.edit();
            break;
          case 'SAVE':
            this.submitChange(currentRow);
            break;
          default:
            this.myInject.ftsDialogService.alert.show({
              icon: 'warning',
              content:
                this.myInject.resourceManager.CommonResource.MyResource
                  .MessageFunctionUnderDevelop,
            });
            break;
        }
      });
  }

  ngAfterViewInit(): void {}

  ngOnDestroy(): void {
    this.myInject?.eventManager?.UnSubcriberKeyDown(this.id);
    this.onDestroy$.next();
  }
  //#endregion

  //#region method

  abstract initFormGroup(): void;

  /**
   * Khởi tạo các giá trị cho record khi thêm mới
   */
  getNewRecord(): object {
    return { ...this.defaultRecord };
  }

  getDefauldRecord() {
    return new Promise<any>((resolve, reject) => {
      this.myInject.detailStore.setLoadingState(EnumLoadingState.Loading);
      this.myService
        .AddNewData()
        .then((item: any) => {
          this.defaultRecord = item;
          resolve(item);
        })
        .catch((err) => {
          this.myInject.ftsDialogService.alert.show({
            content:
              this.myInject.FTSMain.ExceptionManager.processException(err),
            icon: 'warning',
            maxWidth: 300,
          });
          reject(err);
        })
        .finally(() => {
          this.myInject.detailStore.setLoadingState(EnumLoadingState.Complete);
        });
    });
  }

  /**
   * Lấy currentRow hiện tại
   */
  public getCurrentRow(): any {
    let currentRow = null;
    this.myInject.detailStore.currentRow$
      .pipe(take(1))
      .subscribe((_currentRow) => {
        currentRow = _currentRow;
      });
    return currentRow;
  }

  /**
   * Lấy actionType
   */
  public getActionType(): ActionType {
    let actionType: ActionType = 'NONE';
    this.myInject.detailStore.actionType$.pipe(take(1)).subscribe((type) => {
      actionType = type;
    });
    return actionType;
  }

  setDataDuplicate(currentRow: any): any {
    let newRecord: any = this.getNewRecord();
    currentRow = {
      ...currentRow,
      [this.idField]: newRecord[this.idField],
    };
    return currentRow;
  }

  /**
   * Load danh mục
   */
  loadDm(): void {}

  /**
   * đóng form Editor
   */
  close(): void {
    if (!this.preventClose()) {
      this.myInject.detailStore.setLoadingState(EnumLoadingState.Init);
      this.myInject.detailStore.setActionType('NONE');
      this.myInject.detailStore.setIsShow(false);
      this.myInject?.eventManager?.UnSubcriberKeyDown(this.id);
    }
  }

  /**
   * Đóng cửa sổ detail
   * Khi ở actionType khác view thì xác nhận trước khi đóng
   */
  preventClose(): boolean {
    this.myInject.detailStore.setLoadingState(EnumLoadingState.Init);
    this.setTextBottom('');
    let actionType = this.getActionType();
    const formValue = this.formGroup?.value;

    if (actionType != 'VIEW' && formValue) {
      let currentRow = { ...this.getCurrentRow(), ...formValue };
      let currentRowOld = undefined;
      this.myInject.detailStore.currentRowOld$
        .pipe(take(1))
        .subscribe((_currentRowOld) => {
          currentRowOld = _currentRowOld;
        });
      if (JSON.stringify(currentRow) !== JSON.stringify(currentRowOld)) {
        this.myInject.ftsDialogService.confirmSave
          .show({
            icon: 'question',
            content:
              this.myInject.resourceManager.CommonResource.MyResource
                .MessageCloseDataChange,
          })
          .pipe(take(1))
          .subscribe((value) => {
            if (value == 'yes') {
              this.saveAndClose = true;
              this.myInject.detailStore.actionClick.emit('SAVE');
            } else {
              this.myInject.detailStore.setLoadingState(EnumLoadingState.Init);
              this.myInject.detailStore.setActionType('NONE');
              this.myInject.detailStore.setIsShow(false);
            }
          });
        return true;
      }
    }
    return false;
  }

  /**
   * set enable control nhập liệu theo action type
   * @param actionType
   */
  setEnableFormControls(actionType: ActionType) {
    for (const key in this.formGroup.controls) {
      if (Object.prototype.hasOwnProperty.call(this.formGroup.controls, key)) {
        const control = this.formGroup.controls[key];
        if (actionType == 'ADD' || actionType == 'DUPLICATE') {
          control.enable();
        } else if (actionType == 'EDIT' && key != this.idField) {
          control.enable();
        } else {
          control.disable();
        }
      }
    }
  }

  /**
   * focus vào ô nhập liệu đầu tiên
   */
  focusFirstControl() {
    let elInvalids = this.myInject.el.nativeElement.querySelectorAll(
      '.control-input'
    ) as Array<any>;

    for (let index = 0; index < elInvalids.length; index++) {
      const item = elInvalids[index];
      if (item.tagName == 'INPUT') {
        if (!item.disabled && !item.readOnly) {
          item.focus();
          return;
        }
      } else {
        const inputEl = item.querySelector('input');
        if (inputEl && !inputEl.disabled && !inputEl.readOnly) {
          inputEl.focus();
          return;
        }
      }
    }
  }

  /**
   * focus vào ô nhập liệu bị lỗi đầu tiên
   */
  focusFirstControlInvalid() {
    const controlEls: Array<any> =
      this.myInject.el.nativeElement.querySelectorAll(
        '[controlname],[formcontrolname]'
      );
    if (controlEls) {
      for (let i = 0; i < controlEls.length; i++) {
        const controlEl = controlEls[i];
        const controlName =
          controlEl.getAttribute('controlName') ||
          controlEl.getAttribute('formControlName');
        const control: AbstractControl = this.formGroup.controls[controlName];
        if (!control?.valid && control.errors) {
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

  /**
   * Xóa bản ghi
   * Created by: MTLUC - 03/11/2021
   */
  delete(currentRow: any): void {
    this.myInject.ftsDialogService.confirm
      .show({
        icon: 'question',
        content: commonFunction.stringFormat(
          this.myInject.resourceManager.CommonResource.MyResource
            .ConfirmBeforDelete,
          `${currentRow[this.nameField]} - ${currentRow[this.idField]}`
        ),
      })
      .pipe(take(1))
      .subscribe((state) => {
        if (state == 'yes') {
          this.myInject.detailStore.setLoadingState(EnumLoadingState.Loading);
          this.myService
            .Delete(currentRow[this.idField])
            .then(() => {
              this.myInject.detailStore.formActionComplete({
                currentRow: currentRow,
                error: undefined,
              });

              this.myInject.detailStore.actionResult.emit({
                actionType: 'DELETE',
                success: true,
                data: currentRow,
              });

              this.myInject.detailStore.setLoadingState(
                EnumLoadingState.Complete
              );
              this.myInject.detailStore.setActionType('NONE');
              this.myInject.detailStore.setIsShow(false);

              this.myInject.notificationService.show({
                content:
                  this.myInject.resourceManager.CommonResource.MyResource
                    .DeleteSuccess,
                hideAfter: 1000,
                position: { horizontal: 'right', vertical: 'bottom' },
                animation: { type: 'fade', duration: 400 },
                type: { style: 'success', icon: true },
              });
            })
            .catch((error) => {
              this.myInject.detailStore.formActionComplete({
                currentRow: currentRow,
                error: error,
              });
              this.myInject.ftsDialogService.alert.show({
                icon: 'warning',
                maxWidth: 300,
                content:
                  this.myInject.FTSMain.ExceptionManager.processException(
                    error
                  ),
              });
            })
            .finally(() => {
              this.myInject.detailStore.setLoadingState(
                EnumLoadingState.Complete
              );
            });
        } else {
          this.myInject.detailStore.formActionComplete({
            currentRow: currentRow,
            error: undefined,
          });
        }
      });
  }

  add(): void {
    this.myInject.detailStore.setCurrentRow(this.getNewRecord());
    this.setEnableFormControls('ADD');
    this.myInject.detailStore.setActionType('ADD');
  }

  duplicate(currentRow: any): void {
    let newRecord: any = this.getNewRecord();
    this.myInject.detailStore.setCurrentRow({
      ...currentRow,
      [this.idField]: newRecord[this.idField],
    });
    this.myInject.detailStore.setCurrentRowOld({ ...this.getCurrentRow() });
    this.setEnableFormControls('DUPLICATE');
    this.myInject.detailStore.setActionType('DUPLICATE');
  }

  edit(): void {
    this.setEnableFormControls('EDIT');
    this.myInject.detailStore.setActionType('EDIT');
  }

  submitChange(currentRow: any): void {
    let actionType = this.getActionType();
    this.setTextBottom('');
    if (actionType == 'ADD' || actionType == 'DUPLICATE') {
      this.insert(currentRow);
    } else if (actionType == 'EDIT') {
      this.update(currentRow);
    }
  }

  /**
   * create by: TAN.VU
   * Set text Bottom:
   */
  public setTextBottom(text: string) {
    let domEl: HTMLElement | null = document.querySelector('#form-bottom');
    if (!domEl) return;
    domEl.textContent = text;
  }

  /**
   * create by: TAN.VU
   * focus control
   */
  public focusControl(key: string) {
    if (key.length > 0) {
      if (this.formGroup.controls[key].valid) {
        const invalidControl = this.myInject.el.nativeElement.querySelector(
          '[formcontrolname="' + key + '"]'
        );
        invalidControl.focus();
      }
    }
  }

  /**
   * create by: TAN.VU
   * checkBusinessRules
   */
  checkBusinessRules(currentRow: any) {}

  /**
   * Thêm mới bản ghi
   * Created by: MTLUC - 03/11/2021
   */
  insert(currentRow: any): void {
    try {
      this.formGroup.markAllAsTouched();
      if (this.formGroup.invalid) {
        this.myInject.detailStore.formActionComplete({
          currentRow: currentRow,
          error: 'invalid',
        });
        this.focusFirstControlInvalid();
        return;
      }
      let data = {
        ...currentRow,
        ...this.formGroup.value,
      };
      /**TAN.VU: Set tạm UpperCase cho idField */
      data[this.idField] = data[this.idField].toUpperCase();

      this.checkBusinessRules(data);

      this.myInject.detailStore.setLoadingState(EnumLoadingState.Loading);

      this.myService
        .CreateData(data)
        .then((item: any) => {
          this.myInject.detailStore.formActionComplete({
            currentRow: item,
            error: undefined,
          });

          this.refreshForm(item);

          this.myInject.detailStore.actionResult.emit({
            actionType: 'ADD',
            success: true,
            data: item,
          });

          if (this.saveAndClose) {
            this.myInject.detailStore.setIsShow(false);
            this.saveAndClose = false;
          }

          this.myInject.notificationService.show({
            content:
              this.myInject.resourceManager.CommonResource.MyResource
                .SaveSuccess,
            hideAfter: 1000,
            position: { horizontal: 'right', vertical: 'bottom' },
            animation: { type: 'fade', duration: 400 },
            type: { style: 'success', icon: true },
          });
        })
        .catch((error) => {
          this.myInject.detailStore.formActionComplete({
            currentRow: currentRow,
            error: error,
          });
          this.myInject.ftsDialogService.alert.show({
            icon: 'warning',
            maxWidth: 300,
            content:
              this.myInject.FTSMain.ExceptionManager.processException(error),
          });
        });
    } catch (error) {
      let ftsException: FtsException = error as FtsException;
      this.setTextBottom(ftsException.mMessage);
      this.focusControl(ftsException.mFieldName);
    }
  }

  /**
   * Cập nhật bản ghi
   */
  update(currentRow: any): void {
    try {
      this.formGroup.markAllAsTouched();
      if (this.formGroup.invalid) {
        this.myInject.detailStore.formActionComplete({
          currentRow: currentRow,
          error: 'invalid',
        });
        this.focusFirstControlInvalid();
        return;
      }

      let data = {
        ...currentRow,
        ...this.formGroup.value,
      };

      this.checkBusinessRules(data);

      this.myInject.detailStore.setLoadingState(EnumLoadingState.Loading);

      this.myService
        .UpdateData(data)
        .then((item: any) => {
          this.refreshForm(item);

          this.myInject.detailStore.actionResult.emit({
            actionType: 'EDIT',
            success: true,
            data: item,
          });

          if (this.saveAndClose) {
            this.myInject.detailStore.setIsShow(false);
            this.saveAndClose = false;
          }

          this.myInject.notificationService.show({
            content:
              this.myInject.resourceManager.CommonResource.MyResource
                .SaveSuccess,
            hideAfter: 1000,
            position: { horizontal: 'right', vertical: 'bottom' },
            animation: { type: 'fade', duration: 400 },
            type: { style: 'success', icon: true },
          });
        })
        .catch((error) => {
          this.myInject.detailStore.formActionComplete({
            currentRow: currentRow,
            error: error,
          });
          this.myInject.ftsDialogService.alert.show({
            icon: 'warning',
            maxWidth: 300,
            content:
              this.myInject.FTSMain.ExceptionManager.processException(error),
          });
        });
    } catch (error) {
      let ftsException: FtsException = error as FtsException;
      this.setTextBottom(ftsException.mMessage);
      this.focusControl(ftsException.mFieldName);
    }
  }

  refreshForm(currentRow: any) {
    this.myInject.detailStore.setState({
      actionType: 'VIEW',
      currentRow: { ...currentRow },
      currentRowOld: { ...currentRow },
      error: undefined,
      loadingState: EnumLoadingState.Complete,
      width: this.width,
      height: this.height,
      title: this.formTitle,
      isShow: true,
    } as DictBaseDetailState);
  }

  handleKeyDown() {
    const that = this;
    that.myInject.eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      that.myInject.detailStore.state$.pipe(take(1)).subscribe((state) => {
        if (state.loadingState != EnumLoadingState.Loading) {
          const strKey = e.key.toUpperCase();
          let stopEvt = false;
          //esc
          if (strKey == 'ESCAPE') {
            this.myInject.detailStore.actionClick.emit('CLOSE');
            stopEvt = true;
          }
          //tổ hợp phím ctr
          else if (e.ctrlKey) {
            switch (strKey) {
              case 'I':
                if (state.actionType == 'VIEW') {
                  this.myInject.detailStore.actionClick.emit('ADD');
                }
                stopEvt = true;
                break;
              case 'E':
                if (state.actionType == 'VIEW') {
                  this.myInject.detailStore.actionClick.emit('EDIT');
                }
                stopEvt = true;
                break;
              case 'D':
                if (state.actionType == 'VIEW') {
                  this.myInject.detailStore.actionClick.emit('DELETE');
                }
                stopEvt = true;
                break;
              case 'S':
                if (
                  state.actionType == 'ADD' ||
                  state.actionType == 'EDIT' ||
                  state.actionType == 'DUPLICATE'
                ) {
                  this.myInject.detailStore.actionClick.emit('SAVE');
                }
                stopEvt = true;
                break;
              case 'X':
                if (state.actionType == 'VIEW') {
                  this.myInject.detailStore.actionClick.emit('DUPLICATE');
                }
                stopEvt = true;
                break;
            }
          }

          if (stopEvt) {
            e.preventDefault();
            this.myInject.cdr.detectChanges();
          }
        }
      });
    });
  }

  /**
   * TAN.VU
   * Các xử lý khác
   */
  public updateInfo(currentRow: any): void {}

  /**
   * Mở popup
   */
  public open(currentRow: any, actionType: ActionType) {
    this.myInject.detailStore.setState({
      actionType: actionType,
      currentRow: currentRow,
      currentRowOld: { ...currentRow },
      error: undefined,
      loadingState: EnumLoadingState.Init,
      width: this.width,
      height: this.height,
      title: this.formTitle,
      isShow: false,
    } as DictBaseDetailState);
    this.saveAndClose = false;

    this.myInject?.eventManager?.UnSubcriberKeyDown(this.id);
    this.handleKeyDown();

    new Promise<void>((resolve, reject) => {
      if (!this.defaultRecord) {
        this.getDefauldRecord().then(() => {
          this.loadDm();
          resolve();
        });
      } else {
        resolve();
      }
    }).then(() => {
      if (actionType == 'ADD') {
        currentRow = this.getNewRecord();
        this.myInject.detailStore.setCurrentRow(currentRow);
      }
      //Xử lý khác
      this.updateInfo(currentRow);

      //Thay đổi hết trạng thái khác rồi mới set show
      this.myInject.detailStore.setIsShow(true);

      this.myInject.detailStore.setCurrentRowOld({ ...this.getCurrentRow() });
    });
  }

  //#endregion
}
