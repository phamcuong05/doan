import {
  Component,
  ContentChild,
  ElementRef,
  Input,
  OnDestroy,
  OnInit,
  Optional,
  Self,
  TemplateRef,
  ViewChild,
  ViewContainerRef,
  ViewEncapsulation,
} from '@angular/core';
import { ControlValueAccessor, FormGroup, NgControl } from '@angular/forms';
import { WindowRef, WindowService } from '@progress/kendo-angular-dialog';
import { Subject, Subscription } from 'rxjs';
import { take, takeUntil } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { ResourceManager } from 'src/app/common/resource-manager';
import { TextLookupSelectorDirective } from 'src/app/directive/fts-text-lookup-selector-directive';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DictBaseDetailDirective } from '../../directive/fts-dict-base-detail-directive';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';
import { FTSDictBaseDetail } from '../fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';

@Component({
  selector: 'fts-text-lookup',
  templateUrl: './fts-text-lookup.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class FtsTextLookupComponent
  implements ControlValueAccessor, OnInit, OnDestroy
{
  //#region prop
  /**
   * Có hiện nút thêm nhanh hay không
   */
  @Input() showQuickAdd: boolean = false;

  @Input() freeText: boolean = true;

  /**
   * chỉ cho phép đọc dl
   */
  @Input() readonly: boolean = false;

  /**
   * control của trường name khi hiển thị tên
   */
  @Input() controlTextName: string = '';

  /**
   * item đang edit trên grid | object model
   */
  @Input() dataItem: any = {};

  /**
   * Field value trên dm
   */
  @Input() valueField: string = 'ID';

  /**
   * Field name trên dm
   */
  @Input() textField: string = '';

  /**
   * disabled
   */
  @Input() disabled = false;

  focused = false;
  private onChanged: Function = () => {};
  private onTouched: Function = () => {};
  private updateOn: 'change' | 'blur' | 'submit' = 'change';

  private _value!: string;
  @Input() set value(v: string) {
    this.setValue(v);
  }

  get value(): string {
    return this._value;
  }

  public valueText: string = '';

  destroyed: boolean = true;
  searchValue: string = '';
  private onDestroy$ = new Subject<void>();

  ftsDictBaseDetailComponent!: FTSDictBaseDetail;

  private _selectorDirective!: TextLookupSelectorDirective;
  @ContentChild(TextLookupSelectorDirective)
  get selectorDirective(): TextLookupSelectorDirective {
    return this._selectorDirective;
  }
  set selectorDirective(val: TextLookupSelectorDirective) {
    if (val) this._selectorDirective = val;
  }
  @Input() set passSelectorDirective(val: TextLookupSelectorDirective) {
    if (val) this._selectorDirective = val;
  }

  private _popupTemplate!: TemplateRef<any>;
  @ViewChild('popupTemplate') get popupTemplateRef(): TemplateRef<any> {
    return this._popupTemplate;
  }
  set popupTemplateRef(val: TemplateRef<any>) {
    if (val && !this._popupTemplate) this._popupTemplate = val;
  }
  @Input() set passPopupTemplateRef(val: TemplateRef<any>) {
    if (val) this._popupTemplate = val;
  }

  @ViewChild('btnShowPopup') btnShowPopupRef!: ElementRef;

  @ContentChild(DictBaseDetailDirective)
  dictBaseDetailDirective!: DictBaseDetailDirective;

  //#endregion

  constructor(
    private ftsDialogService: FtsDialogService,
    private windowService: WindowService,
    public resourceManager: ResourceManager,
    private ftsMain: FTSMain,
    public ctn: ViewContainerRef,
    @Optional() @Self() public ngControl: NgControl
  ) {
    if (ngControl != null) {
      ngControl.valueAccessor = this;
    }
  }

  ngOnInit(): void {
    this.destroyed = false;
  }

  ngOnDestroy(): void {
    this.destroyed = true;
    this.selectorDirective?.component?.windowRef?.close();
    this.onDestroy$.next();
  }

  ngAfterViewInit(): void {
    this.selectorDirective?.component.selectItem
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((item) => {
        if (item && this.value != item[this.valueField]) {
          this.setValueItem(item);
        }
        this.selectorDirective.component.windowRef?.close();
        this.btnShowPopupRef?.nativeElement?.focus();
      });

    if (this.dictBaseDetailDirective?.component) {
      this.ftsDictBaseDetailComponent = this.dictBaseDetailDirective?.component;

      this.ftsDictBaseDetailComponent?.myInject.detailStore.actionResult
        .pipe(takeUntil(this.onDestroy$))
        .subscribe((state) => {
          if (state.success && state.actionType == 'ADD') {
            const currentRow: any = state.data;
            if (
              this.value != currentRow[this.ftsDictBaseDetailComponent.idField]
            ) {
              this.setValueItem(currentRow);
            }
          }
        });
    }

    if (this.controlTextName) {
      this.valueText = this.dataItem[this.controlTextName];
    }
  }

  writeValue(obj: any): void {
    this._value = obj;
    if (!obj) {
      obj = 0;
    }
  }
  registerOnChange(fn: any): void {
    this.onChanged = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean) {
    this.disabled = isDisabled;
  }

  setValue(value: string) {
    if (this._value != value) {
      this._value = value;
      this.onChanged(value);
      if (this.updateOn != 'blur') {
        this.onTouched();
      }
    }
  }

  //#region quick add
  /**
   * click nút quick add
   * @param $event
   */
  quickAdd_Click($event: Event) {
    const that = this;
    if (that.ftsDictBaseDetailComponent) {
      that.ftsDictBaseDetailComponent?.open({}, 'ADD');
    } else {
      this.ftsDialogService.alert.show({
        content: `dictBaseDetail Directive not exists!`,
        icon: 'warning',
      });
    }
  }
  //#endregion

  //#region popup
  /**
   * click show popup
   */
  windowResultSubscription!: Subscription;
  showPopupSelector(keySearch?: string) {
    const that = this;
    if (this.selectorDirective) {
      const windowRef: WindowRef = that.windowService.open({
        content: that.popupTemplateRef,
      });
      this.selectorDirective.component.windowRef = windowRef;
      if (keySearch) {
        this.selectorDirective.component.txtSearchEl.nativeElement.value =
          keySearch;
      }

      this.selectorDirective.component.initWindow();
      windowRef.window.instance.onComponentKeydown = function () {
        return;
      };
      this.windowResultSubscription?.unsubscribe();
      this.windowResultSubscription = windowRef.result
        .pipe(takeUntil(this.onDestroy$))
        .subscribe((x) => {
          this.selectorDirective.component.unHandleKeyDown();
          this.btnShowPopupRef?.nativeElement?.focus();
        });
    }
  }
  //#endregion

  @Input() selectionChange = (state: {
    item: any;
    form: FormGroup;
    currentRow: any;
  }) => {};

  get controlName() {
    if (this.ngControl) {
      if (this.ngControl.name) {
        return this.ngControl.name;
      }

      if (this.ngControl?.control?.parent?.controls) {
        return commonFunction.getControlName(
          this.ngControl?.control?.parent?.controls,
          this.ngControl?.control
        );
      }
    }
    return '';
  }

  setValueItem(item: any) {
    const _form = this.ngControl?.control?.parent as FormGroup;
    if (this.dataItem) {
      this.setValue(item?.[this.valueField]);
      if (this.controlName) {
        this.dataItem[this.controlName] = item?.[this.valueField];
      }

      if (this.controlTextName) {
        this.valueText = item?.[this.textField];
        this.dataItem[this.controlTextName] = item?.[this.textField];
      }
      this.selectionChange({
        item: item,
        form: _form,
        currentRow: this.dataItem,
      });
    }
  }

  isFilter: boolean = false;
  filterItemPromise(key: string): Promise<any> {
    return new Promise<any>((resolve, reject) => {
      if (!this.isFilter && this.selectorDirective?.component?.myService) {
        this.isFilter = true;
        new Promise<any>((resolve, reject) => {
          let param: PagingParam = {
            PageIndex: 1,
            FilterGroups: [],
            Sorts: undefined,
            PageSize: this.ftsMain.PageSize,
            FilterFields: undefined,
            SumaryFields: undefined,
            TranId: '',
          };
          param = this.selectorDirective.component.setParamBeforLoad(param);

          if (!param.FilterGroups?.length) {
            param.FilterGroups = [];
          }

          const filterGroup = {
            Filters: [
              {
                Field: this.valueField,
                Operator: 'eq',
                Value: key,
              },
            ],
            Logic: 'or',
          };

          if (this.controlTextName) {
            filterGroup.Filters.push({
              Field: this.textField,
              Operator: 'eq',
              Value: key,
            });
          }

          param.FilterGroups.push(filterGroup);

          this.selectorDirective.component.myService
            .GetDataByFilter(JSON.stringify(param.FilterGroups))
            .then((items) => {
              resolve(items);
            })
            .catch((err) => {
              reject(err);
            });
        })
          .then((items) => {
            if (items?.length == 1) {
              this.setValueItem(items[0]);
            } else {
              this.setValueItem({});
            }
            resolve(items);
          })
          .catch((err) => {
            let error = undefined;
            if (err.error) {
              error = JSON.parse(err.error);
            }
            if (error?.mExceptionID == 'MSG_RECORD_NOT_EXISTS') {
              this.setValueItem({});
              resolve({});
            } else {
              this.ftsDialogService.alert
                .show({
                  icon: 'warning',
                  maxWidth: 300,
                  content: this.ftsMain.ExceptionManager.processException(err),
                })
                .pipe(take(1))
                .subscribe(() => {
                  this.setValueItem(null);
                });
              reject(err);
            }
          })
          .finally(() => {
            this.isFilter = false;
          });
      }
    });
  }

  freeTextChange(e: Event): void {
    const el = e.target as HTMLInputElement;
    if (el?.value) {
      this.filterItemPromise(el.value)
        .then((items) => {
          this.searchValue = '';
          if (!items || items.length == 0 || items.length > 1) {
            el?.focus();
            if (!this.destroyed) {
              this.searchValue = el.value;
              this.showPopupSelector(this.searchValue);
            }
          }
        })
        .catch((err) => {
          el?.focus();
        });
    } else {
      el.value = '';
      this.setValueItem({});
    }
  }
}
