import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmJobSelectorComponent } from './dm-job-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';



@NgModule({
  declarations: [
    DmJobSelectorComponent
  ],
  imports: [
    CommonModule,
    FtsGridModule
  ],
  exports: [DmJobSelectorComponent],
})
export class DmJobSelectorModule { }
