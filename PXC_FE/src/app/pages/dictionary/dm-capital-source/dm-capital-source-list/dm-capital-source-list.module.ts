import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmCapitalSourceListComponent } from './dm-capital-source-list.component';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { DmCapitalSourceDetailModule } from '../dm-capital-source-detail/dm-capital-source-detail.module';

@NgModule({
  declarations: [DmCapitalSourceListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmCapitalSourceListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmCapitalSourceDetailModule,
  ],
})
export class DmCapitalSourceListModule {}
