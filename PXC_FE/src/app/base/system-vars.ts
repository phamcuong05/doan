import { SystemVar } from "../model/system/system-var/system-var";

/**
 * SystemVars : Các biến hệ thống
 */
export class SystemVars {

   public systemVars !: SystemVar [];
    /**
   * constructor
   */
    constructor() {
    }

    /**
     * Lấy khai báo hệ thống
     * @param _varName 
     */
    GetSystemVars (_varName : string): string {
        let varValue = this.systemVars?.find(x=>x.VAR_NAME == _varName)?.VAR_VALUE;        
        return <string>varValue;
    }

}
