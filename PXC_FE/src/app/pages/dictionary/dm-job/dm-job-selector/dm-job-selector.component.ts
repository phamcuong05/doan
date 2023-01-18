import {
  Component,
  ElementRef,
  forwardRef,
  ViewContainerRef,
} from '@angular/core';
import { FTSMain } from 'src/app/base/ftsmain';
import { EventManager } from 'src/app/common/eventManager';
import { MyReference } from 'src/app/common/MyReference';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FtsDialogService } from 'src/app/controls/fts-dialog/fts-dialog.service';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import { FtsTextLookupSelectorBase } from 'src/app/controls/fts-text-lookup-selector/fts-text-lookup-selector-base';
import { MaskLoadService } from 'src/app/controls/mask-load/mask-load.service';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmJobService } from 'src/app/model/dictionary/dm-job/dm-job.service';

@Component({
  selector: 'dm-job-selector',
  templateUrl: './dm-job-selector.component.html',
  styleUrls: ['./dm-job-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmJobSelectorComponent),
    },
  ],
})
export class DmJobSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_JOB';
  searchFields: string[] = [
    'JOB_ID',
    'JOB_NAME',
  ];
  get formTitle(): string {
    return this.resourceManager.DmJobResource.MyResource.DM_JOB;
  }
  columns: FtsColumn[] = [
    { FieldId: 'JOB_ID', Length: 20 } as FtsColumn,
    { FieldId: 'JOB_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmJobService: DmJobService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmJobService,
      maskLoad,
      viewContainerRef,
      el,
      ftsDialogService,
      eventManager,
      ftsMain
    );
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  override setParamBeforLoad(param: PagingParam): PagingParam {
    if (param) {
      if (!param.FilterGroups) {
        param.FilterGroups = [];
      }
      param.FilterGroups.push({
        Filters: [
          {
            Field: 'ACTIVE',
            Operator: 'eq',
            Value: 1,
          },
        ],
        Logic: 'and',
      });

      param.FilterFields = [
        'JOB_ID',
        'JOB_NAME',
      ];
    }
    return param;
  }
}
