import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { ListMawbService } from 'src/app/model/wh/list-mawb/list-mawb.service';

@Component({
  selector: 'list-mawb-detail',
  templateUrl: './list-mawb-detail.component.html',
  styleUrls: ['./list-mawb-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => ListMawbDetailComponent),
    },
  ],
})
export class ListMawbDetailComponent extends FTSDictBaseDetail {

  width: number = 680;

  constructor(
    private fb: FormBuilder,
    listMawbService: ListMawbService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(listMawbService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'MAWB_ID';
  nameField : string = 'MAWB_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.ListMawbResource.MyResource
      .LIST_MAWB;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      MAWB_ID: [''],
      MAWB_NAME: [''],
      DEPARTURE: [''],
      DESTINATION: [''],
      TOTAL_ORDER: [''],
      WEIGHT: [0],
      CREATE_DATE: [new Date()],
      USER_ID: [''],
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
