import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsPrintComponent } from './fts-print.component';
import { MaskLoadModule } from '../mask-load/mask-load.module';
import { FtsWindowModule } from '../fts-window/fts-window.module';
import { FtsPreviewModule } from '../fts-preview/fts-preview.module';

@NgModule({
  declarations: [FtsPrintComponent],
  imports: [CommonModule, FtsWindowModule, MaskLoadModule, FtsPreviewModule],
  exports: [FtsPrintComponent],
})
export class FtsPrintModule {}
