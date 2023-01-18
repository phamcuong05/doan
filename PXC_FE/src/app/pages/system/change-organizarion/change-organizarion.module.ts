import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChangeOrganizarionComponent } from './change-organizarion.component';
import { RouterModule } from '@angular/router';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { FtsTextLookupModule } from 'src/app/controls/fts-text-lookup/fts-text-lookup.module';
import { DmOrganizationSelectorModule } from '../dm-organizarion/dm-organization-selector/dm-organization-selector.module';



@NgModule({
  declarations: [ChangeOrganizarionComponent],
  imports: [
    CommonModule,
    LabelModule,
    InputsModule,
    FormsModule,
    ReactiveFormsModule,
    FtsTextLookupModule,
    DmOrganizationSelectorModule,
    RouterModule.forChild([
      {
        path: '',
        component: ChangeOrganizarionComponent,
      },
    ]),
  ],
  providers: [WindowContainerService, WindowService]
})
export class ChangeOrganizarionModule { }
