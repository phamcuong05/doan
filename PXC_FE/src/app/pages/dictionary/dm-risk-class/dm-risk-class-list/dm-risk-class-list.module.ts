import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmRiskClassDetailModule } from '../dm-risk-class-detail/dm-risk-class-detail.module';
import { DmRiskClassListComponent } from './dm-risk-class-list.component';



@NgModule({
  declarations: [DmRiskClassListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmRiskClassListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmRiskClassDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmRiskClassListModule { }
