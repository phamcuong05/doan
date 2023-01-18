import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDialogComponent } from './fts-dialog.component';
import { DialogService } from '@progress/kendo-angular-dialog';

@NgModule({
  declarations: [FtsDialogComponent],
  imports: [CommonModule],
  exports: [FtsDialogComponent],
  providers: [DialogService]
})
export class FtsDialogModule {}
