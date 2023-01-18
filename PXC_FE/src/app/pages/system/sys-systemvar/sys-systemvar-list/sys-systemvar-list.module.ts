import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { SysSystemVarListComponent } from './sys-systemvar-list.component';
import { SysSystemVarDetailModule } from '../sys-systemvar-detail/sys-systemvar-detail.module';


@NgModule({
  declarations: [SysSystemVarListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: SysSystemVarListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    SysSystemVarDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class SysSystemVarListModule { }
