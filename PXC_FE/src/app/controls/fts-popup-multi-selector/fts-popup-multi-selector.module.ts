import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsPopupMultiSelectorComponent } from './fts-popup-multi-selector.component';
import { FtsWindowModule } from '../fts-window/fts-window.module';
import { FtsGridModule } from '../fts-grid/fts-grid.module';
import { MaskLoadModule } from '../mask-load/mask-load.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [FtsPopupMultiSelectorComponent],
  imports: [
    CommonModule,
    FtsWindowModule,
    FtsGridModule,
    MaskLoadModule,
    FormsModule,
  ],
  exports: [FtsPopupMultiSelectorComponent],
})
export class FtsPopupMultiSelectorModule {}
