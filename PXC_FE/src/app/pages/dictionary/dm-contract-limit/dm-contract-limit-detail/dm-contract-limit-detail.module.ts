import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { DmContractLimitDetailComponent } from './dm-contract-limit-detail.component';
import { FtsDatePickerModule } from 'src/app/controls/fts-date-picker/fts-date-picker.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmPrDetailSelectorModule } from '../../dm-pr-detail/dm-pr-detail-selector/dm-pr-detail-selector.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';

@NgModule({
  declarations: [DmContractLimitDetailComponent],
  imports: [
    CommonModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsDatePickerModule,
    FtsTextLookupModule,
    DmPrDetailSelectorModule,
    FtsNumerictextboxModule,
  ],
  exports: [DmContractLimitDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmContractLimitDetailModule {}
