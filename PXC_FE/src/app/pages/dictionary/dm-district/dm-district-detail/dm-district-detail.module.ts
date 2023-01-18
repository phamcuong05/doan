import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';
import { DmDistrictDetailComponent } from './dm-district-detail.component';
import { FtsIdFieldDirective } from 'src/app/directive/fts-id-field.directive';
import { DmProvinceSelectorModule } from '../../dm-province/dm-province-selector/dm-province-selector.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';

@NgModule({
  declarations: [DmDistrictDetailComponent, FtsIdFieldDirective],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    DmProvinceSelectorModule,
    FtsDictBaseDetailModule
  ],
  exports:[DmDistrictDetailComponent, FtsIdFieldDirective],
  providers:[WindowService,WindowContainerService]
})
export class DmDistrictDetailModule { }
