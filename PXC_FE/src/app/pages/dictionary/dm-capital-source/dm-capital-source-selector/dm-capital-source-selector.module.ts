import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmCapitalSourceSelectorComponent } from './dm-capital-source-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmCapitalSourceSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmCapitalSourceSelectorComponent],
})
export class DmCapitalSourceSelectorModule {}
