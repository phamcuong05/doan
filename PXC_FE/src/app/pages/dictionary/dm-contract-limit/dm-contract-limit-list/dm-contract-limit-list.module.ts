import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmContractLimitListComponent } from './dm-contract-limit-list.component';
import { DmContractLimitDetailModule } from '../dm-contract-limit-detail/dm-contract-limit-detail.module';




@NgModule({
  declarations: [DmContractLimitListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmContractLimitListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmContractLimitDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmContractLimitListModule { }
