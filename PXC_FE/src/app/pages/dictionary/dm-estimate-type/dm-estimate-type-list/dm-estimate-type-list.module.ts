import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmEstimateTypeListComponent } from './dm-estimate-type-list.component';
import { DmEstimateTypeDetailModule } from '../dm-estimate-type-detail/dm-estimate-type-detail.module';


@NgModule({
  declarations: [DmEstimateTypeListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmEstimateTypeListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmEstimateTypeDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmEstimateTypeListModule { }
