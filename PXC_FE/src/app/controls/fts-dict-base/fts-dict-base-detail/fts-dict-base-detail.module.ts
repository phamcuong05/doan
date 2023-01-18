import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsDictBaseDetailComponent } from './fts-dict-base-detail.component';
import { DictBaseDetailDirective } from '../../../directive/fts-dict-base-detail-directive';
import { ToolBarModule } from '@progress/kendo-angular-toolbar';
import { FtsWindowModule } from '../../fts-window/fts-window.module';
import { MaskLoadModule } from '../../mask-load/mask-load.module';
import { TooltipModule } from '@progress/kendo-angular-tooltip';

@NgModule({
  declarations: [FtsDictBaseDetailComponent, DictBaseDetailDirective],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ToolBarModule,
    FtsWindowModule,
    MaskLoadModule,
    TooltipModule,
  ],
  exports: [FtsDictBaseDetailComponent, DictBaseDetailDirective],
})
export class FtsDictBaseDetailModule {}
