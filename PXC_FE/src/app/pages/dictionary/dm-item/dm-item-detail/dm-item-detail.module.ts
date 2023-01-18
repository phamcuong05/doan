import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmItemClassSelectorModule } from '../../dm-item-class/dm-item-class-selector/dm-item-class-selector.module';
import { DmUnitSelectorModule } from '../../dm-unit/dm-unit-selector/dm-unit-selector.module';
import { DmItemDetailComponent } from './dm-item-detail.component';

@NgModule({
  declarations: [DmItemDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    FtsDictBaseDetailModule,
    DmItemClassSelectorModule,
    DmUnitSelectorModule
  ],
  exports: [DmItemDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmItemDetailModule {}
