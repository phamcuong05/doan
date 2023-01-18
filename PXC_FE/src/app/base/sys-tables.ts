import { SysTable } from "../model/system/sys-table/sys-table";

/**
 * SysTables
 */
export class SysTables {

    public sysTables !: SysTable[];
    constructor() {
    }

    loadBySearch(tableName: string): boolean {
        let loadBySearch = this.sysTables?.find(x => x.TABLE_NAME == tableName)?.LOAD_BY_SEARCH;
        return <boolean>loadBySearch;
    }
}
