import { Directive, Input } from '@angular/core';
import { MyReference } from 'src/app/common/MyReference';
import { FtsTextLookupSelectorBase } from '../controls/fts-text-lookup-selector/fts-text-lookup-selector-base';

@Directive({
  selector: '[textLookupSelector]',
})
export class TextLookupSelectorDirective {
  @Input() detailComponent!:any;
  private _component!:FtsTextLookupSelectorBase;
  public get component(): FtsTextLookupSelectorBase{
    return this.detailComponent as FtsTextLookupSelectorBase || this._component;
  };
  constructor(componentRef: MyReference) {
    this._component = componentRef as FtsTextLookupSelectorBase;
  }
  
}
