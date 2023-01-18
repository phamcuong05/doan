import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { DmVatTaxDetailComponent } from './dm-vat-tax-detail.component';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';

@NgModule({
  declarations: [DmVatTaxDetailComponent],
  imports: [
    CommonModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsNumerictextboxModule,
  ],
  exports: [DmVatTaxDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmVatTaxDetailModule {}
