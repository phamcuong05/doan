import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmExpenseListComponent } from './dm-expense-list/dm-expense-list.component';
import { RouterModule } from '@angular/router';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { DmExpenseDetailModule } from './dm-expense-detail/dm-expense-detail.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';

@NgModule({
  declarations: [DmExpenseListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmExpenseListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmExpenseDetailModule,

  ],
})
export class DmExpenseModule {}
