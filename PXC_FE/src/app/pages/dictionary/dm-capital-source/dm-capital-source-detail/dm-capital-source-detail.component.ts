import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmCapitalSourceService } from 'src/app/model/dictionary/dm-capital-source/dm-capital-source-service';

@Component({
  selector: 'dm-capital-source-detail',
  templateUrl: './dm-capital-source-detail.component.html',
  styleUrls: ['./dm-capital-source-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmCapitalSourceDetailComponent),
    },
  ],
})
export class DmCapitalSourceDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmCapitalSourceResource.MyResource
      .CAPITAL_SOURCE;
  }
  formGroup!: FormGroup;
  idField: string = 'CAPITAL_SOURCE_ID';
  nameField: string = 'CAPITAL_SOURCE_NAME';
  width: number = 450;

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      CAPITAL_SOURCE_ID: ['', [Validators.required, Validators.maxLength(20)]],
      CAPITAL_SOURCE_NAME: [
        '',
        [Validators.required, Validators.maxLength(100)],
      ],
      ACTIVE: [true],
    });
  }
  constructor(
    private fb: FormBuilder,
    dmCapitalSourceService: DmCapitalSourceService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmCapitalSourceService, myInject);
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
