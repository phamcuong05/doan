import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmSecurityTypeSelectorComponent } from './dm-security-type-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmSecurityTypeSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmSecurityTypeSelectorComponent],
})
export class DmSecurityTypeSelectorModule {}
