import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmCurrencySelectorComponent } from './dm-currency-selector.component';

@NgModule({
  declarations: [DmCurrencySelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmCurrencySelectorComponent],
})
export class DmCurrencySelectorModule {}
