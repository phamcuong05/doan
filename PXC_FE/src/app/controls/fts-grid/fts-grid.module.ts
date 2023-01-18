import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {
  DateInputsModule,
  DatePickerModule
} from '@progress/kendo-angular-dateinputs';
import { ComboBoxModule } from '@progress/kendo-angular-dropdowns';
import { ExcelModule, GridModule } from '@progress/kendo-angular-grid';
import { L10N_PREFIX } from '@progress/kendo-angular-l10n';
import { ToolBarModule } from '@progress/kendo-angular-toolbar';
import { FtsGridEditTemplateDirective } from 'src/app/directive/fts-grid-edit-template-directive';
import { FtsNumerictextboxModule } from '../fts-numerictextbox/fts-numerictextbox.module';
import { FtsGridColumnComponent } from './fts-grid-column/fts-grid-column.component';
import { FtsGridComboFilterCellComponent } from './fts-grid-combo-filter-cell/fts-grid-combo-filter-cell.component';
import { FtsGridComponent } from './fts-grid.component';

@NgModule({
  declarations: [
    FtsGridComponent,
    FtsGridComboFilterCellComponent,
    FtsGridColumnComponent,
    FtsGridEditTemplateDirective,
  ],
  imports: [
    CommonModule,
    GridModule,
    DatePickerModule,
    ToolBarModule,
    ReactiveFormsModule,
    ComboBoxModule,
    ExcelModule,
    DateInputsModule,
    FtsNumerictextboxModule,
  ],
  providers: [{ provide: L10N_PREFIX, useValue: '' }],
  exports: [
    FtsGridComponent,
    FtsGridComboFilterCellComponent,
    FtsGridColumnComponent,
  ],
})
export class FtsGridModule {}
