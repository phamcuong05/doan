import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'fts-tabstrip-tab',
  templateUrl: './fts-tabstrip-tab.component.html',
  encapsulation: ViewEncapsulation.None
})
export class FtsTabstripTabComponent implements OnInit {
  /**
   * Tiêu đề tab
   */
  @Input() tabTitle!:string;

  @Input() tabName!:string;

  @Input() activeted: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

}
