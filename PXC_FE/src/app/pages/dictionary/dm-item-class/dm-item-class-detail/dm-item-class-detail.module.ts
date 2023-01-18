import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';
import { DmItemClassDetailComponent } from './dm-item-class-detail.component';
import { DmAccountSelectorModule } from '../../dm-account/dm-account-selector/dm-account-selector.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';

/**
 * 
 */
@NgModule({
  declarations: [DmItemClassDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    FtsTextLookupModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    DmAccountSelectorModule
  ],
  exports:[DmItemClassDetailComponent],
  providers:[WindowService,WindowContainerService]
})
export class DmItemClassDetailModule { }