import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';
import { DmAgentSelectorComponent } from './dm-agent-selector.component';

@NgModule({
  declarations: [DmAgentSelectorComponent],
  imports: [CommonModule,FtsGridModule],
  exports: [DmAgentSelectorComponent],
})
export class DmAgentSelectorModule {}
