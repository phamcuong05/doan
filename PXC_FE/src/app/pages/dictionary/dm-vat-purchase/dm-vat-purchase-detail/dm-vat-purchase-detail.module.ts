import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmVatPurchaseDetailComponent } from './dm-vat-purchase-detail.component';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmAccountSelectorModule } from '../../dm-account/dm-account-selector/dm-account-selector.module';
import { DmPrDetailClassSelectorModule } from '../../dm-pr-detail-class/dm-pr-detail-class-selector/dm-pr-detail-class-selector.module';
import { DmDistrictSelectorModule } from '../../dm-district/dm-district-selector/dm-district-selector.module';
import { DmProvinceSelectorModule } from '../../dm-province/dm-province-selector/dm-province-selector.module';
import { ReactiveFormsModule } from '@angular/forms';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';



@NgModule({
  declarations: [
    DmVatPurchaseDetailComponent
  ],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    FtsDictBaseDetailModule,
    DmPrDetailClassSelectorModule,
    DmAccountSelectorModule,
    DmProvinceSelectorModule,
    DmDistrictSelectorModule
  ],
  exports: [DmVatPurchaseDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmVatPurchaseDetailModule { }
