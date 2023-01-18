import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmItemClassListComponent } from './dm-item-class-list.component';
import { DmItemClassDetailModule } from '../dm-item-class-detail/dm-item-class-detail.module';



@NgModule({
  declarations: [DmItemClassListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component:DmItemClassListComponent
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmItemClassDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class DmItemClassListModule { }
