import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmSecurityClassDetailComponent } from './dm-security-class-detail.component';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';



@NgModule({
  declarations: [
    DmSecurityClassDetailComponent
  ],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule
  ],
  exports:[DmSecurityClassDetailComponent]
})
export class DmSecurityClassDetailModule { }
