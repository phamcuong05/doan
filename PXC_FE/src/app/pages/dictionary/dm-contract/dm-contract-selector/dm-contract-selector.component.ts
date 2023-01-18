import {
  Component,
  ElementRef,
  forwardRef,
  Input,
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
import { DmContractService } from 'src/app/model/dictionary/dm-contract/dm-contract-service';

@Component({
  selector: 'dm-contract-selector',
  templateUrl: './dm-contract-selector.component.html',
  styleUrls: ['./dm-contract-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmContractSelectorComponent),
    },
  ],
})
export class DmContractSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_CONTRACT';
  searchFields: string[] = ['CONTRACT_NO', 'CONTRACT_NAME'];
  @Input() tranId: string = '';
  get formTitle(): string {
    return this.resourceManager.ContractResource.MyResource.CONTRACT;
  }
  columns: FtsColumn[] = [
    { FieldId: 'CONTRACT_NO', Width: 250 },
    { FieldId: 'CONTRACT_NAME', Width: 250 },
    { FieldId: 'CONTRACT_DATE', ColumnType: 'date' } as FtsColumn,
    { FieldId: 'STATUS' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmContractService: DmContractService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmContractService,
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
  override ngOnDestroy(): void {
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
            Field: 'ORGANIZATION_ID',
            Operator: 'eq',
            Value: this.ftsMain.UserInfo.OrganizationID,
          },
        ],
        Logic: 'and',
      });

      if (this.tranId.length > 0) {
        param.FilterGroups.push({
          Filters: [
            {
              Field: 'TRAN_ID',
              Operator: 'eq',
              Value: this.tranId,
            },
          ],
          Logic: 'and',
        });
      }

      param.FilterFields = [
        'PR_KEY',
        'CONTRACT_NO',
        'CONTRACT_NAME',
        'CONTRACT_DATE',
      ];
    }
    return param;
  }
}
