import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { DmExpenseClassDetailComponent } from './dm-expense-class-detail.component';

@NgModule({
  declarations: [
    DmExpenseClassDetailComponent,
  ],
  imports: [
    CommonModule,
    FtsDictBaseDetailModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
  ],
  exports: [DmExpenseClassDetailComponent],
})
export class DmExpenseClassDetailModule { }
