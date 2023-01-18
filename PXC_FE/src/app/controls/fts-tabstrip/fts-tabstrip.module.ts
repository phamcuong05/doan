import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsTabstripTabComponent } from './fts-tabstrip-tab/fts-tabstrip-tab.component';
import { FtsTabstripComponent } from './fts-tabstrip.component';

@NgModule({
  declarations: [FtsTabstripComponent, FtsTabstripTabComponent],
  imports: [CommonModule],
  exports: [FtsTabstripComponent, FtsTabstripTabComponent],
})
export class FtsTabstripModule {}
