import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Constaints } from 'src/app/common/constaints';
import { AuthState } from './auth-state';

/**
 * Lấy AuthState
 * Created by: MTLUC - 19/10/2021
 */
export const selectAuthFeature = createFeatureSelector<AuthState>(
  Constaints.Selector.AUTH
);

export const selectAuthState = createSelector(selectAuthFeature,
  (state: AuthState) => state
)

/**
 * Lấy token
 * Created by: MTLUC - 19/10/2021
 */
export const selectAuthToken = createSelector(
  selectAuthFeature,
  (state: AuthState) => state.token
);

/**
 * Lấy token Type
 * Created by: MTLUC - 19/10/2021
 */
export const selectAuthTokenType = createSelector(
  selectAuthFeature,
  (state: AuthState) => state.tokenType
);

/**
 * Lấy trạng thái loading token
 * Created by: MTLUC - 19/10/2021
 */
export const selectAuthLoadingState = createSelector(
  selectAuthFeature,
  (state) => state.loading
);

/**
 * Lấy error
 * Created by: MTLUC - 19/10/2021
 */
 export const selectAuthError = createSelector(
  selectAuthFeature,
  (state) => state.error
);

/**
 * Lấy trạng thái auth
 * Created by: MTLUC - 19/10/2021
 */
export const selectAuthStatus = createSelector(
  selectAuthFeature,
  (state: AuthState) => {
    return !!(
      state &&
      state.token &&
      state.expiredAt &&
      state.expiredAt > new Date()
    );
  }
);
