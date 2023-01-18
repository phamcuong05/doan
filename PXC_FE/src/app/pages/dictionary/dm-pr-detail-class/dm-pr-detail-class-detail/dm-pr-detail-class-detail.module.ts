import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmPrDetailClassDetailComponent } from './dm-pr-detail-class-detail.component';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

/**
 *
 */
@NgModule({
  declarations: [DmPrDetailClassDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    DropDownsModule
  ],
  exports:[DmPrDetailClassDetailComponent],
  providers:[WindowService,WindowContainerService]
})
export class DmPrDetailClassDetailModule { }
