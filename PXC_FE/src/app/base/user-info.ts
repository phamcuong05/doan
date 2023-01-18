
/**
 * Thông tin User đăng nhập hệ thống
 */
export class UserInfo {
    public UserID !: string;
    public UserName !: string ;
    public UserGroupID !: string;
    public ClientIP !: string ;
    public ClientMachineName !: string ;
    public EmployeeId !: string ;
    public OrganizationID !: string ;
    public OrganizationName !: string ;
    public ParentOrganizationID !: string ;
    public ParentOrganizationName !: string ;
    public ModuleList !: string ;
    public POSShiftKey !: string ;
    public HTShiftKey !: string ;
    public RegisterID !: string ;
    public IsSubOrg : boolean = false;
    public WorkingYear !: number;

    constructor(){
    }
}
