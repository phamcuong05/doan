import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmVatPurchaseListComponent } from './dm-vat-purchase-list.component';
import { DmVatPurchaseDetailModule } from '../dm-vat-purchase-detail/dm-vat-purchase-detail.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { RouterModule } from '@angular/router';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';



@NgModule({
  declarations: [
    DmVatPurchaseListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: DmVatPurchaseListComponent,
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmVatPurchaseDetailModule,
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmVatPurchaseListModule { }
