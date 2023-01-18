import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { DmDepartmentListComponent } from './dm-department-list.component';
import { DmDepartmentDetailModule } from '../dm-department-detail/dm-department-detail.module';

@NgModule({
  declarations: [DmDepartmentListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmDepartmentListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmDepartmentDetailModule,
  ],
})
export class DmDepartmentListModule {}
