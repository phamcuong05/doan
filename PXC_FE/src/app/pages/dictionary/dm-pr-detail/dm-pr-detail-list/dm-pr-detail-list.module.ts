import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { DmPrDetailDetailModule } from '../dm-pr-detail-detail/dm-pr-detail-detail.module';
import { DmPrDetailListComponent } from './dm-pr-detail-list.component';

@NgModule({
  declarations: [DmPrDetailListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmPrDetailListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmPrDetailDetailModule,
  ],
})
export class DmPrDetailListModule {}
