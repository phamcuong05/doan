import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ResourceManager } from 'src/app/common/resource-manager';

@Component({
  selector: 'app-dash-board',
  templateUrl: './dash-board.component.html',
  styleUrls: ['./dash-board.component.scss'],
})
export class DashBoardComponent implements OnInit {
  now = new Date();
  constructor(
    public resourceManager: ResourceManager,
  ) {}

  ngOnInit(): void {}

}
