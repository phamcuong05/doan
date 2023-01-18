import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmWarehouseSelectorComponent } from './dm-warehouse-selector.component';



@NgModule({
  declarations: [DmWarehouseSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmWarehouseSelectorComponent],
})

export class DmWarehouseSelectorModule { }
