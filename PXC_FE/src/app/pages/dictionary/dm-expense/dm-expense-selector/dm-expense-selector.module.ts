import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import {  DmExpenseSelectorComponent } from './dm-expense-selector.component';

@NgModule({
  declarations: [DmExpenseSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmExpenseSelectorComponent],
})
export class DmExpenseSelectorModule {}
