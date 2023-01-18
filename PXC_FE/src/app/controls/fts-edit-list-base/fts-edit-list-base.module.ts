import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsEditListBaseComponent } from './fts-edit-list-base.component';
import { FtsGridModule } from '../fts-grid/fts-grid.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ToolBarModule } from '@progress/kendo-angular-toolbar';
import { FtsImportExcelModule } from '../fts-import-excel/fts-import-excel.module';
import { TooltipModule } from '@progress/kendo-angular-tooltip';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [FtsEditListBaseComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FtsGridModule,
    InputsModule,
    ToolBarModule,
    FtsImportExcelModule,
    TooltipModule
  ],
  exports: [FtsEditListBaseComponent],
})
export class FtsEditListBaseModule {}
