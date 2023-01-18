import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmItemClassSelectorComponent } from './dm-item-class-selector.component';

@NgModule({
  declarations: [DmItemClassSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmItemClassSelectorComponent],
})
export class DmItemClassSelectorModule {}
