import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { SysTableListComponent } from './sys-table-list.component';
import { SysTableDetailModule } from '../sys-table-detail/sys-table-detail.module';


@NgModule({
  declarations: [SysTableListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: SysTableListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    SysTableDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class SysTableListModule { }
