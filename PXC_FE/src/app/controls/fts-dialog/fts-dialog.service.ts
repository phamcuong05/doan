import { Injectable } from '@angular/core';
import { DialogService } from '@progress/kendo-angular-dialog';
import { FtsDialogComponent } from './fts-dialog.component';

export interface DiaLogConfig {
  width?: number;
  height?: number;
  minWidth?: number;
  maxWidth?: number;
  minHeight?: number;
  maxHeight?: number;
  content?: string;
  icon: 'info' | 'warning' | 'question' | 'notification';
}

export class DiaLog {
  _dialogService: DialogService;
  _type: 'alert' | 'confirm' | 'confirmSave';
  constructor(
    dialogService: DialogService,
    type: 'alert' | 'confirm' | 'confirmSave'
  ) {
    this._dialogService = dialogService;
    this._type = type;
  }

  show(config: DiaLogConfig) {
    const dialog = this._dialogService.open({
      content: FtsDialogComponent,
    });
    let comp: FtsDialogComponent = dialog.content.instance;
    comp.config = config;
    comp.type = this._type;
    comp.setFocus();
    return comp.dialogResult;
  }
}

@Injectable({
  providedIn: 'root',
})
export class FtsDialogService {
  constructor(private dialogService: DialogService) {}
  alert: DiaLog = new DiaLog(this.dialogService, 'alert');
  confirm: DiaLog = new DiaLog(this.dialogService, 'confirm');
  confirmSave: DiaLog = new DiaLog(this.dialogService, 'confirmSave');
}
