import { Directive, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[ftsIdField]'
})
export class FtsIdFieldDirective {

  //lastValue!: string;

  constructor(public ref: ElementRef) {
  }

  @HostListener('input', ['$event']) onInput(event:any) {
    this.ref.nativeElement.value = event.target.value.toUpperCase();
  }

}
