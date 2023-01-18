export interface DmCashbankLimit {
    PR_KEY: string;
    ORGANIZATION_ID: string;
    ORGANIZATION_NAME: string;
    ACCOUNT_ID: string;
    ACCOUNT_NAME: string;
    BANK_ID: string;
    BANK_NAME: string;
    VALID_DATE: Date;
    LIMIT: number;
    NOTES: string;
    USER_ID: string;
    CREATE_DATE: Date;
    MODIFY_DATE: Date;
}