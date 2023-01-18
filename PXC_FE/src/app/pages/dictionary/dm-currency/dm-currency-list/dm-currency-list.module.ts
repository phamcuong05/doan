import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { DmCurrencyDetailModule } from '../dm-currency-detail/dm-currency-detail.module';
import { DmCurrencyListComponent } from './dm-currency-list.component';

@NgModule({
  declarations: [DmCurrencyListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmCurrencyListComponent,
      },
    ]),
    FtsDictBaseListModule,
    FtsDictBaseDetailModule,
    DmCurrencyDetailModule,
  ],
})
export class DmCurrencyListModule {}
