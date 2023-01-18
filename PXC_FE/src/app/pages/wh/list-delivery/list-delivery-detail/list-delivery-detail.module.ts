import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListDeliveryDetailComponent } from './list-delivery-detail.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsDatePickerModule } from 'src/app/controls/fts-date-picker/fts-date-picker.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';
import { DmOrganizationSelectorModule } from 'src/app/pages/system/dm-organizarion/dm-organization-selector/dm-organization-selector.module';



@NgModule({
  declarations: [
    ListDeliveryDetailComponent
  ],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDatePickerModule,
    FtsTextLookupModule,
    FtsDictBaseDetailModule,
    FtsNumerictextboxModule,
    DmOrganizationSelectorModule
  ],
  exports: [ListDeliveryDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class ListDeliveryDetailModule { }
