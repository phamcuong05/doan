import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';
import { DmItemOpDetailComponent } from './dm-item-op-detail.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmItemOpSelectorModule } from '../dm-item-op-selector/dm-item-op-selector.module';

@NgModule({
  declarations: [DmItemOpDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsTextLookupModule,
    DmItemOpSelectorModule
  ],
  exports:[DmItemOpDetailComponent],
  providers:[WindowService,WindowContainerService]
})
export class DmItemOpDetailModule { }
