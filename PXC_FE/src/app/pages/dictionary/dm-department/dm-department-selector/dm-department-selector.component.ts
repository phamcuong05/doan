import {
  Component,
  ElementRef,
  forwardRef, ViewContainerRef
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
import { DmDepartmentService } from 'src/app/model/dictionary/dm-department/dm-department-service';

@Component({
  selector: 'dm-department-selector',
  templateUrl: './dm-department-selector.component.html',
  styleUrls: ['./dm-department-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmDepartmentSelectorComponent),
    },
  ],
})
export class DmDepartmentSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_DEPARTMENT';
  searchFields: string[] = ['DEPARTMENT_ID', 'DEPARTMENT_NAME'];
  get formTitle(): string {
    return this.resourceManager.DmDepartmentResource.MyResource.DM_DEPARTMENT;
  }

  columns: FtsColumn[] = [
    { FieldId: 'DEPARTMENT_ID', Length: 20 },
    { FieldId: 'DEPARTMENT_NAME'},
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmDepartmentService: DmDepartmentService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmDepartmentService,
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

      param.FilterFields = ['DEPARTMENT_ID', 'DEPARTMENT_NAME'];
    }
    return param;
  }
}
