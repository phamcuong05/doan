import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmPrDetailClassDetailModule } from '../dm-pr-detail-class-detail/dm-pr-detail-class-detail.module';
import { DmPrDetailClassListComponent } from './dm-pr-detail-class-list.component';


@NgModule({
  declarations: [DmPrDetailClassListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmPrDetailClassListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmPrDetailClassDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmPrDetailClassListModule { }
