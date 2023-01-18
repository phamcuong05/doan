import {
  Component,
  ContentChildren,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
  QueryList,
  ViewEncapsulation,
} from '@angular/core';
import { AbstractControl, FormGroup } from '@angular/forms';
import { WindowService } from '@progress/kendo-angular-dialog';
import {
  ComboBoxColumnComponent,
  VirtualizationSettings,
} from '@progress/kendo-angular-dropdowns';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ResourceManager } from 'src/app/common/resource-manager';
import { FTSDictBaseDetail } from '../fts-dict-base/fts-dict-base-detail/fts-dict-base-detail';
@Component({
  selector: 'fts-combobox-multicolumn',
  templateUrl: './fts-combobox-multicolumn.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class FtsComboboxMulticolumnComponent implements OnInit, OnDestroy {
  @Input() form!: FormGroup;
  @Input() controlName!: string;
  @Input() classList: string = 'control-input';
  @Input() allowCustom: boolean = false;
  @Input() textField!: string;
  @Input() valueField!: string;
  @Input() valuePrimitive: boolean = true;
  @Input() itemHeight: number = 26;
  @Input() pageSize: number = 10;
  @Input() suggest: boolean = true;
  @Input() readonly: boolean = false;
  @Input() quickAdd: boolean = true;
  @Input() editorQuickAdd!: Function;

  @Input() data!: any[] | [] | any;
  @Output() dataChange = new EventEmitter<any[] | [] | any>();

  @ContentChildren(ComboBoxColumnComponent)
  columns!: QueryList<ComboBoxColumnComponent>;

  virtual: VirtualizationSettings = {
    itemHeight: this.itemHeight,
    pageSize: this.pageSize,
  };

  get formControl(): AbstractControl | null {
    return this.form.get(this.controlName);
  }

  private onDestroy$: Subject<void> = new Subject<void>();

  constructor(
    public resourceManager: ResourceManager,
    private windowService: WindowService
  ) {}

  ngOnDestroy(): void {
    this.onDestroy$.next();
  }

  ngOnInit(): void {}

  /**
   * Show popup editor
   * @param actionType Actiontype
   */
  openEditor() {
    const that = this;
    let _currentRow = {};
    if (_currentRow) {
      const windowref = this.windowService.open({
        content: this.editorQuickAdd,
      });

      let comp: FTSDictBaseDetail = windowref.content.instance;
      comp.open(_currentRow, 'ADD');

      //subscribe nếu thêm sửa xóa thành công thì load lại list
      comp?.myInject.detailStore.actionResult
        .pipe(takeUntil(this.onDestroy$))
        .subscribe((state) => {
          if (state.success && state.actionType == 'ADD') {
            const currentRow: any = state.data;
            that.data.push({
              ...currentRow,
              [that.valueField]: currentRow[comp.idField],
              [that.textField]: currentRow[comp.nameField],
            });
            that.formControl?.setValue(currentRow[comp.idField]);
          }
        });
    }
  }

  selectionChange_Handler(e: any) {
    this.selectionChange.emit(e);
  }

  @Output() selectionChange: EventEmitter<any> = new EventEmitter();
}
