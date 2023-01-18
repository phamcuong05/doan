import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmExpenseClassService } from 'src/app/model/dictionary/dm-expense-class/dm-expense-class-service';

@Component({
  selector: 'dm-expense-class-detail',
  templateUrl: './dm-expense-class-detail.component.html',
  styleUrls: ['./dm-expense-class-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmExpenseClassDetailComponent),
    },
  ],
})
export class DmExpenseClassDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return 'Danh mục nhóm chi phí';
  }
  formGroup!: FormGroup;
  idField: string = 'EXPENSE_CLASS_ID';
  nameField: string = 'EXPENSE_CLASS_NAME';
  width: number = 400;

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      EXPENSE_CLASS_ID: ['', [Validators.required, Validators.maxLength(20)]],
      EXPENSE_CLASS_NAME: [
        '',
        [Validators.required, Validators.maxLength(100)],
      ],
      ACTIVE: [true],
    });
  }

  constructor(
    private fb: FormBuilder,
    dmExpenseClassService: DmExpenseClassService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmExpenseClassService, myInject);
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
