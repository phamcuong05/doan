import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsGridModule } from '../../fts-grid/fts-grid.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FtsDictBaseListComponent } from './fts-dict-base-list.component';
import { FtsDictBaseDetailModule } from '../fts-dict-base-detail/fts-dict-base-detail.module';
import { ToolBarModule } from '@progress/kendo-angular-toolbar';
import { FtsImportExcelModule } from '../../fts-import-excel/fts-import-excel.module';

@NgModule({
  declarations: [FtsDictBaseListComponent],
  imports: [
    CommonModule,
    FtsGridModule,
    InputsModule,
    FtsDictBaseDetailModule,
    ToolBarModule,
    FtsImportExcelModule
  ],
  exports:[FtsDictBaseListComponent]
})
export class FtsDictBaseListModule { }
