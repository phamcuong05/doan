import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmItemOpListComponent } from './dm-item-op-list.component';
import { DmItemOpDetailModule } from '../dm-item-op-detail/dm-item-op-detail.module';



@NgModule({
  declarations: [DmItemOpListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmItemOpListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmItemOpDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmItemOpListModule { }
