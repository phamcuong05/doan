import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DmAdvanceLimitEditListComponent } from './dm-advance-limit-edit-list.component';
import { RouterModule } from '@angular/router';
import { FtsEditListBaseModule } from 'src/app/controls/fts-edit-list-base/fts-edit-list-base.module';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmOrganizationSelectorModule } from '../../system/dm-organizarion/dm-organization-selector/dm-organization-selector.module';
import { DmAccountSelectorModule } from '../dm-account/dm-account-selector/dm-account-selector.module';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsTextLookupEditColumnModule } from 'src/app/controls/fts-grid/fts-text-lookup-edit-column/fts-text-lookup-edit-column.module';

@NgModule({
  declarations: [DmAdvanceLimitEditListComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DmAdvanceLimitEditListComponent,
      },
    ]),
    ReactiveFormsModule,
    FtsEditListBaseModule,
    FtsTextLookupModule,
    FtsTextLookupEditColumnModule,
    DmOrganizationSelectorModule,
    DmAccountSelectorModule
  ],
})
export class DmAdvanceLimitEditListModule {}
