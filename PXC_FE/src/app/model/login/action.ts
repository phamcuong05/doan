import { createAction, props } from '@ngrx/store';

/**
 * Action gọi login
 * Created by: MTLUC - 19/10/2021
 */
export const loginAction = createAction(
  'login',
  props<{ userName: string; password: string; workingYear: number }>()
);

/**
 * Action login thành công
 * Created by: MTLUC - 19/10/2021
 */
export const loginActionSuccess = createAction(
  'login success',
  props<{ token: string; tokenType: string; expiredAt: Date }>()
);

/**
 * Action login lỗi
 * Created by: MTLUC - 19/10/2021
 */
export const loginActionFail = createAction(
  'login fail',
  props<{ error: Error }>()
);

/**
 * Action login lại
 * Created by: MTLUC - 19/10/2021
 */
export const restoreAuthAction = createAction(
  'restore auth',
  props<{ token: string; tokenType: string; expiredAt: Date }>()
);

/**
 * Action login lại
 * Created by: MTLUC - 19/10/2021
 */
export const clearTokenAction = createAction('clear token');
