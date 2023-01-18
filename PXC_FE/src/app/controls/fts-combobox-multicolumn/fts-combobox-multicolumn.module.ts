import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsComboboxMulticolumnComponent } from './fts-combobox-multicolumn.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [FtsComboboxMulticolumnComponent],
  imports: [
    CommonModule,
    DropDownsModule,
    ReactiveFormsModule,
  ],
  exports: [FtsComboboxMulticolumnComponent],
})
export class FtsComboboxMulticolumnModule {}
