import { Injectable, ViewContainerRef } from '@angular/core';
import { NotificationService } from '@progress/kendo-angular-notification';
import { EventManager } from 'src/app/common/eventManager';
import { FTSMain } from 'src/app/base/ftsmain';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';
import { MaskLoadService } from '../mask-load/mask-load.service';
import { EditListBaseStore } from 'src/app/model/base/edit-list-base/edit-list-base-store';

@Injectable()
export class FtsEditListBaseInject {
  constructor(
    public resourceManager: ResourceManager,
    public editListBaseStore: EditListBaseStore,
    public maskLoad: MaskLoadService,
    public viewContainerRef: ViewContainerRef,
    public ftsDialogService: FtsDialogService,
    public notificationService: NotificationService,
    public FTSMain: FTSMain,
    public eventManager: EventManager
  ) {}
}