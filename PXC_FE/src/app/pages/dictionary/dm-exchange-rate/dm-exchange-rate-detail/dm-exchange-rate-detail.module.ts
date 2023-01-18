import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {
  WindowContainerService,
  WindowService
} from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { FtsDatePickerModule } from 'src/app/controls/fts-date-picker/fts-date-picker.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmCurrencySelectorModule } from '../../dm-currency/dm-currency-selector/dm-currency-selector.module';
import { DmExchangeRateDetailComponent } from './dm-exchange-rate-detail.component';

/**
 *
 */
@NgModule({
  declarations: [DmExchangeRateDetailComponent],
  imports: [
    CommonModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsDatePickerModule,
    FtsTextLookupModule,
    DmCurrencySelectorModule,
    FtsNumerictextboxModule
  ],
  exports: [DmExchangeRateDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmExchangeRateDetailModule {}
