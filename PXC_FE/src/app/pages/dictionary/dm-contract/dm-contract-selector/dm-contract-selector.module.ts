import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmContractSelectorComponent } from './dm-contract-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmContractSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmContractSelectorComponent],
})
export class DmContractSelectorModule {}
