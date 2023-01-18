import {
  Directive,
  ElementRef,
  EventEmitter,
  HostListener,
  Input,
} from '@angular/core';

@Directive({
  selector: '[ftsGridEditTemplate]',
})
export class FtsGridEditTemplateDirective {
  @Input() fieldId!: any;
  @Input() eventEditCellKeydown!: EventEmitter<{
    event: KeyboardEvent;
    fieldId: string;
  }>;
  constructor(private el: ElementRef) {
  }
  @HostListener('keydown', ['$event'])
  handKeyDown(e: KeyboardEvent) {
    this.eventEditCellKeydown?.emit({ event: e, fieldId: this.fieldId });
  }
}