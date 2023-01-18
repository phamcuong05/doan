import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {
  WindowContainerService,
  WindowService
} from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { LabelModule } from '@progress/kendo-angular-label';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';
import { SysMenuDetailComponent } from './sys-menu-detail.component';

@NgModule({
  declarations: [SysMenuDetailComponent],
  imports: [
    CommonModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsNumerictextboxModule,
  ],
  exports: [SysMenuDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class SysMenuDetailModule {}
