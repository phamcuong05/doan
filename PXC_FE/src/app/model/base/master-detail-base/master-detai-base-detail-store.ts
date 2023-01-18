import { EventEmitter, Injectable, Output } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { EnumLoadingState } from 'src/app/common/enum';
import { Observable } from 'rxjs';
import { ActionType } from 'src/app/common/types';
import { take } from 'rxjs/operators';

/**
 * State của dictbase
 * Component Store
 * Created by: MTLUC - 03/11/2021
 */
export interface MasterDetailBaseDetailState {
  /**
   * objectManager
   */
  objectManager: object;
  objectManagerOld: object;

  listingObjectName: string;
  detailObjectNames: string[];
  detailFrKey: string;

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
export class MasterDetailBaseDetailStore extends ComponentStore<MasterDetailBaseDetailState> {
  constructor() {
    super({
      objectManager: {},
      objectManagerOld: {},
      listingObjectName: '',
      detailObjectNames: [],
      detailFrKey: '',
      actionType: 'NONE',
      error: undefined,
      loadingState: EnumLoadingState.Init,
      width: 0,
      height: 0,
      title: '',
      isShow: false,
    });
  }

  //#region state
  /**
   * select state
   * Created by: MTLUC - 02/11/2021
   */
  readonly state$: Observable<any> = this.select((state) => state);
  //#endregion

  //#region objectManager
  /**
   * select objectManager
   * Created by: MTLUC - 02/11/2021
   */
  readonly objectManager$: Observable<any> = this.select(
    (state) => state.objectManager
  );

  /*
   * set objectManager
   * Created by: MTLUC - 02/11/2021
   */
  readonly setObjectManager = this.updater((state, objectManager: any) => {
    return { ...state, objectManager };
  });
  //#endregion

  //#region objectManager
  /**
   * select objectManager
   * Created by: MTLUC - 02/11/2021
   */
  readonly objectManagerOld$: Observable<any> = this.select(
    (state) => state.objectManagerOld
  );

  /*
   * set objectManagerOld
   * Created by: MTLUC - 02/11/2021
   */
  readonly setObjectManagerOld = this.updater(
    (state, objectManagerOld: any) => {
      return { ...state, objectManagerOld };
    }
  );
  //#endregion

  //#region listingObjectName
  /**
   * select listingObjectName
   * Created by: MTLUC - 02/11/2021
   */
  readonly listingObjectName$: Observable<string> = this.select(
    (state) => state.listingObjectName
  );

  /*
   * set listingObjectName
   * Created by: MTLUC - 02/11/2021
   */
  readonly setListingObjectName = this.updater(
    (state, listingObjectName: string) => {
      return { ...state, listingObjectName };
    }
  );
  //#endregion

  //#region detailObjectNames
  /**
   * select detailObjectNames
   * Created by: MTLUC - 02/11/2021
   */
  readonly detailObjectNames$: Observable<Array<string>> = this.select(
    (state) => state.detailObjectNames
  );

  /*
   * set DetailObjectNames
   * Created by: MTLUC - 02/11/2021
   */
  readonly setDetailObjectNames = this.updater(
    (state, detailObjectNames: string[]) => {
      return { ...state, detailObjectNames };
    }
  );
  //#endregion

  //#region detailFrKey
  /**
   * select detailFrKey
   * Created by: MTLUC - 02/11/2021
   */
  readonly detailFrKey$: Observable<string> = this.select(
    (state) => state.detailFrKey
  );

  /*
   * set detailFrKey
   * Created by: MTLUC - 02/11/2021
   */
  readonly setDetailFrKey = this.updater((state, detailFrKey: string) => {
    return { ...state, detailFrKey };
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
   * set actionType
   * Created by: MTLUC - 02/11/2021
   */
  readonly setActionType = this.updater((state, actionType: ActionType) => {
    return { ...state, actionType };
  });
  //#endregion

  //#region width
  /**
   * select width
   * Created by: MTLUC - 02/11/2021
   */
  readonly width$: Observable<number> = this.select((state) => state.width);

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
  readonly height$: Observable<number> = this.select((state) => state.height);

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
  readonly title$: Observable<string> = this.select((state) => state.title);

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
  readonly isShow$: Observable<boolean> = this.select((state) => state.isShow);

  /*
   * set isShow
   * Created by: MTLUC - 02/11/2021
   */
  readonly setIsShow = this.updater((state, isShow: boolean) => {
    return { ...state, isShow };
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
      objectManager: data.objectManager,
      error: data.error,
    };
  });

  getValue<T>(obj: Observable<T>): T | undefined {
    let value: T | undefined;
    obj.pipe(take(1)).subscribe((_value) => {
      value = _value;
    });
    return value;
  }
}
