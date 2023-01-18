import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmOrganizationSelectorComponent } from './dm-organization-selector.component';
import { FtsGridModule } from 'src/app/controls/fts-grid/fts-grid.module';

@NgModule({
  declarations: [DmOrganizationSelectorComponent],
  imports: [CommonModule,FtsGridModule],
  exports:[DmOrganizationSelectorComponent]
})
export class DmOrganizationSelectorModule { }
