import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListContainerListComponent } from './list-container-list.component';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { PeriodModule } from 'src/app/controls/period/period.module';
import { ListContainerDetailModule } from '../list-container-detail/list-container-detail.module';
import { RouterModule } from '@angular/router';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';



@NgModule({
  declarations: [
    ListContainerListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ListContainerListComponent,
      },
    ]),
    PeriodModule,
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    ListContainerDetailModule
  ],
  exports:[RouterModule],
  providers:[WindowService, WindowContainerService],
})
export class ListContainerListModule { }
