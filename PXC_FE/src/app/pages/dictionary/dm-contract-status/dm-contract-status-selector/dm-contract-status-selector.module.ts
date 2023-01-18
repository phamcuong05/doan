import { CommonModule } from '@angular/common';
import {
  Component,
  ElementRef,
  forwardRef,
  NgModule,
  ViewContainerRef,
} from '@angular/core';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmContractStatusSelectorComponent } from './dm-contract-status-selector.component';

@NgModule({
  declarations: [DmContractStatusSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmContractStatusSelectorComponent],
})
export class DmContractStatusSelectorModule { }
