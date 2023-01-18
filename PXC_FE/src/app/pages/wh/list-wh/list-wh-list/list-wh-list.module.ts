import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListWhListComponent } from './list-wh-list.component';
import { ListWhDetailModule } from '../list-wh-detail/list-wh-detail.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { RouterModule } from '@angular/router';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';



@NgModule({
  declarations: [
    ListWhListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ListWhListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    ListWhDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class ListWhListModule { }
