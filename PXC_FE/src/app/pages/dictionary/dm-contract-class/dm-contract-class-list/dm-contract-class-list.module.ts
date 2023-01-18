import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmContractClassListComponent } from './dm-contract-class-list.component';
import { DmContractClassDetailModule } from '../dm-contract-class-detail/dm-contract-class-detail.module';




@NgModule({
  declarations: [DmContractClassListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmContractClassListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmContractClassDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmContractClassListModule { }
