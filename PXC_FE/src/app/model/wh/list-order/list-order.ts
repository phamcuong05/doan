export interface ListOrder {
    ORDER_ID: string,
    ORDER_DATE: Date,
    PACKAGE_CODE: string,
    PACKAGE_NAME: string,
    SERVICE_CHARGE_ID: string,
    SERVICE_CHARGE_NAME: string,
    TOTAL: number,
    BUY_FEE: number,
    SHIP_FEE: number,
    CUSTOMER_NAME: string,
    PHONE: string,
    ADDRESS: string,
    USER_ID: string
}