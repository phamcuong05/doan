import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmProvinceSelectorComponent } from './dm-province-selector.component';



@NgModule({
  declarations: [DmProvinceSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmProvinceSelectorComponent],
})
export class DmProvinceSelectorModule { }
