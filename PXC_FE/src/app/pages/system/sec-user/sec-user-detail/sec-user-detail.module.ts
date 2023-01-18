import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { SecUserDetailComponent } from './sec-user-detail.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmOrganizationSelectorModule } from '../../dm-organizarion/dm-organization-selector/dm-organization-selector.module';
import { DmEmployeeSelectorModule } from 'src/app/pages/dictionary/dm-employee/dm-employee-selector/dm-employee-selector.module';
import { FtsMultiSelectModule } from 'src/app/controls/fts-multi-select/fts-multi-select.module';



@NgModule({
  declarations: [SecUserDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    FtsDictBaseDetailModule,
    FtsMultiSelectModule,
    DmOrganizationSelectorModule,
    DmEmployeeSelectorModule
  ],
  exports:[SecUserDetailComponent],
  providers:[WindowService,WindowContainerService]
})
export class SecUserDetailModule { }
