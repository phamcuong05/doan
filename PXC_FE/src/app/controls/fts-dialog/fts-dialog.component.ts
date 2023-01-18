import {
  Component,
  ElementRef,
  EventEmitter,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { DialogCloseResult, DialogRef } from '@progress/kendo-angular-dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';
import { ResourceManager } from 'src/app/common/resource-manager';
import { DiaLogConfig } from './fts-dialog.service';

@Component({
  selector: 'app-fts-dialog',
  templateUrl: './fts-dialog.component.html',
  styleUrls: ['./fts-dialog.component.scss'],
})
export class FtsDialogComponent implements OnInit, OnDestroy {
  @ViewChild('btnSubmit') btnSubmitRef!: ElementRef;
  @ViewChild('btnCancel') btnCancelRef!: ElementRef;
  @ViewChild('btnClose') btnCloseRef!: ElementRef;
  @ViewChild('btnSave') btnSaveRef!: ElementRef;

  config!: DiaLogConfig;
  type: 'alert' | 'confirm' | 'confirmSave' = 'alert';
  private cancelCloseEmit: boolean = false;

  public get GetIconClass(): string {
    switch (this.config?.icon) {
      case 'warning':
        return ' k-i-warning color-drange';
      case 'question':
        return ' k-i-question color-primary';
      case 'info':
        return ' k-i-information color-primary';
      default:
        return ' k-i-notification color-primary';
    }
  }

  id: string = commonFunction.newGuid();

  private onDestroy$: Subject<void> = new Subject<void>();

  constructor(
    private dialogRef: DialogRef,
    public resourceManager: ResourceManager,
    private eventManager: EventManager
  ) {}

  ngOnInit(): void {
    this.setTitle();
    this.setConfig();
    this.handleKeydown();
  }

  ngAfterViewInit(): void {
    this.dialogRef.result.pipe(takeUntil(this.onDestroy$)).subscribe((x) => {
      if (x instanceof DialogCloseResult && !this.cancelCloseEmit)
        this.dialogResult.next('close');
    });

    this.dialogRef.dialog.instance.onComponentKeydown = (e) => {};
  }

  ngOnDestroy(): void {
    this.eventManager.UnSubcriberKeyDown(this.id);
    this.onDestroy$.next();
  }

  setFocus() {
    if (this.type == 'confirm') {
      this.btnSubmitRef?.nativeElement?.focus();
    } else if (this.type === 'confirmSave') {
      this.btnSaveRef?.nativeElement?.focus();
    } else {
      this.btnCloseRef?.nativeElement?.focus();
    }
  }

  private setConfig() {
    this.dialogRef.dialog.instance.minWidth = this.config.minWidth || '';
    this.dialogRef.dialog.instance.maxWidth = this.config.maxWidth || '';
    this.dialogRef.dialog.instance.width = this.config.width || '';
    this.dialogRef.dialog.instance.minHeight = this.config.minHeight || '';
    this.dialogRef.dialog.instance.maxHeight = this.config.maxHeight || '';
    this.dialogRef.dialog.instance.height = this.config.height || '';
  }

  private setTitle() {
    if (this.type == 'alert')
      this.dialogRef.dialog.instance.title =
        this.resourceManager.CommonResource.MyResource?.Alert;
    else
      this.dialogRef.dialog.instance.title =
        this.resourceManager.CommonResource.MyResource?.Confirm;
  }

  dialogResult: EventEmitter<'close' | 'yes' | 'no'> = new EventEmitter();

  btnOk_Click($event: Event) {
    this.cancelCloseEmit = true;
    this.dialogRef.close();
    this.dialogResult.next('yes');
  }

  btnCancel_Click($event: Event) {
    this.cancelCloseEmit = true;
    this.dialogRef.close();
    this.dialogResult.next('no');
  }

  btnClose_Click($event: Event) {
    this.cancelCloseEmit = true;
    this.dialogRef.close();
    this.dialogResult.next('close');
  }

  handleKeydown() {
    const that = this;
    that.eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      const strKey = e.key.toUpperCase();
      //esc
      if (strKey == 'ESCAPE') {
        that.cancelCloseEmit = false;
        that.dialogRef.close();
      }
    });
  }
}
