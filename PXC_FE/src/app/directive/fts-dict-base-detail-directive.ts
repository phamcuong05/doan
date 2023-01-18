import { Directive, Input } from '@angular/core';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from '../controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';

@Directive({
  selector: '[dictBaseDetail]',
})
export class DictBaseDetailDirective {
  @Input() detailComponent!:any;
  private _component!:FTSDictBaseDetail;
  public get component(): FTSDictBaseDetail{
    return this.detailComponent as FTSDictBaseDetail || this._component;
  };
  constructor(componentRef: MyReference) {
    this._component = componentRef as FTSDictBaseDetail;
  }
}