import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmExpenseClassSelectorComponent } from './dm-expense-class-selector.component';

@NgModule({
  declarations: [DmExpenseClassSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmExpenseClassSelectorComponent],
})
export class DmExpenseClassSelectorModule {}
