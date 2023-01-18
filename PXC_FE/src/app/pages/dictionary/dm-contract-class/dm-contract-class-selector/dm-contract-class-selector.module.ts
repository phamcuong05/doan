import { CommonModule } from '@angular/common';
import {
  NgModule
} from '@angular/core';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmContractClassSelectorComponent } from './dm-contract-class-selector.component';

@NgModule({
  declarations: [DmContractClassSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmContractClassSelectorComponent],
})
export class DmContractClassSelectorModule { }
