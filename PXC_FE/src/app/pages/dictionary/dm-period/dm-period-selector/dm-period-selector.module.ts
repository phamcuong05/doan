import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmPeriodSelectorComponent } from './dm-period-selector.component';


@NgModule({
  declarations: [DmPeriodSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmPeriodSelectorComponent],
})
export class DmPeriodSelectorModule { }
