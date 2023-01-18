import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmUnitListComponent } from './dm-unit-list.component';
import { DmUnitDetailModule } from '../dm-unit-detail/dm-unit-detail.module';


@NgModule({
  declarations: [DmUnitListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmUnitListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmUnitDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmUnitListModule { }
