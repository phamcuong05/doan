import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmUnitSelectorComponent } from './dm-unit-selector.component';

@NgModule({
  declarations: [DmUnitSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmUnitSelectorComponent],
})
export class DmUnitSelectorModule { }
