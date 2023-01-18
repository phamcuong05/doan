import {
  Component,
  Input,
  OnDestroy,
  OnInit,
  Optional,
  Self,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import {
  DateInputFormatPlaceholder,
  DatePickerComponent,
} from '@progress/kendo-angular-dateinputs';
import { FTSMain } from 'src/app/base/ftsmain';
import { ResourceManager } from 'src/app/common/resource-manager';

@Component({
  selector: 'fts-date-picker',
  templateUrl: './fts-date-picker.component.html',
  styles: ['fts-date-picker {width: 100%}'],
  encapsulation: ViewEncapsulation.None,
})
export class FtsDatePickerComponent
  implements ControlValueAccessor, OnInit, OnDestroy
{
  @Input() formatPlaceholder: DateInputFormatPlaceholder = {
    year: 'yyyy',
    month: 'MM',
    day: 'dd',
    hour: 'hh',
    minute: 'mm',
    second: 'ss',
  };

  @Input() format!: string;

  private _value!: Date;
  @Input() set value(v: Date) {
    this.setValue(v);
  }

  get value(): Date {
    return this._value;
  }

  @Input() readonly: boolean = false;

  @Input() classList: string = 'control-input';

  datePicker!: DatePickerComponent;
  @ViewChild(DatePickerComponent) _datePicker!: DatePickerComponent;

  constructor(
    public resourceManager: ResourceManager,
    public ftsMain: FTSMain,
    @Optional() @Self() public ngControl: NgControl
  ) {
    if (!this.format) {
      this.format = this.ftsMain.dateFormat || 'dd/MM/yyyy';
    }

    if (ngControl != null) {
      ngControl.valueAccessor = this;
    }
  }
  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.datePicker = this._datePicker;
  }

  ngOnDestroy(): void {}

  @Input() disabled = false;
  focused = false;
  private onChanged: Function = () => {};
  private onTouched: Function = () => {};
  private updateOn: 'change' | 'blur' | 'submit' = 'change';

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

  setValue(value: Date) {
    if (this._value != value) {
      this._value = value;
      this.onChanged(value);
      if (this.updateOn != 'blur') {
        this.onTouched();
      }
    }
  }

  onBlur(e: any) {
    if (this.readonly || this.disabled) {
      return;
    }
    this.focused = false;

    e?.preventDefault();
    if (this.updateOn == 'blur') {
      if (!this.ngControl || this._value != this.ngControl.value) {
        this.onTouched();
      }
    }
  }
}
