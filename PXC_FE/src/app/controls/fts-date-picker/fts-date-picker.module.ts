import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDatePickerComponent } from './fts-date-picker.component';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [FtsDatePickerComponent],
  imports: [CommonModule, DateInputsModule, FormsModule],
  exports: [FtsDatePickerComponent],
})
export class FtsDatePickerModule {}
