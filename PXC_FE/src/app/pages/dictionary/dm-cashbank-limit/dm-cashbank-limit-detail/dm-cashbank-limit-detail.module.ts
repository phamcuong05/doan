import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmCashbankLimitDetailComponent } from './dm-cashbank-limit-detail.component';
import { DmAccountSelectorModule } from '../../dm-account/dm-account-selector/dm-account-selector.module';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { FtsDatePickerModule } from 'src/app/controls/fts-date-picker/fts-date-picker.module';
import { DmBankSelectorModule } from '../../dm-bank/dm-bank-selector/dm-bank-selector.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';



@NgModule({
  declarations: [
    DmCashbankLimitDetailComponent
  ],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    FtsDatePickerModule,
    FtsDictBaseDetailModule,
    DmAccountSelectorModule,
    DmBankSelectorModule,
    FtsNumerictextboxModule
  ],
  exports: [DmCashbankLimitDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmCashbankLimitDetailModule { }
