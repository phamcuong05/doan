import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmEstimateTypeSelectorComponent } from './dm-estimate-type-selector.component';



@NgModule({
  declarations: [DmEstimateTypeSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmEstimateTypeSelectorComponent],
})
export class DmEstimateTypeSelectorModule { }
