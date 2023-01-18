import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';
import { DmWarehouseDetailComponent } from './dm-warehouse-detail.component';
import { DmDepartmentSelectorModule } from '../../dm-department/dm-department-selector/dm-department-selector.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';


@NgModule({
  declarations: [DmWarehouseDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    DmDepartmentSelectorModule,
    FtsTextLookupModule,
  ],
  exports:[DmWarehouseDetailComponent],
  providers:[WindowService,WindowContainerService]
})
export class DmWarehouseDetailModule { }
