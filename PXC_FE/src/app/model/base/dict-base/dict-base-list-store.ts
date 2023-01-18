import { EventEmitter, Injectable, Output } from '@angular/core';
import { ComponentStore } from '@ngrx/component-store';
import { EnumLoadingState } from 'src/app/common/enum';
import { Observable } from 'rxjs';
import { FtsColumn } from 'src/app/controls/fts-grid/fts-grid.component';
import {
  CompositeFilterDescriptor,
  SortDescriptor,
} from '@progress/kendo-data-query';
import { ActionType } from 'src/app/common/types';
import { DataStateChangeEvent, GridDataResult } from '@progress/kendo-angular-grid';

export interface DictBaseListState {
  currentRow?: object;
  sortable: boolean;
  filterable: boolean;
  gridData: GridDataResult;
  columns: Array<FtsColumn>;
  filter: CompositeFilterDescriptor;
  sort: Array<SortDescriptor>;
  loadingState: EnumLoadingState;
  error: any;
}

@Injectable()
export class DictBaseListStore extends ComponentStore<DictBaseListState> {
  constructor() {
    super({
      currentRow: undefined,
      sortable: true,
      filterable: true,
      gridData: { data: [], total: 0 },
      columns: [],
      filter: {
        logic: 'and',
        filters: [],
      },
      sort: [],
      loadingState: EnumLoadingState.Init,
      error: undefined,
    });
  }

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

  //#region sortable
  /**
   * select sortable
   * Created by: MTLUC - 02/11/2021
   */
  readonly sortable$: Observable<boolean> = this.select(
    (state) => state.sortable
  );

  /*
   * set sortable
   * Created by: MTLUC - 02/11/2021
   */
  readonly setSortable = this.updater((state, sortable: boolean) => {
    return { ...state, sortable };
  });
  //#endregion

  //#region filterable
  /**
   * select filterable
   * Created by: MTLUC - 02/11/2021
   */
  readonly filterable$: Observable<boolean> = this.select(
    (state) => state.sortable
  );

  /*
   * set filterable
   * Created by: MTLUC - 02/11/2021
   */
  readonly setFilterable = this.updater((state, filterable: boolean) => {
    return { ...state, filterable };
  });
  //#endregion

  //#region gridData
  /**
   * select gridData
   * Created by: MTLUC - 02/11/2021
   */
  readonly gridData$: Observable<GridDataResult> = this.select(
    (state) => state.gridData
  );

  /*
   * set gridData
   * Created by: MTLUC - 02/11/2021
   */
  readonly setGridData = this.updater((state, gridData: GridDataResult) => {
    return { ...state, gridData };
  });
  //#endregion

  //#region columns
  /**
   * select columns
   * Created by: MTLUC - 02/11/2021
   */
  readonly columns$: Observable<Array<FtsColumn>> = this.select(
    (state) => state.columns
  );

  /*
   * set columns
   * Created by: MTLUC - 02/11/2021
   */
  readonly setColumns = this.updater((state, columns: Array<FtsColumn>) => {
    return { ...state, columns };
  });
  //#endregion

  //#region filter
  /**
   * select filter
   * Created by: MTLUC - 02/11/2021
   */
  readonly filter$: Observable<CompositeFilterDescriptor> = this.select(
    (state) => state.filter
  );

  /*
   * set filter
   * Created by: MTLUC - 02/11/2021
   */
  readonly setFilter = this.updater(
    (state, filter: CompositeFilterDescriptor) => {
      return { ...state, filter };
    }
  );
  //#endregion

  //#region sort
  /**
   * select sort
   * Created by: MTLUC - 02/11/2021
   */
  readonly sort$: Observable<Array<SortDescriptor>> = this.select(
    (state) => state.sort
  );

  /*
   * set sort
   * Created by: MTLUC - 02/11/2021
   */
  readonly setSort = this.updater((state, sort: Array<SortDescriptor>) => {
    return { ...state, sort };
  });
  //#endregion

  //#region loadingState
  /**
   * select loadingState
   * Created by: MTLUC - 02/11/2021
   */
  readonly loadingState$: Observable<EnumLoadingState> = this.select(
    (state) => state.loadingState
  );

  /*
   * set loadingState
   * Created by: MTLUC - 02/11/2021
   */
  readonly setLoadingState = this.updater(
    (state, loadingState: EnumLoadingState) => {
      return { ...state, loadingState };
    }
  );
  //#endregion

  /*
   * set loadData
   * Created by: MTLUC - 02/11/2021
   */
  readonly loadData = this.updater((state) => {
    return {
      ...state,
      error: undefined,
      loadingState: EnumLoadingState.Loading,
    };
  });

  /*
   * set loadDataComplete
   * Created by: MTLUC - 02/11/2021
   */
  readonly loadDataComplete = this.updater(
    (
      state,
      newState: {
        gridData: GridDataResult;
        error: any;
      }
    ) => {
      return {
        ...state,
        gridData: newState.gridData,
        error: newState.error,
        loadingState: EnumLoadingState.Complete,
      };
    }
  );

  @Output() actionClick: EventEmitter<[ActionType, EnumLoadingState]> =
    new EventEmitter();

  @Output() dataStateChange:  EventEmitter<DataStateChangeEvent> =
  new EventEmitter();
}
