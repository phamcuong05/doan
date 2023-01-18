import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmSecurityClassSelectorComponent } from './dm-security-class-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmSecurityClassSelectorComponent],
  imports: [CommonModule, FtsGridModule],
  exports: [DmSecurityClassSelectorComponent],
})
export class DmSecurityClassSelectorModule {}
