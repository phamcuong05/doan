import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListDeliveryListComponent } from './list-delivery-list.component';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { ListDeliveryDetailModule } from '../list-delivery-detail/list-delivery-detail.module';



@NgModule({
  declarations: [
    ListDeliveryListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ListDeliveryListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    ListDeliveryDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class ListDeliveryListModule { }
