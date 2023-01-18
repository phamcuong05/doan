import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmEmployeeSelectorComponent } from './dm-employee-selector.component';


@NgModule({
  declarations: [DmEmployeeSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmEmployeeSelectorComponent],
})
export class DmEmployeeSelectorModule { }
