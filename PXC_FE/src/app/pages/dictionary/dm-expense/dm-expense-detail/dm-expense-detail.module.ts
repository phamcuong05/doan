import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmExpenseDetailComponent } from './dm-expense-detail.component';
import { DmExpenseClassSelectorModule } from '../../dm-expense-class/dm-expense-class-selector/dm-expense-class-selector.module';

@NgModule({
  declarations: [DmExpenseDetailComponent],
  imports: [
    CommonModule,
    FtsDictBaseDetailModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    DmExpenseClassSelectorModule,
  ],
  exports: [DmExpenseDetailComponent],
})
export class DmExpenseDetailModule {}
