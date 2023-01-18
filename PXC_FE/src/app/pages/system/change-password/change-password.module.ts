import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChangePasswordComponent } from './change-password.component';
import { RouterModule } from '@angular/router';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';


@NgModule({
  declarations: [ChangePasswordComponent],
  imports: [
    CommonModule,
    FormsModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      {
        path: '',
        component: ChangePasswordComponent,
      },
    ]),
  ],
  providers: [WindowContainerService, WindowService]
})
export class ChangePasswordModule { }
