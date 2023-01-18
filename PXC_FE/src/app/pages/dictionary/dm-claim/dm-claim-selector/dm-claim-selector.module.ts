import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmClaimSelectorComponent } from './dm-claim-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { PeriodModule } from 'src/app/controls/period/period.module';

@NgModule({
  declarations: [DmClaimSelectorComponent],
  imports: [CommonModule, FtsGridModule, PeriodModule],
  exports: [DmClaimSelectorComponent],
})
export class DmClaimSelectorModule {}
