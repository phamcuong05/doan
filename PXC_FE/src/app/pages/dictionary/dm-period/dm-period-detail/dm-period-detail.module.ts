import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import {
  WindowContainerService,
  WindowService,
} from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { DmPeriodDetailComponent } from './dm-period-detail.component';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmPeriodSelectorModule } from '../dm-period-selector/dm-period-selector.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';

@NgModule({
  declarations: [DmPeriodDetailComponent],
  imports: [
    CommonModule,
    LabelModule,
    DropDownsModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule,
    FtsTextLookupModule,
    DmPeriodSelectorModule,
    FtsNumerictextboxModule,
  ],
  exports: [DmPeriodDetailComponent],
  providers: [WindowService, WindowContainerService],
})
export class DmPeriodDetailModule {}
