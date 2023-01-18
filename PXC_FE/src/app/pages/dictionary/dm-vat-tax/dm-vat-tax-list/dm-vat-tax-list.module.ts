import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmVatTaxListComponent } from './dm-vat-tax-list.component';
import { DmVatTaxDetailModule } from '../dm-vat-tax-detail/dm-vat-tax-detail.module';



@NgModule({
  declarations: [DmVatTaxListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmVatTaxListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmVatTaxDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmVatTaxListModule { }
