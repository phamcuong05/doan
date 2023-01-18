import { Component, OnInit } from '@angular/core';
import {
  ResourceManager,
} from 'src/app/common/resource-manager';

@Component({
  selector: 'app-page-not-found',
  templateUrl: './page-not-found.component.html',
  styleUrls: ['./page-not-found.component.scss'],
})
export class PageNotFoundComponent implements OnInit {
  constructor(public resourceManager: ResourceManager) {}

  ngOnInit(): void {}
}
