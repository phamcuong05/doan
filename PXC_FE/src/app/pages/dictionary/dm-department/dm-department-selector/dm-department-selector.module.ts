import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmDepartmentSelectorComponent } from './dm-department-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmDepartmentSelectorComponent],
  imports: [CommonModule,FtsGridModule],
  exports: [DmDepartmentSelectorComponent],
})
export class DmDepartmentSelectorModule {}
