import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmVatTaxSelectorComponent } from './dm-vat-tax-selector.component';



@NgModule({
  declarations: [DmVatTaxSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmVatTaxSelectorComponent],
})
export class DmVatTaxSelectorModule { }
