import {
  ChangeDetectorRef,
  ElementRef,
  Injectable,
  ViewContainerRef,
} from '@angular/core';
import { NotificationService } from '@progress/kendo-angular-notification';
import { EventManager } from 'src/app/common/eventManager';
import { FTSMain } from 'src/app/base/ftsmain';
import { ResourceManager } from 'src/app/common/resource-manager';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { FtsDialogService } from '../../fts-dialog/fts-dialog.service';
import { MaskLoadService } from '../../mask-load/mask-load.service';

@Injectable()
export class FtsDictBaseDetailInject {
  constructor(
    public resourceManager: ResourceManager,
    public detailStore: DictBaseDetailStore,
    public maskLoad: MaskLoadService,
    public viewContainerRef: ViewContainerRef,
    public el: ElementRef,
    public ftsDialogService: FtsDialogService,
    public notificationService: NotificationService,
    public cdr: ChangeDetectorRef,
    public eventManager: EventManager,
    public FTSMain: FTSMain
  ) {}
}
