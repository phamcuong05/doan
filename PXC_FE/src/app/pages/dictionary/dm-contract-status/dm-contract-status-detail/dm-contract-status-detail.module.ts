import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsDictBaseDetailModule } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail.module';
import { WindowContainerService, WindowService } from '@progress/kendo-angular-dialog';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { ReactiveFormsModule } from '@angular/forms';
import { DmContractStatusDetailComponent } from './dm-contract-status-detail.component';




@NgModule({
  declarations: [DmContractStatusDetailComponent],
  imports: [
    CommonModule,
    InputsModule,
    LabelModule,
    ReactiveFormsModule,
    FtsDictBaseDetailModule
  ],
  exports:[DmContractStatusDetailComponent],
  providers:[WindowService,WindowContainerService]
})
export class DmContractStatusDetailModule { }
