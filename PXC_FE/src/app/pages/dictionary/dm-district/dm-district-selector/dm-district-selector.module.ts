import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmDistrictSelectorComponent } from './dm-district-selector.component';

@NgModule({
  declarations: [DmDistrictSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmDistrictSelectorComponent],
})
export class DmDistrictSelectorModule { }
