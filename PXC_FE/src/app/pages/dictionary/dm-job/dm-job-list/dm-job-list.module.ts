import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmJobListComponent } from './dm-job-list.component';
import { DmJobDetailModule } from '../dm-job-detail/dm-job-detail.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    DmJobListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path:'',
      component: DmJobListComponent,
    }]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmJobDetailModule,
  ]
})
export class DmJobListModule { }
