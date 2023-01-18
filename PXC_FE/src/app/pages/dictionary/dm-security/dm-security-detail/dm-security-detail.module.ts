import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { LabelModule } from '@progress/kendo-angular-label';
import { FtsDatePickerModule } from 'src/app/controls/fts-date-picker/fts-date-picker.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmAccountSelectorModule } from '../../dm-account/dm-account-selector/dm-account-selector.module';
import { DmCurrencySelectorModule } from '../../dm-currency/dm-currency-selector/dm-currency-selector.module';
import { DmPeriodSelectorModule } from '../../dm-period/dm-period-selector/dm-period-selector.module';
import { DmPrDetailSelectorModule } from '../../dm-pr-detail/dm-pr-detail-selector/dm-pr-detail-selector.module';
import { DmSecurityClassSelectorModule } from '../../dm-security-class/dm-security-class-selector/dm-security-class-selector.module';
import { DmSecurityDetailComponent } from './dm-security-detail.component';

@NgModule({
  declarations: [DmSecurityDetailComponent],
  imports: [
    CommonModule,
    LabelModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    FtsDictBaseDetailModule,
    DmSecurityClassSelectorModule,
    DmCurrencySelectorModule,
    DmPrDetailSelectorModule,
    FtsDatePickerModule,
    DmPeriodSelectorModule,
    DmAccountSelectorModule,
    FtsNumerictextboxModule,
  ],
  exports: [DmSecurityDetailComponent],
})
export class DmSecurityDetailModule {}
