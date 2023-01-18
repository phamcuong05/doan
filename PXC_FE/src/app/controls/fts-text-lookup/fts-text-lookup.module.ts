import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TextLookupSelectorDirective } from 'src/app/directive/fts-text-lookup-selector-directive';
import { FtsTextLookupComponent } from './fts-text-lookup.component';

@NgModule({
  declarations: [FtsTextLookupComponent, TextLookupSelectorDirective],
  imports: [CommonModule, FormsModule],
  exports: [FtsTextLookupComponent, TextLookupSelectorDirective],
})
export class FtsTextLookupModule {}
