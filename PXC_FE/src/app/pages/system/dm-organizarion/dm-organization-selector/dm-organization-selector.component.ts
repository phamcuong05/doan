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
import { DmOrganizarionService } from 'src/app/model/system/dm-organizarion/dm-organizarion-service';

@Component({
  selector: 'dm-organization-selector',
  templateUrl: './dm-organization-selector.component.html',
  styleUrls: ['./dm-organization-selector.component.scss'],
  providers: [
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmOrganizationSelectorComponent),
    },
  ],
})
export class DmOrganizationSelectorComponent extends FtsTextLookupSelectorBase {
  tableName: string = 'DM_ORGANIZATION';
  searchFields: string[] = ['ORGANIZATION_ID', 'ORGANIZATION_NAME'];

  get formTitle(): string {
    return this.resourceManager.DmOrganizarionResource.MyResource
      .DM_ORGANIZATION;
  }

  columns: FtsColumn[] = [
    { FieldId: 'ORGANIZATION_ID', Text: 'Mã đơn vị', Length: 20 },
    { FieldId: 'ORGANIZATION_NAME', Text: 'Tên đơn vị' },
  ] as FtsColumn[];

  constructor(
    resourceManage: ResourceManager,
    dmOrganizarionService: DmOrganizarionService,
    maskLoad: MaskLoadService,
    viewContainerRef: ViewContainerRef,
    el: ElementRef,
    ftsDialogService: FtsDialogService,
    eventManager: EventManager,
    ftsMain: FTSMain
  ) {
    super(
      resourceManage,
      dmOrganizarionService,
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
}
