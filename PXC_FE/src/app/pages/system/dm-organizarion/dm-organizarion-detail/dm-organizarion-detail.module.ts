import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmOrganizarionDetailComponent } from './dm-organizarion-detail.component';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { DmOrganizarionListComponent } from '../dm-organizarion-list/dm-organizarion-list.component';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmOrganizationSelectorModule } from '../dm-organization-selector/dm-organization-selector.module';

@NgModule({
  declarations: [DmOrganizarionDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsTextLookupModule,
    DmOrganizationSelectorModule
  ],
  exports:[DmOrganizarionDetailComponent],
  providers:[WindowService,WindowContainerService]
})
export class DmOrganizarionDetailModule { }
