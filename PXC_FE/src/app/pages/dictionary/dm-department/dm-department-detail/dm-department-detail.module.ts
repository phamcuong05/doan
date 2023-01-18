import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { DmDepartmentDetailComponent } from './dm-department-detail.component';

@NgModule({
  declarations: [
    DmDepartmentDetailComponent,
  ],
  imports: [
    CommonModule,
    FtsDictBaseDetailModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
  ],
  exports: [DmDepartmentDetailComponent],
})
export class DmDepartmentDetailModule { }
