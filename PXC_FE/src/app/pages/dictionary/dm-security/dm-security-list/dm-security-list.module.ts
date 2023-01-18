import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { DmSecurityDetailModule } from '../dm-security-detail/dm-security-detail.module';
import { DmSecurityListComponent } from './dm-security-list.component';

@NgModule({
  declarations: [DmSecurityListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmSecurityListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmSecurityDetailModule,
  ],
})
export class DmSecurityTypeListModule {}
