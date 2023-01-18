import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  ViewEncapsulation,
} from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FTSMain } from 'src/app/base/ftsmain';
import { commonFunction } from 'src/app/common/commonFunction';
import { LocalStorage } from 'src/app/common/local-storage';
import { ResourceManager } from 'src/app/common/resource-manager';
import { Period } from 'src/app/model/other/period';

@Component({
  selector: 'period',
  templateUrl: './period.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class PeriodComponent implements OnInit, OnDestroy {
  @Input() formName: string = 'Period';
  @Input() periods: Array<Period> = commonFunction.getPeriod(
    this.ftsMain,
    this.resourceManager
  );

  private _period!: Period;
  @Input() set period(v: Period) {
    if (v && v.Id && this._period?.Id != v.Id) {
      this._period = v;
      this.localStorage.setPeriod(v, this.formName);
    }
    this.periodChange.emit(this._period);
  }
  get period(): Period {
    return this._period;
  }
  @Output('periodChange') periodChange: EventEmitter<Period> =
    new EventEmitter();

  get readonlyDatepiker(): boolean {
    if (this.period && this.period.Id == 'Any') {
      return false;
    }
    return true;
  }

  @Input() showDropdownPeriod: boolean = true;
  @Input() hideDatePicker: boolean = false;

  private onDestroy$ = new Subject<void>();

  constructor(
    public resourceManager: ResourceManager,
    private localStorage: LocalStorage,
    private ftsMain: FTSMain
  ) {
    if (!this.period) {
      const periodOld =
        localStorage.getPeriod(this.formName) || this.periods?.[0];
      this.period = this.periods?.[0]; // this.periods.find((x) => x.Id === periodOld.Id) || this.periods?.[0];
    }
    localStorage.EventChangeLanguage.pipe(takeUntil(this.onDestroy$)).subscribe(
      (state) => {
        const that = this;
        let fn = function () {
          if (that.resourceManager.CommonResource.MyResource?.ToDay) {
            that.periods = commonFunction.getPeriod(
              that.ftsMain,
              that.resourceManager
            );
            let _peri = that.periods.find((item) => item.Id == that.period.Id);
            if (_peri) {
              that.period = { ...that.period, Text: _peri.Text };
            } else {
              that.period = that.periods?.[0];
            }
          } else {
            setTimeout(() => {
              fn();
            }, 100);
          }
        };
        fn();
      }
    );
  }
  ngOnDestroy(): void {
    this.onDestroy$.next();
  }

  ngOnInit(): void {}
}
