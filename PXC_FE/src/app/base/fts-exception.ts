/**
 * FtsExceptionManager
 */
export interface FtsException {
    mMessage: string;
    mExceptionID: string;
    mTableName: string;
    mFieldName: string;
    mPara: [];
    mRowPos: number;
    mExtraInformation: string;
}
