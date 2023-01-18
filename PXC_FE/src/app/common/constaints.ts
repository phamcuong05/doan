/**
 * Constaints sử sụng trong ứng dụng
 */
export const Constaints = {
  /**
   * Key localStorage
   */
  LocalStorageKey: {
    /**
     * Id ngôn ngữ
     */
    LANG_ID: 'LangID',

    /**
     * AuthState
     */
    AUTH_STATE: 'AuthState',

    FONTSIZE: "FONTSIZE",
    DARKMODE: "USED_DARK_MODE"
  },

  /**
   * Mã lỗi khi login
   */
  LoginErrorCode: {
    /**
     * TK|MK không đúng
     */
    UserNameOrPassWordIncorrect:
      'Your login details are incorrect. Please check your login information.',

    /**
     * UserNameRequired
     */
    UserNameRequired: 'UserNameRequired',

    /**
     * UserNameRequired
     */
    PasswordRequired: 'PasswordRequired',
  },

  /**
   * Selector
   */
  Selector: {
    AUTH: 'AUTH_SELECTOR',
    PR_DETAIL: 'PR_DETAIL',
    PR_DETAIL_CLASS: 'PR_DETAIL_CLASS',
    PR_DETAIL_CLASS1: 'PR_DETAIL_CLASS1',
    DM_ACCOUNT: 'DM_ACCOUNT',
    APP_STATE: 'APP_STATE'
  },

  /**
   * Kỳ số liệu
   */
  PERIODS: 'PERIODS',

  FIELDSELECTIONS:'FIELDSELECTIONS'
};
