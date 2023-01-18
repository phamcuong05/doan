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
import { DmExpenseService } from 'src/app/model/dictionary/dm-expense/dm-expense-service';

@Component({
  selector: 'dm-expense-selector',
  templateUrl: './dm-expense-selector.component.html',
  styleUrls: ['./dm-expense-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmExpenseSelectorComponent),
    },
  ],
})
export class DmExpenseSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_EXPENSE';
  searchFields: string[] = ['EXPENSE_ID', 'EXPENSE_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmExpenseResource.MyResource.DM_EXPENSE;
  }
  columns: FtsColumn[] = [
    { FieldId: 'EXPENSE_ID', Length: 20 },
    { FieldId: 'EXPENSE_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmExpenseService: DmExpenseService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmExpenseService,
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

      param.FilterFields = ['EXPENSE_ID', 'EXPENSE_NAME'];
    }
    return param;
  }
}
