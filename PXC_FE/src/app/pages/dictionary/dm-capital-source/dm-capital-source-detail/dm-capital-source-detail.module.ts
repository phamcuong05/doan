import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { DmCapitalSourceDetailComponent } from './dm-capital-source-detail.component';



@NgModule({
  declarations: [
    DmCapitalSourceDetailComponent
  ],
  imports: [
    CommonModule,
    FtsDictBaseDetailModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
  ],
  exports:[DmCapitalSourceDetailComponent]
})
export class DmCapitalSourceDetailModule { }
