import { EventEmitter, Injectable, Output } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { EnumLoadingState } from 'src/app/common/enum';
import { Observable } from 'rxjs';
import { ActionType } from 'src/app/common/types';

/**
 * State của dictbase
 * Component Store
 * Created by: MTLUC - 03/11/2021
 */
export interface DictBaseDetailState {

  /**
   * dòng dữ liệu đang thao tác
   */
  currentRow: object;

  currentRowOld: object;

  /**
   * actionType của form detail
   */
  actionType: ActionType;

  /**
   * lỗi nếu có
   */
  error: any;

  /**
   * trạng thái load dữ liệu
   */
  loadingState: EnumLoadingState;

  width: number;
  height: number;
  title: string;
  isShow: boolean;
}

@Injectable()
export class DictBaseDetailStore extends ComponentStore<DictBaseDetailState> {
  constructor() {
    super({
      currentRow: {},
      currentRowOld: {},
      actionType: 'NONE',
      error: undefined,
      loadingState: EnumLoadingState.Init,
      isShow: false
    } as DictBaseDetailState);
  }
  //#region state
  /**
   * select state
   * Created by: MTLUC - 02/11/2021
   */
  readonly state$: Observable<DictBaseDetailState> = this.select(
    (state) => state
  );
  //#endregion

  //#region width
  /**
   * select width
   * Created by: MTLUC - 02/11/2021
   */
   readonly width$: Observable<number> = this.select(
    (state) => state.width
  );

  /*
   * set width
   * Created by: MTLUC - 02/11/2021
   */
  readonly setWidth = this.updater((state, width: number) => {
    return { ...state, width };
  });
  //#endregion

  //#region height
  /**
   * select height
   * Created by: MTLUC - 02/11/2021
   */
   readonly height$: Observable<number> = this.select(
    (state) => state.height
  );

  /*
   * set height
   * Created by: MTLUC - 02/11/2021
   */
  readonly setHeight = this.updater((state, height: number) => {
    return { ...state, height };
  });
  //#endregion

  //#region title
  /**
   * select title
   * Created by: MTLUC - 02/11/2021
   */
   readonly title$: Observable<string> = this.select(
    (state) => state.title
  );

  /*
   * set title
   * Created by: MTLUC - 02/11/2021
   */
  readonly setTitle = this.updater((state, title: string) => {
    return { ...state, title };
  });
  //#endregion

  //#region isShow
  /**
   * select isShow
   * Created by: MTLUC - 02/11/2021
   */
   readonly isShow$: Observable<boolean> = this.select(
    (state) => state.isShow
  );

  /*
   * set isShow
   * Created by: MTLUC - 02/11/2021
   */
  readonly setIsShow = this.updater((state, isShow: boolean) => {
    return { ...state, isShow };
  });
  //#endregion

  //#region currentRow
  /**
   * select currentRow
   * Created by: MTLUC - 02/11/2021
   */
  readonly currentRow$: Observable<any> = this.select(
    (state) => state.currentRow
  );

  /*
   * set currentRow
   * Created by: MTLUC - 02/11/2021
   */
  readonly setCurrentRow = this.updater((state, currentRow: any) => {
    return { ...state, currentRow };
  });
  //#endregion

  //#region currentRow
  /**
   * select currentRowOld
   * Created by: MTLUC - 02/11/2021
   */
   readonly currentRowOld$: Observable<any> = this.select(
    (state) => state.currentRowOld
  );

  /*
   * set currentRowOld
   * Created by: MTLUC - 02/11/2021
   */
  readonly setCurrentRowOld = this.updater((state, currentRowOld: any) => {
    return { ...state, currentRowOld };
  });
  //#endregion

  //#region actionType
  /**
   * select actionType
   * Created by: MTLUC - 02/11/2021
   */
  readonly actionType$: Observable<ActionType> = this.select(
    (state) => state.actionType
  );

  /*
   * set currentRow
   * Created by: MTLUC - 02/11/2021
   */
  readonly setActionType = this.updater((state, actionType: ActionType) => {
    return { ...state, actionType };
  });
  //#endregion

  //#region error
  /**
   * Select error
   * Created by: MTLUC - 03/11/2021
   */
  readonly error$: Observable<any> = this.select((state) => state.error);

  /**
   * Set Error
   * Created by: MTLUC - 03/11/2021
   */
  readonly setError = this.updater((state, error: any) => {
    return { ...state, error };
  });
  //#endregion

  //#region loadingState
  /**
   * Select loadingState
   * Created by: MTLUC - 03/11/2021
   */
  readonly loadingState$: Observable<EnumLoadingState> = this.select(
    (state) => state.loadingState
  );

  /**
   * Set loadingState
   * Created by: MTLUC - 03/11/2021
   */
  readonly setLoadingState = this.updater(
    (state, loadingState: EnumLoadingState) => {
      return { ...state, loadingState };
    }
  );
  //#endregion

  @Output() actionClick: EventEmitter<ActionType> = new EventEmitter();

  @Output() actionResult: EventEmitter<{
    actionType: ActionType;
    success: boolean;
    data: any;
    error?: any;
  }> = new EventEmitter();

  readonly loadData = this.updater((state) => {
    return {
      ...state,
      loadingState: EnumLoadingState.Loading,
      error: undefined,
    };
  });

  readonly loadDataComplete = this.updater((state, error: any) => {
    return {
      ...state,
      loadingState: EnumLoadingState.Complete,
      error: error,
    };
  });

  readonly formActionComplete = this.updater((state, data: any) => {
    return {
      ...state,
      loadingState: EnumLoadingState.Complete,
      currentRow: data.currentRow,
      error: data.error,
    };
  });
}
