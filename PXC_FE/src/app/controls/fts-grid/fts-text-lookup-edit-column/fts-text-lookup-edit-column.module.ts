import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FtsTextLookupEditColumnComponent } from './fts-text-lookup-edit-column.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FtsTextLookupModule } from '../../fts-text-lookup/fts-text-lookup.module';
import { TextLookupSelectorDirective } from 'src/app/directive/fts-text-lookup-selector-directive';

@NgModule({
  declarations: [FtsTextLookupEditColumnComponent],
  imports: [CommonModule, ReactiveFormsModule, FtsTextLookupModule],
  exports: [FtsTextLookupEditColumnComponent, TextLookupSelectorDirective],
})
export class FtsTextLookupEditColumnModule {}
