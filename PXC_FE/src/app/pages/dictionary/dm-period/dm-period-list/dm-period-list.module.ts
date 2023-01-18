import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmPeriodListComponent } from './dm-period-list.component';
import { DmPeriodDetailModule } from '../dm-period-detail/dm-period-detail.module';



@NgModule({
  declarations: [DmPeriodListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmPeriodListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmPeriodDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmPeriodListModule { }
