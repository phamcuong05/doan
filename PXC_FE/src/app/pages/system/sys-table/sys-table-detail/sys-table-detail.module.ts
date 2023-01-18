import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { LabelModule } from '@progress/kendo-angular-label';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';
import { SysTableDetailComponent } from './sys-table-detail.component';

@NgModule({
  declarations: [SysTableDetailComponent],
  imports: [
    CommonModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsNumerictextboxModule,
  ],
  exports: [SysTableDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class SysTableDetailModule {}
