/**
 * Danh mục tài khoản
 */
export interface DmAccount {
    ACCOUNT_ID: string;
    ACCOUNT_NAME: string;
    IS_PARENT: boolean;
    PARENT_ACCOUNT_ID: string;
    BALANCE_TYPE: string;
    CURRENCY_ID: string;
    RATE_METHOD: string;
    IS_OOB: boolean;
    IS_JOB: boolean;
    IS_PR_DETAIL: boolean;
    IS_EXPENSE: boolean;
    IS_BANK: boolean;
    IS_EMPLOYEE: boolean;
    IS_DEPARTMENT: boolean;
    IS_INSURANCE_SOURCE: boolean;
    IS_CAPITAL_SOURCE: boolean;
    IS_REINSURANCE_SOURCE: boolean;
    IS_AGENT:boolean;
    IS_ITEM:boolean;
    IS_VAT: boolean;
    ACTIVE: boolean;
    IS_CONTRACT:boolean;
    USER_ID: string;
    CURRENCY_NAME:string;
    PARENT_ACCOUNT_NAME:string;
}