import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsDictBaseListModule } from 'src/app/controls/fts-dict-base/fts-dict-base-list/fts-dict-base-list.module';
import { DmExchangeRateDetailModule } from './dm-exchange-rate-detail/dm-exchange-rate-detail.module';
import { DmExchangeRateListComponent } from './dm-exchange-rate-list/dm-exchange-rate-list.component';

@NgModule({
  declarations: [DmExchangeRateListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmExchangeRateListComponent,
      },
    ]),
    DmExchangeRateDetailModule,
    FtsDictBaseDetailModule,
    FtsDictBaseListModule
  ],
})
export class DmExchangeRateModule {}
