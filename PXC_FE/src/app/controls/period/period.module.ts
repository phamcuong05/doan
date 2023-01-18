import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PeriodComponent } from './period.component';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';

@NgModule({
  declarations: [PeriodComponent],
  imports: [
    CommonModule,
    LabelModule,
    InputsModule,
    DateInputsModule,
    DropDownsModule,
  ],
  exports: [PeriodComponent],
})
export class PeriodModule {}
