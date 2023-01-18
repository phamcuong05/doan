import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { LabelModule } from '@progress/kendo-angular-label';
import { ToolBarModule } from '@progress/kendo-angular-toolbar';
import { TooltipModule } from '@progress/kendo-angular-tooltip';
import { FtsGridModule } from '../../fts-grid/fts-grid.module';
import { FtsWindowModule } from '../../fts-window/fts-window.module';
import { MaskLoadModule } from '../../mask-load/mask-load.module';
import { ImportExcelTemplateComponent } from './import-excel-template.component';

@NgModule({
  declarations: [ImportExcelTemplateComponent],
  imports: [
    CommonModule,
    LabelModule,
    ReactiveFormsModule,
    FtsGridModule,
    FtsWindowModule,
    ToolBarModule,
    TooltipModule,
    MaskLoadModule
  ],
  exports: [ImportExcelTemplateComponent],
})
export class ImportExcelTemplateModule {}
