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
import { DmSecurityClassService } from 'src/app/model/dictionary/dm-security-class/dm-security-class-service';

@Component({
  selector: 'dm-security-class-selector',
  templateUrl: './dm-security-class-selector.component.html',
  styleUrls: ['./dm-security-class-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmSecurityClassSelectorComponent),
    },
  ],
})
export class DmSecurityClassSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_SECURITY_CLASS';
  searchFields: string[] = ['SECURITY_CLASS_ID', 'SECURITY_CLASS_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmSecurityClassResource.MyResource.DM_SECURITY_CLASS;
  }
  columns: FtsColumn[] = [
    { FieldId: 'SECURITY_CLASS_ID', Length: 20 },
    { FieldId: 'SECURITY_CLASS_NAME' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmSecurityClassService: DmSecurityClassService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmSecurityClassService,
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

      param.FilterFields = ['SECURITY_CLASS_ID', 'SECURITY_CLASS_NAME'];
    }
    return param;
  }
}
