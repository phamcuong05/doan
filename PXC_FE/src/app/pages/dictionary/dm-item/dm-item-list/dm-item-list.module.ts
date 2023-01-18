import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmItemListComponent } from './dm-item-list.component';
import { DmItemDetailModule } from '../dm-item-detail/dm-item-detail.module';



@NgModule({
  declarations: [DmItemListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmItemListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmItemDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmItemListModule { }
