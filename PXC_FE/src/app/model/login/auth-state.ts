import { EnumLoadingState } from "src/app/common/enum";

export interface AuthState {
  token?: string;
  tokenType?: string;
  loading: EnumLoadingState;
  expiredAt?: Date;
  error?: any;
}
