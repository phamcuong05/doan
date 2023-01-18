import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmAgentListComponent } from './dm-agent-list.component';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { DmAgentDetailModule } from '../dm-agent-detail/dm-agent-detail.module';

@NgModule({
  declarations: [DmAgentListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmAgentListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmAgentDetailModule,
  ],
})
export class DmAgentListModule {}
