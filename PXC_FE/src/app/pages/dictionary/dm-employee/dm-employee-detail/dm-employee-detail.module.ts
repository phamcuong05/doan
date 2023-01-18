import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmEmployeeDetailComponent } from './dm-employee-detail.component';
import { DmDepartmentSelectorModule } from '../../dm-department/dm-department-selector/dm-department-selector.module';

@NgModule({
  declarations: [DmEmployeeDetailComponent],
  imports: [
    CommonModule,
    FtsDictBaseDetailModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    DmDepartmentSelectorModule,
  ],
  exports: [DmEmployeeDetailComponent],
})
export class DmEmployeeDetailModule {}
