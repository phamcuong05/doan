import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmBankSelectorComponent } from './dm-bank-selector.component';

@NgModule({
  declarations: [DmBankSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmBankSelectorComponent],
})
export class DmBankSelectorModule {}
