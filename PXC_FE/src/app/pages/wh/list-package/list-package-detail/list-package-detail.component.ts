import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { ListPackageService } from 'src/app/model/wh/list-package/list-package.service';

@Component({
  selector: 'list-package-detail',
  templateUrl: './list-package-detail.component.html',
  styleUrls: ['./list-package-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => ListPackageDetailComponent),
    },
  ],
})
export class ListPackageDetailComponent extends FTSDictBaseDetail  {

  width: number = 700;

  constructor(
    private fb: FormBuilder,
    listPackageService: ListPackageService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(listPackageService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'PACKAGE_CODE';
  nameField : string = 'PACKAGE_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.ListPackageResource.MyResource
      .LIST_PACKAGE;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      PACKAGE_CODE: [''],
      PACKAGE_NAME: [''],
      TRACKING_CODE: [''],
      WEIGHT: [0],
      USER_ID: [''],
      ITEM: [''],
      CREATE_DATE: [''],
      MODIFY_DATE: [''],
      CONTAINER_ID: [''],
      WARE_HOUSE_ID: [''],
      MAWB_ID: [''],
      SERVICE_CHARGE_CODE: [''],
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
