import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmCashbankLimitListComponent } from './dm-cashbank-limit-list.component';
import { DmCashbankLimitDetailModule } from '../dm-cashbank-limit-detail/dm-cashbank-limit-detail.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { RouterModule } from '@angular/router';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';



@NgModule({
  declarations: [
    DmCashbankLimitListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: DmCashbankLimitListComponent,
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmCashbankLimitDetailModule,
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmCashbankLimitListModule { }
