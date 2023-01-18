import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { DmSecurityTypeDetailModule } from '../dm-security-type-detail/dm-security-type-detail.module';
import { DmSecurityTypeListComponent } from './dm-security-type-list.component';

@NgModule({
  declarations: [DmSecurityTypeListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmSecurityTypeListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmSecurityTypeDetailModule,
  ],
})
export class DmSecurityTypeListModule {}
