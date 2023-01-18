import {
  Component,
  ContentChild,
  Input,
  OnDestroy,
  OnInit,
  TemplateRef,
  ViewChild
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { TextLookupSelectorDirective } from 'src/app/directive/fts-text-lookup-selector-directive';
import { FtsColumn } from '../fts-grid.component';

@Component({
  selector: 'fts-text-lookup-edit-column',
  templateUrl: './fts-text-lookup-edit-column.component.html',
})
export class FtsTextLookupEditColumnComponent implements OnInit, OnDestroy {
  @Input() ftsColumn!: FtsColumn;

  @Input() selectionChange = (state: {
    item: any;
    form: FormGroup;
    currentRow: any;
  }) => {};

  @ContentChild(TextLookupSelectorDirective)
  selectorDirective!: TextLookupSelectorDirective;

  @ViewChild('editorTemplate', { static: true })
  editorTemplate!: TemplateRef<any>;

  constructor() {}
  ngOnDestroy(): void {}
  ngAfterViewInit(): void {
    if (this.ftsColumn && this.editorTemplate) {
      this.ftsColumn.EditTemplateRef = this.editorTemplate;
    }
  }
  ngOnInit(): void {}
}
