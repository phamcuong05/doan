import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmWarehouseListComponent } from './dm-warehouse-list.component';
import { DmWarehouseDetailModule } from '../dm-warehouse-detail/dm-warehouse-detail.module';


@NgModule({
  declarations: [DmWarehouseListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmWarehouseListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmWarehouseDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmWarehouseListModule { }
