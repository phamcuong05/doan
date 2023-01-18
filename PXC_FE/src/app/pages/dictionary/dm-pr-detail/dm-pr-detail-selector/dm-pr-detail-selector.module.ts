import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmPrDetailSelectorComponent } from './dm-pr-detail-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmPrDetailSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmPrDetailSelectorComponent],
})
export class DmPrDetailSelectorModule { }
