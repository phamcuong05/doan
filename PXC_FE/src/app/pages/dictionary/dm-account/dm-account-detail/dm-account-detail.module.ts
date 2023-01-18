import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';
import { DmAccountDetailComponent } from './dm-account-detail.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmCurrencySelectorModule } from '../../dm-currency/dm-currency-selector/dm-currency-selector.module';
import { DmAccountSelectorModule } from '../dm-account-selector/dm-account-selector.module';

/**
 *TAN.VU
 */
@NgModule({
  declarations: [DmAccountDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    DmCurrencySelectorModule,
    DmAccountSelectorModule,
    FtsTextLookupModule
  ],
  exports: [DmAccountDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmAccountDetailModule { }
