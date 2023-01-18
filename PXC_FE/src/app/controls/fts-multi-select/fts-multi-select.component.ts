import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Optional,
  Output,
  Self,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { ResourceManager } from 'src/app/common/resource-manager';
import { BaseService } from 'src/app/model/base/BaseService';

@Component({
  selector: 'fts-multi-select',
  templateUrl: './fts-multi-select.component.html',
  styleUrls: ['./fts-multi-select.component.scss'],
})
export class FtsMultiSelectComponent implements ControlValueAccessor, OnInit {
  /**
   * sevice của danh mục cần lấy
   */
  @Input() service!: BaseService<any>;
  @Input() popupWidth: number = 900;
  @Input() popupHeight: number = 450;
  @Input() formTitle!: string;

  /**
   * cột id trên danh mục cần lấy
   */
  @Input() colId: string = 'ID';

  /**
   * cột tên trên danh mục cần lấy
   */
  @Input() colName: string = 'Name';

  /**
   * Tên cột Id hiển thị trên grid. Để trống sẽ là Mã
   */
  @Input() titleColId!: string;

  /**
   * Tên cột Name hiển thị trên grid. Để trống sẽ là Tên
   */
  @Input() titleColName!: string;
  @Input() disabled = false;
  @Input() freeText: boolean = false;
  @Input() readonly: boolean = false;

  private onChanged: Function = () => {};
  private onTouched: Function = () => {};
  private updateOn: 'change' | 'blur' | 'submit' = 'change';

  public value: string = '';

  //#region show binding
  private _show: boolean = false;
  @Input() set show(v: boolean) {
    if (v !== this.show) {
      this._show = v;
      this.showChange.emit(v);
    }
  }
  get show(): boolean {
    return this._show;
  }
  @Output() showChange = new EventEmitter<boolean>();
  //#endregion

  //----------------------------------------
  constructor(
    public resourceManager: ResourceManager,
    @Optional() @Self() public ngControl: NgControl
  ) {
    if (ngControl != null) {
      ngControl.valueAccessor = this;
    }
  }

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    const control = this.ngControl && this.ngControl.control;
    if (control) {
      this.updateOn = control.updateOn;
    }
  }

  writeValue(obj: any): void {
    this.value = obj;
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

  onModelChange(value: string) {
    this.onChanged(value);
    if (this.updateOn != 'blur') {
      this.onTouched();
    }
  }
}
