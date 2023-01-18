import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmProvinceListComponent } from './dm-province-list.component';
import { DmProvinceDetailModule } from '../dm-province-detail/dm-province-detail.module';


@NgModule({
  declarations: [DmProvinceListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmProvinceListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmProvinceDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmProvinceListModule { }
