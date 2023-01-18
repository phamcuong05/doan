import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmPolicySelectorComponent } from './dm-policy-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmPolicySelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmPolicySelectorComponent],
})
export class DmPolicySelectorModule {}
