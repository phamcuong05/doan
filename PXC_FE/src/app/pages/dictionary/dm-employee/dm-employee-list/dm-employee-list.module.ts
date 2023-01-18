import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { DmEmployeeListComponent } from './dm-employee-list.component';
import { DmEmployeeDetailModule } from '../dm-employee-detail/dm-employee-detail.module';

@NgModule({
  declarations: [DmEmployeeListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmEmployeeListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmEmployeeDetailModule,

  ],
})
export class DmEmployeeListModule { }
