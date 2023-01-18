import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmCurrencyDetailComponent } from './dm-currency-detail.component';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';

/**
 *
 */
@NgModule({
  declarations: [DmCurrencyDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule
  ],
  exports: [DmCurrencyDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmCurrencyDetailModule {}
