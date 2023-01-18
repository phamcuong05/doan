import { createReducer, on } from '@ngrx/store';
import { EnumLoadingState } from 'src/app/common/enum';
import { loginAction, loginActionFail, loginActionSuccess, restoreAuthAction } from './action';
import { AuthState } from './auth-state';

export const authReducer = createReducer(
  {
    token: '',
    tokenType: 'Bearer',
    loading: EnumLoadingState.Init,
  } as AuthState,
  on(loginAction, (state): AuthState => {
    return {
      ...state,
      loading: EnumLoadingState.Loading,
      token: '',
      expiredAt: undefined,
      error: undefined,
    };
  }),
  on(
    loginActionSuccess,
    restoreAuthAction,
    (state, action): AuthState => {
      return {
        ...state,
        ...action,
        loading: EnumLoadingState.Complete,
        error: undefined,
      };
    }
  ),
  on(loginActionFail, (state, action): AuthState => {
    return {
      ...state,
      ...action,
      loading: EnumLoadingState.Complete,
    };
  })
);
