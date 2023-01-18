import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmAgentDetailComponent } from './dm-agent-detail.component';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmProvinceSelectorModule } from '../../dm-province/dm-province-selector/dm-province-selector.module';
import { DmDistrictSelectorModule } from '../../dm-district/dm-district-selector/dm-district-selector.module';

@NgModule({
  declarations: [DmAgentDetailComponent],
  imports: [
    CommonModule,
    FtsDictBaseDetailModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    DmProvinceSelectorModule,
    DmDistrictSelectorModule,
  ],
  exports: [DmAgentDetailComponent],
})
export class DmAgentDetailModule {}
