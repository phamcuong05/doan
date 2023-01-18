import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmJobDetailComponent } from './dm-job-detail.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { DmJobSelectorModule } from '../dm-job-selector/dm-job-selector.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';



@NgModule({
  declarations: [
    DmJobDetailComponent
  ],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    DmJobSelectorModule,
    FtsTextLookupModule
  ],
  exports: [DmJobDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmJobDetailModule { }
