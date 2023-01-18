import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FtsNumerictextboxComponent } from './fts-numerictextbox.component';

@NgModule({
  declarations: [FtsNumerictextboxComponent],
  imports: [CommonModule, FormsModule],
  exports: [FtsNumerictextboxComponent],
})
export class FtsNumerictextboxModule {}
