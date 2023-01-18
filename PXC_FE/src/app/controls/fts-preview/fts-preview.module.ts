import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsPreviewComponent } from './fts-preview.component';
import { FtsWindowModule } from '../fts-window/fts-window.module';
import { ToolBarModule } from '@progress/kendo-angular-toolbar';
import { SafePipe } from 'src/app/pipe/safe-pipe';
import { MaskLoadModule } from '../mask-load/mask-load.module';

@NgModule({
  declarations: [FtsPreviewComponent, SafePipe],
  imports: [CommonModule, FtsWindowModule, ToolBarModule, MaskLoadModule],
  exports: [FtsPreviewComponent],
})
export class FtsPreviewModule {}
