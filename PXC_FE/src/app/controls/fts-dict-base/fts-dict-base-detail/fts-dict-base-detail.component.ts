import {
  Component,
  ContentChild,
  Input,
  OnDestroy,
  OnInit,
  TemplateRef,
  ViewChild,
  ViewEncapsulation,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { TooltipDirective } from '@progress/kendo-angular-tooltip';
import { combineLatest, Observable, Subject } from 'rxjs';
import { map, take, takeUntil } from 'rxjs/operators';
import { commonFunction } from 'src/app/common/commonFunction';
import { EnumLoadingState } from 'src/app/common/enum';
import { FtsDictBaseDetailInject } from './fts-dict-base-detail-inject';

@Component({
  selector: 'fts-dict-base-detail',
  templateUrl: './fts-dict-base-detail.component.html',
  encapsulation: ViewEncapsulation.None,
})
export class FtsDictBaseDetailComponent implements OnInit, OnDestroy {
  @Input() formGroup!: FormGroup;
  @ContentChild('formControls') formControls!: TemplateRef<any>;
  @ViewChild(TooltipDirective) tooltipDir!: TooltipDirective;

  vm$!: Observable<{
    isShowBtnSave: boolean;
    isShowBtnEdit: boolean;
    isShowBtnDelete: boolean;
    maxWidth: number;
    maxHeight: number;
    minWidth: number;
    minHeight: number;
    width: number;
    height: number;
    title: string;
    isShow: boolean;
    isLoading: boolean;
  }>;
  @Input() showBtnAdd: boolean = true;
  @Input() showBtnDuplicate: boolean = true;
  @Input() showBtnEdit: boolean = true;
  @Input() showBtnDelete: boolean = true;
  @Input() showBtnDocument: boolean = false;

  id: string = commonFunction.newGuid();
  private onDestroy$: Subject<void> = new Subject<void>();
  constructor(public myInject: FtsDictBaseDetailInject) {}

  ngOnInit(): void {
    /**
     * combine state title window, ẩn hiện các nút task bar, enable controls
     */
    this.vm$ = combineLatest([
      this.myInject.detailStore.actionType$,
      this.myInject.detailStore.width$,
      this.myInject.detailStore.height$,
      this.myInject.detailStore.title$,
      this.myInject.detailStore.isShow$,
      this.myInject.detailStore.loadingState$,
    ]).pipe(
      map(([actionType, width, height, title, isShow, loadingState]) => {
        return {
          isShowBtnSave:
            actionType == 'ADD' ||
            actionType == 'DUPLICATE' ||
            actionType == 'EDIT',
          isShowBtnEdit: actionType == 'VIEW',
          isShowBtnDelete: actionType == 'VIEW',
          width: width,
          height: height,
          title: title,
          isShow: isShow,
          isLoading: loadingState === EnumLoadingState.Loading ? true : false,
        } as {
          isShowBtnSave: boolean;
          isShowBtnEdit: boolean;
          isShowBtnDelete: boolean;
          maxWidth: number;
          maxHeight: number;
          minWidth: number;
          minHeight: number;
          width: number;
          height: number;
          title: string;
          isShow: boolean;
          isLoading: boolean;
        };
      })
    );

    //patch value lên form
    this.myInject.detailStore.currentRow$
      .pipe(takeUntil(this.onDestroy$))
      .subscribe((currentRow) => {
        let objReset = {};
        let objOld: any = {};
        this.myInject.detailStore.currentRowOld$
          .pipe(take(1))
          .subscribe((_currentRowOld) => {
            objOld = _currentRowOld;
          });
        for (const controlName in this.formGroup.controls) {
          if (
            Object.prototype.hasOwnProperty.call(
              this.formGroup.controls,
              controlName
            )
          ) {
            if (objOld[controlName]) {
              objReset = { ...objReset, [controlName]: objOld[controlName] };
            }
          }
        }
        this.formGroup.reset(objReset);
        this.formGroup.patchValue(currentRow);
      });
  }

  ngOnDestroy(): void {
    this.onDestroy$.next();
  }

  btnClose_Click($event: Event): void {
    this.myInject.detailStore.actionClick.emit('CLOSE');
  }

  /**
   * click thêm
   * @param $event
   * Created by: TAN.VU - 23/11/2021
   */
  btnAdd_Click($event: Event): void {
    this.myInject.detailStore.actionClick.emit('ADD');
  }

  /**
   * click copy
   * @param $event
   * Created by: TAN.VU - 23/11/2021
   */
  btnCopy_Click($event: Event): void {
    this.myInject.detailStore.actionClick.emit('DUPLICATE');
  }

  /**
   * click copy
   * @param $event
   * Created by: TAN.VU - 27/04/2022
   */
  btnDocument_Click($event: Event): void {
      this.myInject.detailStore.actionClick.emit('DOCUMENT');
  }

  /**
   * click sửa
   * @param $event
   * Created by: MTLUC - 28/10/2021
   */
  btnEdit_Click($event: Event): void {
    this.myInject.detailStore.actionClick.emit('EDIT');
  }

  /**
   * Nhấn xóa
   * @param $event
   * Created by: MTLUC - 29/10/2021
   */
  btnDelete_Click($event: Event): void {
    this.myInject.detailStore.actionClick.emit('DELETE');
  }

  onSubmit(): void {
    this.myInject.detailStore.actionClick.emit('SAVE');
  }

  closeWindow() {
    this.myInject.detailStore.actionClick.emit('CLOSE');
  }
}
