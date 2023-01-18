import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsMultiSelectComponent } from './fts-multi-select.component';
import { FtsPopupMultiSelectorModule } from '../fts-popup-multi-selector/fts-popup-multi-selector.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [FtsMultiSelectComponent],
  imports: [CommonModule, FtsPopupMultiSelectorModule, FormsModule],
  exports: [FtsMultiSelectComponent],
})
export class FtsMultiSelectModule {}
