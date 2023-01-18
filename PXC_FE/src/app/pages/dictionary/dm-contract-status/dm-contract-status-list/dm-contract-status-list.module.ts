import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmContractStatusListComponent } from './dm-contract-status-list.component';
import { DmContractStatusDetailModule } from '../dm-contract-status-detail/dm-contract-status-detail.module';




@NgModule({
  declarations: [DmContractStatusListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmContractStatusListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmContractStatusDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmContractStatusListModule { }
