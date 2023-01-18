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
import { DmPrDetailClassService } from 'src/app/model/dictionary/dm-pr-detail-class/dm-pr-detail-class-service';

@Component({
  selector: 'dm-pr-detail-class-selector',
  templateUrl: './dm-pr-detail-class-selector.component.html',
  styleUrls: ['./dm-pr-detail-class-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmPrDetailClassSelectorComponent),
    },
  ],
})
export class DmPrDetailClassSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_PR_DETAIL_CLASS';
  searchFields: string[] = ['PR_DETAIL_CLASS_ID', 'PR_DETAIL_CLASS_NAME'];
  get formTitle(): string {
    return 'Danh mục nhóm đối tượng';
  }

  columns: FtsColumn[] = [
    { FieldId: 'PR_DETAIL_CLASS_ID', Text: 'Mã nhóm', Length: 20 },
    { FieldId: 'PR_DETAIL_CLASS_NAME', Text: 'Tên nhóm' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmPrDetailClassService: DmPrDetailClassService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmPrDetailClassService,
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

      param.FilterFields = ['PR_DETAIL_CLASS_ID', 'PR_DETAIL_CLASS_NAME'];
    }
    return param;
  }
}
