import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListMawbListComponent } from './list-mawb-list.component';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { RouterModule } from '@angular/router';
import { ListMawbDetailModule } from '../list-mawb-detail/list-mawb-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';



@NgModule({
  declarations: [
    ListMawbListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ListMawbListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    ListMawbDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class ListMawbListModule { }
