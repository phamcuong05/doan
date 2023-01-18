import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { DmAccountDetailModule } from '../dm-account-detail/dm-account-detail.module';
import { DmAccountListComponent } from './dm-account-list.component';

@NgModule({
  declarations: [DmAccountListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmAccountListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmAccountDetailModule,
  ],
})
export class DmAccountListModule { }
