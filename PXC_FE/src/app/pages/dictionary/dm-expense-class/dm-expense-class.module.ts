import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmExpenseClassListComponent } from './dm-expense-class-list/dm-expense-class-list.component';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { RouterModule } from '@angular/router';
import { DmExpenseClassDetailModule } from './dm-expense-class-detail/dm-expense-class-detail.module';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';

@NgModule({
  declarations: [DmExpenseClassListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmExpenseClassListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmExpenseClassDetailModule,
  ],
})
export class DmExpenseClassModule {}
