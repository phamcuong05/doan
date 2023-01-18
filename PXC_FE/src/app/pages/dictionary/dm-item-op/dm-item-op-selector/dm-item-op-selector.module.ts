import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmItemOpSelectorComponent } from './dm-item-op-selector.component';


@NgModule({
  declarations: [DmItemOpSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmItemOpSelectorComponent],
})
export class DmItemOpSelectorModule { }
