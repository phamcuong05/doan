import {
  Component,
  ElementRef,
  HostBinding,
  Input,
  OnDestroy,
  OnInit,
  Optional,
  Self,
  ViewChild,
} from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { commonFunction } from 'src/app/common/commonFunction';

@Component({
  selector: 'fts-numerictextbox',
  templateUrl: './fts-numerictextbox.component.html',
  styleUrls: ['./fts-numerictextbox.component.scss'],
})
export class FtsNumerictextboxComponent
  implements ControlValueAccessor, OnInit, OnDestroy
{
  public id = commonFunction.newGuid();
  @ViewChild('inputEl') inputEl!: ElementRef<HTMLInputElement>;
  @HostBinding('class.k-numerictextbox') numerictextboxClass: boolean = true;
  valuePreview: string = '';
  value: number = 0;

  @Input() creaseButton: boolean = false;
  @Input() placeholder: string = '';

  @Input() disabled = false;
  focused = false;
  private onChanged: Function = () => {};
  private onTouched: Function = () => {};

  private updateOn: 'change' | 'blur' | 'submit' = 'change';

  //#region
  @Input() format: string = 'n2';
  @Input() max: number = 1000000000000000.0;
  @Input() min: number = -1000000000000000.0;
  @Input() separator: string = ',';
  @Input() separatorGroup: string = '.';
  @Input() readonly: boolean = false;

  private get fix(): number {
    const s = this.format?.toLocaleLowerCase().replace(/[a-zA-Z]/gi, '');
    return parseInt(s);
  }

  private get rgxFormatValue(): RegExp {
    let rgx: string = `\\d(?=(\\d{3})+$)`;
    if (this.fix > 0) {
      rgx = `\\d(?=(\\d{3})+\\${this.separator})`;
    }
    return new RegExp(rgx, 'g');
  }

  private formatValue(value: number) {
    if (value == undefined || value == NaN) {
      return '';
    }
    return this.calc(value)
      .toFixed(this.fix)
      .replace('.', this.separator)
      .replace(this.rgxFormatValue, '$&' + this.separatorGroup);
  }

  private calc(value: any) {
    const reg = new RegExp(`^-?\\\d+(?:\\\.\\\d{0,${this.fix}})?`);
    const strValue = value.toString().match(reg)[0];
    return parseFloat(strValue) || 0;
  }

  convertToValue(str: string) {
    const reg = new RegExp(`\\${this.separatorGroup}`, 'gi');
    let value: number = parseFloat(
      str.replace(reg, '').replace(this.separator, '.')
    );

    if (!value) {
      return 0;
    }
    return value;
  }

  //#endregion

  constructor(@Optional() @Self() public ngControl: NgControl) {
    if (ngControl != null) {
      ngControl.valueAccessor = this;
    }
  }

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
    this.valuePreview = this.formatValue(obj);
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

  setValue(value: number) {
    this.valuePreview = this.formatValue(value);
    if (this.value != value) {
      this.value = value;
      this.onChanged(value);
      if (this.updateOn != 'blur') {
        this.onTouched();
      }
    }
  }

  ngOnDestroy(): void {}

  ngOnInit(): void {}

  onKeyDown(e: any) {
    if (this.readonly || this.disabled) {
      return;
    }
    if (e.keyCode == 38) {
      this.increase();
      e.preventDefault();
    } else if (e.keyCode == 40) {
      this.decrease();
      e.preventDefault();
    } else if (e.ctrlKey && e.key.toLocaleLowerCase() == 'v') {
      const oldValue = this.valuePreview;
      setTimeout(() => {
        let value = this.convertToValue(this.valuePreview);

        if (!value) {
          this.valuePreview = oldValue;
        } else {
          if (value > this.max) {
            value = this.max;
          }

          if (value < this.min) {
            value = this.min;
          }
          this.setValue(value);
        }
      }, 1);
    }
  }

  onKeyPress(e: any) {
    if (this.readonly || this.disabled) {
      return;
    }
    if (e?.key && !e?.ctrlKey) {
      e.preventDefault();
      if (e.key != this.separatorGroup) {
        let idxMS = e.target.selectionStart;
        let idxME = e.target.selectionEnd;
        const oldValue = this.valuePreview;
        let value = '';
        if (idxMS == 0 && idxME == oldValue.length) {
          idxME = 0;
          value += e.key;
        } else {
          value = [
            oldValue.slice(0, idxMS),
            e.key,
            oldValue.slice(idxMS == idxME ? idxMS : idxMS + 1),
          ].join('');
        }

        let _value = this.convertToValue(value);
        value = value.replace(new RegExp(`\\${this.separatorGroup}`, 'gi'), '');
        let reg = new RegExp(`^-?\\d{1,}\\${this.separator}?\\d{0,}$`);
        if (reg.test(value)) {
          if (_value > this.max) {
            _value = this.max;
          }

          if (_value < this.min) {
            _value = this.min;
          }

          this.setValue(_value);
          value = this.formatValue(_value);
          setTimeout(() => {
            e.target.setSelectionRange(
              idxMS +
                value.split(this.separatorGroup).length -
                oldValue.split(this.separatorGroup).length +
                1,
              idxME +
                value.split(this.separatorGroup).length -
                oldValue.split(this.separatorGroup).length +
                (idxMS != idxME ? 0 : 1)
            );
          }, 1);
        } else if (e.key == this.separator) {
          setTimeout(() => {
            e.target.setSelectionRange(
              oldValue.indexOf(this.separator) + 1,
              oldValue.length
            );
          }, 1);
        }
      }
    }
  }

  onFocus(e: any) {
    this.focused = true;
  }

  onMouseDown(e: any) {
    if (e?.target?.id != this.id) {
      e.preventDefault();
      return false;
    }
    return true;
  }

  onBlur(e: any) {
    if (this.readonly || this.disabled) {
      return;
    }
    this.focused = false;
    let value = this.convertToValue(this.valuePreview);
    if (!value) {
      value = 0;
    }
    this.setValue(value);
    e.preventDefault();
    if (this.updateOn == 'blur') {
      this.onTouched();
    }
  }

  increase(e?: Event) {
    if (this.readonly || this.disabled) {
      return;
    }
    e?.stopPropagation();
    e?.preventDefault();
    if (!this.disabled) {
      this.focused = true;
      const oldValue = this.valuePreview;
      let value = this.convertToValue(oldValue);
      if (!value) {
        value = 0;
      }
      if (value <= this.max - 1) {
        this.setValue(value + 1);
      }
    }

    setTimeout(() => {
      if (e) {
        this.inputEl?.nativeElement.focus();
      }
    }, 1);
  }

  decrease(e?: Event) {
    if (this.readonly || this.disabled) {
      return;
    }
    if (!this.disabled) {
      this.focused = true;
      const oldValue = this.valuePreview;
      let value = this.convertToValue(oldValue);
      if (!value) {
        value = 0;
      }
      if (value >= this.min + 1) {
        this.setValue(value - 1);
      }
    }
    if (e) {
      this.inputEl?.nativeElement.focus();
    }
  }
}
