import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmPrDetailDetailComponent } from './dm-pr-detail-detail.component';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmPrDetailClassSelectorModule } from '../../dm-pr-detail-class/dm-pr-detail-class-selector/dm-pr-detail-class-selector.module';
import { DmAccountSelectorModule } from '../../dm-account/dm-account-selector/dm-account-selector.module';
import { DmProvinceSelectorModule } from '../../dm-province/dm-province-selector/dm-province-selector.module';
import { DmDistrictSelectorModule } from '../../dm-district/dm-district-selector/dm-district-selector.module';

@NgModule({
  declarations: [DmPrDetailDetailComponent],
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
  exports: [DmPrDetailDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmPrDetailDetailModule {}
