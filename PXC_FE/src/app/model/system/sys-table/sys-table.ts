export interface SysTable {
    TABLE_NAME: string;
    ID_FIELD: string;
    NAME_FIELD: string;
    TABLE_TYPE: string;
    CAN_GROUP: boolean;
    ID_AUTO: boolean;
    ID_MASK: string;
    ID_LENGTH: number;
    ID_PARTS: number;
    ID_SPLIT: string;
    LOAD_BY_SEARCH: boolean;
}