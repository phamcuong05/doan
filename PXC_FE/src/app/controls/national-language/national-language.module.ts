import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NationalLanguageComponent } from './national-language.component';

@NgModule({
  declarations: [NationalLanguageComponent],
  imports: [
    CommonModule,
  ],
  exports: [
    NationalLanguageComponent
  ]
})
export class NationalLanguageModule { }
