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
import { DmPolicyService } from 'src/app/model/dictionary/dm-policy/dm-policy-service';

@Component({
  selector: 'dm-policy-selector',
  templateUrl: './dm-policy-selector.component.html',
  styleUrls: ['./dm-policy-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmPolicySelectorComponent),
    },
  ],
})
export class DmPolicySelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_POLICY';
  searchFields: string[] = ['POLICY_NO'];
  get formTitle(): string {
    return 'Số đơn BH';
  }
  columns: FtsColumn[] = [
    { FieldId: 'POLICY_NO', Text: 'Số đơn BH' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmPolicyService: DmPolicyService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmPolicyService,
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
      // param.FilterGroups.push({
      //   Filters: [
      //     {
      //       Field: 'ACTIVE',
      //       Operator: 'eq',
      //       Value: 1,
      //     },
      //   ],
      //   Logic: 'and',
      // });

      param.FilterFields = ['POLICY_NO'];
    }
    return param;
  }
}
