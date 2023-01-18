import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmVatPurchaseSelectorComponent } from './dm-vat-purchase-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmVatPurchaseSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmVatPurchaseSelectorComponent],
})
export class DmVatPurchaseSelectorModule {}
