import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmItemSelectorComponent } from './dm-item-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';



@NgModule({
  declarations: [DmItemSelectorComponent],
  imports: [
    CommonModule,
    FtsGridModule
  ],
  exports:[DmItemSelectorComponent]
})
export class DmItemSelectorModule { }
