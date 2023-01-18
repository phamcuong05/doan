import { Component, forwardRef, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmJobService } from 'src/app/model/dictionary/dm-job/dm-job.service';

@Component({
  selector: 'dm-job-detail',
  templateUrl: './dm-job-detail.component.html',
  styleUrls: ['./dm-job-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmJobDetailComponent),
    },
  ],
})
export class DmJobDetailComponent extends FTSDictBaseDetail {
  width: number = 500;

  constructor(
    private fb: FormBuilder,
    dmJobService: DmJobService,
    myInject: FtsDictBaseDetailInject
  ) { 
    super(dmJobService, myInject);
  }

  formGroup!: FormGroup;
  idField : string = 'JOB_ID';
  nameField : string = 'JOB_NAME';
  get formTitle(): string {
    return this.myInject.resourceManager.DmJobResource.MyResource
      .DM_JOB;
  }

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      JOB_ID: ['', [Validators.required, Validators.maxLength(20)]],
      JOB_NAME: [''],
      ACTIVE: [0],
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
