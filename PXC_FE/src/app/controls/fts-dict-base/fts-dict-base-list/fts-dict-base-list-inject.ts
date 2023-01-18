import { Injectable, ViewContainerRef } from '@angular/core';
import { WindowService } from '@progress/kendo-angular-dialog';
import { NotificationService } from '@progress/kendo-angular-notification';
import { EventManager } from 'src/app/common/eventManager';
import { FTSMain } from 'src/app/base/ftsmain';
import { ResourceManager } from 'src/app/common/resource-manager';
import { DictBaseListStore } from 'src/app/model/base/dict-base/dict-base-list-store';
import { FtsDialogService } from '../../fts-dialog/fts-dialog.service';
import { MaskLoadService } from '../../mask-load/mask-load.service';

@Injectable()
export class FtsDictBaseListInject {
  constructor(
    public resourceManager: ResourceManager,
    public dictBaseListStore: DictBaseListStore,
    public maskLoad: MaskLoadService,
    public viewContainerRef: ViewContainerRef,
    public windowService: WindowService,
    public ftsDialogService: FtsDialogService,
    public notificationService: NotificationService,
    public FTSMain: FTSMain,
    public eventManager: EventManager
  ) {}
}
