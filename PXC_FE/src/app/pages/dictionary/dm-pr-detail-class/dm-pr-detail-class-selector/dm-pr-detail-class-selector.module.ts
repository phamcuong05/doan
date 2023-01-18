import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmPrDetailClassSelectorComponent } from './dm-pr-detail-class-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

/**
 * TAN.VU
 */
@NgModule({
  declarations: [DmPrDetailClassSelectorComponent],
  imports: [
    CommonModule,
    FtsGridModule
  ],
  exports:[DmPrDetailClassSelectorComponent]
})
export class DmPrDetailClassSelectorModule { }
