import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmAccountSelectorComponent } from './dm-account-selector.component';
import { FtsWindowModule } from 'src/app/controls/fts-window/fts-window.module';

@NgModule({
  declarations: [DmAccountSelectorComponent],
  imports: [CommonModule, FtsGridModule, FtsWindowModule],
  exports: [DmAccountSelectorComponent],
})
export class DmAccountSelectorModule {}
