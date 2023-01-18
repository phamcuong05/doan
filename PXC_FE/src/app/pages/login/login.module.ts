import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { TooltipModule } from '@progress/kendo-angular-tooltip';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';
import { NationalLanguageModule } from 'src/app/controls/national-language/national-language.module';
import { AuthEffects } from 'src/app/model/login/effec';
import { LoginComponent } from './login.component';

@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FtsNumerictextboxModule,
    RouterModule.forChild([
      {
        path: '',
        component: LoginComponent,
      },
    ]),
    EffectsModule.forFeature([AuthEffects]),
    NationalLanguageModule,
    TooltipModule,
  ],
})
export class LoginModule {}
