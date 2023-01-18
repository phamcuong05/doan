import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmRiskClassSelectorComponent } from './dm-risk-class-selector.component';

@NgModule({
  declarations: [DmRiskClassSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmRiskClassSelectorComponent],
})
export class DmRiskClassSelectorModule { }
