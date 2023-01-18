import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsWindowComponent } from './fts-window.component';
import { WindowModule } from '@progress/kendo-angular-dialog';

@NgModule({
  declarations: [FtsWindowComponent],
  imports: [CommonModule, WindowModule],
  exports: [FtsWindowComponent],
})
export class FtsWindowModule {}
