import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmDistrictDetailModule } from '../dm-district-detail/dm-district-detail.module';
import { DmDistrictListComponent } from './dm-district-list.component';


@NgModule({
  declarations: [DmDistrictListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmDistrictListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmDistrictDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmDistrictListModule { }
