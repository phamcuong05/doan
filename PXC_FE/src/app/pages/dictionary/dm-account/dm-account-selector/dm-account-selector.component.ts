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
import { FilterGroup } from 'src/app/model/base/paging/filter';
import { PagingParam } from 'src/app/model/base/paging/paging-param';
import { DmAccountService } from 'src/app/model/dictionary/dm-account/dm-account-service';

@Component({
  selector: 'dm-account-selector',
  templateUrl: './dm-account-selector.component.html',
  styleUrls: ['./dm-account-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmAccountSelectorComponent),
    },
  ],
})
export class DmAccountSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_ACCOUNT';
  private _isParent: boolean = false;
  @Input() set isParent(v: boolean) {
    if (v != this._isParent) {
      this._isParent = v;
      this.txtSearchEl.nativeElement.value = '';
      this.loadData();
    }
  }

  @Input() defaultFilter: FilterGroup[] = [];

  get isParent(): boolean {
    return this._isParent;
  }

  searchFields: string[] = ['ACCOUNT_ID', 'ACCOUNT_NAME', 'CURRENCY_ID'];
  get formTitle(): string {
    return this.resourceManager.DmAccountResource.MyResource.DM_ACCOUNT;
  }
  columns: FtsColumn[] = [
    { FieldId: 'ACCOUNT_ID', Length: 20 },
    { FieldId: 'ACCOUNT_NAME' },
    { FieldId: 'CURRENCY_ID', Length: 10 },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmAccountService: DmAccountService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmAccountService,
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

      this.defaultFilter?.forEach((item) => {
        param.FilterGroups?.push({ ...item });
      });

      param.FilterGroups.push({
        Filters: [
          {
            Field: 'ACTIVE',
            Operator: 'eq',
            Value: 1,
          },
          // {
          //   Field: 'IS_PARENT',
          //   Operator: 'eq',
          //   Value: 1,
          // },
        ],
        Logic: 'and',
      });
      if (this.isParent) {
        param.FilterGroups.push({
          Filters: [
            {
              Field: 'IS_PARENT',
              Operator: 'eq',
              Value: 1,
            },
          ],
          Logic: 'and',
        });
      } else {
        param.FilterGroups.push({
          Filters: [
            {
              Field: 'IS_PARENT',
              Operator: 'eq',
              Value: 0,
            },
          ],
          Logic: 'and',
        });
      }

      param.FilterFields = ['ACCOUNT_ID', 'ACCOUNT_NAME', 'CURRENCY_ID'];
    }
    return param;
  }
}
