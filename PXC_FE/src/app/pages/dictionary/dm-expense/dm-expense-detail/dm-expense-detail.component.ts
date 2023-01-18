import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MyReference } from 'src/app/common/MyReference';
import { FTSDictBaseDetail } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
import { FtsDictBaseDetailInject } from 'src/app/controls/fts-dict-base/fts-dict-base-detail/fts-dict-base-detail-inject';
import { DictBaseDetailStore } from 'src/app/model/base/dict-base/dict-base-detail-store';
import { DmExpenseService } from 'src/app/model/dictionary/dm-expense/dm-expense-service';

@Component({
  selector: 'dm-expense-detail',
  templateUrl: './dm-expense-detail.component.html',
  styleUrls: ['./dm-expense-detail.component.scss'],
  providers: [
    DictBaseDetailStore,
    FtsDictBaseDetailInject,
    {
      provide: MyReference,
      useExisting: forwardRef(() => DmExpenseDetailComponent),
    },
  ],
})
export class DmExpenseDetailComponent extends FTSDictBaseDetail {
  get formTitle(): string {
    return this.myInject.resourceManager.DmExpenseResource.MyResource.DM_EXPENSE;
  }

  formGroup!: FormGroup;
  idField: string = 'EXPENSE_ID';
  nameField: string = 'EXPENSE_NAME';
  width: number = 500;

  initFormGroup(): void {
    this.formGroup = this.fb.group({
      EXPENSE_ID: ['', [Validators.required, Validators.maxLength(20)]],
      EXPENSE_NAME: ['', [Validators.required, Validators.maxLength(100)]],
      EXPENSE_CLASS_ID: ['', [Validators.required, Validators.maxLength(20)]],
      EXPENSE_CLASS_NAME: [''],
      ACTIVE: [true],
    });
  }

  constructor(
    private fb: FormBuilder,
    dmExpenseSevice: DmExpenseService,
    myInject: FtsDictBaseDetailInject
  ) {
    super(dmExpenseSevice, myInject);
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

  expenseClassId_selectionChange(state: { item: any; form: FormGroup }) {
    this.formGroup.controls['EXPENSE_CLASS_NAME'].setValue(
      state?.item?.EXPENSE_CLASS_NAME || ''
    );
  }
}
