import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { SysResourceListComponent } from './sys-resource-list.component';
import { SysResourceDetailModule } from '../sys-resource-detail/sys-resource-detail.module';


@NgModule({
  declarations: [SysResourceListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: SysResourceListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    SysResourceDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class SysResourceListModule { }
