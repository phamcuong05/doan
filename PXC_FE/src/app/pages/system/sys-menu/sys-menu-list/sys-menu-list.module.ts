import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { SysMenuListComponent } from './sys-menu-list.component';
import { SysMenuDetailModule } from '../sys-menu-detail/sys-menu-detail.module';


@NgModule({
  declarations: [SysMenuListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: SysMenuListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    SysMenuDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class SysMenuListModule { }
