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
import { DmDistrictService } from 'src/app/model/dictionary/dm-district/dm-district-service';

@Component({
  selector: 'dm-district-selector',
  templateUrl: './dm-district-selector.component.html',
  styleUrls: ['./dm-district-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmDistrictSelectorComponent),
    },
  ],
})
export class DmDistrictSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_DISTRICT';
  private _provinceId: string = '';
  @Input() set provinceId(v: string) {
    if (v != this._provinceId) {
      this._provinceId = v;
      this.txtSearchEl.nativeElement.value = '';
      this.loadData();
    }
  }

  get provinceId(): string {
    return this._provinceId;
  }

  searchFields: string[] = ['DISTRICT_ID', 'DISTRICT_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmDistrictResource.MyResource.DM_DISTRICT;
  }
  columns: FtsColumn[] = [
    { FieldId: 'DISTRICT_ID', Length: 20 },
    { FieldId: 'DISTRICT_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmDistrictService: DmDistrictService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmDistrictService,
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

      if (this.provinceId) {
        param.FilterGroups.push({
          Filters: [
            {
              Field: 'PROVINCE_ID',
              Operator: 'eq',
              Value: this.provinceId,
            },
          ],
          Logic: 'and',
        });
      }

      param.FilterFields = ['DISTRICT_ID', 'DISTRICT_NAME'];
    }
    return param;
  }
}
