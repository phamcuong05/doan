import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DashBoardComponent } from './dash-board.component';
import { FtsMultiSelectModule } from 'src/app/controls/fts-multi-select/fts-multi-select.module';
import { FtsNumerictextboxModule } from 'src/app/controls/fts-numerictextbox/fts-numerictextbox.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [DashBoardComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: DashBoardComponent,
      },
    ]),
    FtsMultiSelectModule,
    FtsNumerictextboxModule,
    FormsModule,
  ],
})
export class DashBoardModule {}
