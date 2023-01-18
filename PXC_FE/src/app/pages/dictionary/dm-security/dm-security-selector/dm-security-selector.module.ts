import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmSecuritySelectorComponent } from './dm-security-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmSecuritySelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmSecuritySelectorComponent],
})
export class DmSecuritySelectorModule {}
