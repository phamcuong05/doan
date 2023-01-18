import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListOrderListComponent } from './list-order-list.component';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { ListOrderDetailModule } from '../list-order-detail/list-order-detail.module';
import { RouterModule } from '@angular/router';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';



@NgModule({
  declarations: [
    ListOrderListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ListOrderListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    ListOrderDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class ListOrderListModule { }
