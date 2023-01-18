import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsImportExcelComponent } from './fts-import-excel.component';
import { FtsWindowModule } from '../fts-window/fts-window.module';
import { ToolBarModule } from '@progress/kendo-angular-toolbar';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaskLoadModule } from '../mask-load/mask-load.module';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsGridModule } from '../fts-grid/fts-grid.module';
import { ImportExcelTemplateModule } from './import-excel-template/import-excel-template.module';



@NgModule({
  declarations: [FtsImportExcelComponent],
  imports: [
    CommonModule,
    FtsWindowModule,
    FtsGridModule,
    ToolBarModule,
    InputsModule,
    FormsModule,
    DropDownsModule,
    ReactiveFormsModule,
    MaskLoadModule,
    ImportExcelTemplateModule
  ],
  exports:[FtsImportExcelComponent]
})
export class FtsImportExcelModule { }
