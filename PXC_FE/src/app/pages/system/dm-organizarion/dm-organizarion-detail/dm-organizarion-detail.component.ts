import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmOrganizarionService } from 'src/app/model/system/dm-organizarion/dm-organizarion-service';

@Component({
  selector: 'dm-organizarion-detail',
  templateUrl: './dm-organizarion-detail.component.html',
  styleUrls: ['./dm-organizarion-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmOrganizarionDetailComponent),
    },
  ],
})
export class DmOrganizarionDetailComponent extends FTSDictBaseDetail {
  width: number = 600;

  constructor(
    private fb: FormBuilder,
    dmOrganizarionService: DmOrganizarionService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmOrganizarionService, myInject);
  }

  formGroup!: FormGroup;
  idField = 'ORGANIZATION_ID';
  nameField = 'ORGANIZATION_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmOrganizarionResource.MyResource
      .DM_ORGANIZATION;
  }
  initFormGroup(): void {
    this.formGroup = this.fb.group({
      ORGANIZATION_ID: ['', [Validators.required]],
      ORGANIZATION_NAME: ['', [Validators.required]],
      ORGANIZATION_NAME_SHORT: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

 
}
