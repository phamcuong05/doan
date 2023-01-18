import { Component, ContentChild, ContentChildren, OnInit, QueryList, TemplateRef } from '@angular/core';
import { FtsTabstripTabComponent } from './fts-tabstrip-tab/fts-tabstrip-tab.component';

@Component({
  selector: 'fts-tabstrip',
  templateUrl: './fts-tabstrip.component.html',
})
export class FtsTabstripComponent implements OnInit {
  @ContentChildren(FtsTabstripTabComponent, { descendants: true })
  tabComponents!: QueryList<FtsTabstripTabComponent>;

  constructor() {}

  ngOnInit(): void {}

  activeTab(tab: FtsTabstripTabComponent) {
    this.tabComponents.forEach((_tab) => {
      if (_tab != tab) {
        _tab.activeted = false;
      } else {
        _tab.activeted = true;
      }
    });
  }
}
