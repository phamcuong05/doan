import { EventEmitter, Injectable } from '@angular/core';
import { ExceptionManager } from './exception-manager';
import { SysTables } from './sys-tables';
import { SystemVars } from './system-vars';
import { UserInfo } from './user-info';

@Injectable({
  providedIn: 'root',
})

/**
 * FTSMain:
 */
export class FTSMain {
  public MainCurrency!: string;
  public SecondCurrency!: string;
  public WorkingYear!: number;
  public Language!: string;
  public DayStartOfFirstYear!: Date;
  /**Thập phân số lượng */
  public TPSL!: number;
  /**Thập phân số tiền VND */
  public TPSTVND!: number;
  /**Thập phân số tiền nguyên tệ */
  public TPSTNTE!: number;
  /**Thập phân số tiền mở rộng */
  public TPSTEXTRA!: number;
  /**Thập phân đơn giá VND */
  public TPDGVND!: number;
  /**Thập phân đơn giá nguyên tệ */
  public TPDGNTE!: number;
  /**Thập phân tỷ giá */
  public TPEXRATE!: number;
  /**Thông tin người dùng */
  public UserInfo!: UserInfo;
  /**Biến hệ thống */
  public SystemVars!: SystemVars;
   /**Bảng hệ thống */
   public SysTables!: SysTables;
  /**Xử lý ngoại lệ */
  public ExceptionManager !: ExceptionManager;
  /**Biến PageSize */
  public PageSize: number = 50;

  //Menu
  public MenusChange = new EventEmitter<any[]>();
  private _Menus: any[] = [];
  public get Menus(): any[] {
    return this._Menus;
  }
  public set Menus(v: any[]) {
    this._Menus = v;
    this.MenusChange.next(v);
  }
  /**Format date */
  private _dateFormat: string = 'dd/MM/yyyy';
  public get dateFormat(): string {
    return this._dateFormat;
  }
  public set dateFormat(v: string) {
    this._dateFormat = v;
  }
  /**Format Quantity */
  private _quantityFormat: string = 'n2';
  public get quantityFormat(): string {
    return this._quantityFormat;
  }
  public set quantityFormat(v: string) {
    this._quantityFormat = v;
  }
  /**Format UnitPrice */
  private _UnitPriceFormat: string = 'n0';
  public get unitPriceFormat(): string {
    return this._UnitPriceFormat;
  }
  public set unitPriceFormat(v: string) {
    this._UnitPriceFormat = v;
  }
  /**Format UnitPriceOrig */
  private _UnitPriceOrigFormat: string = 'n2';
  public get unitPriceOrigFormat(): string {
    return this._UnitPriceOrigFormat;
  }
  public set unitPriceOrigFormat(v: string) {
    this._UnitPriceOrigFormat = v;
  }
  /**Format Amount */
  private _AmountFormat: string = 'n0';
  public get amountFormat(): string {
    return this._AmountFormat;
  }
  public set amountFormat(v: string) {
    this._AmountFormat = v;
  }
  /**Format Amount Orig */
  private _AmountOrigFormat: string = 'n2';
  public get amountOrigFormat(): string {
    return this._AmountOrigFormat;
  }
  public set amountOrigFormat(v: string) {
    this._AmountOrigFormat = v;
  }
   /**Format ExRate Orig */
   private _ExRateFormat: string = 'n4';
   public get exRateFormat(): string {
     return this._ExRateFormat;
   }
   public set exRateFormat(v: string) {
     this._ExRateFormat = v;
   }
  /**
   * constructor
   */
  constructor() {
    this.SystemVars = new SystemVars();
    this.SysTables = new SysTables();
    this.ExceptionManager = new ExceptionManager();
    this.UserInfo = new UserInfo();
    this.UserInfo.UserID = '';
    this.UserInfo.UserName = '';
    this.UserInfo.UserGroupID = '';
    this.UserInfo.OrganizationID = '';
    this.UserInfo.OrganizationName = '';
    this.UserInfo.ParentOrganizationID = '';

    this.MainCurrency = 'VND';
    this.SecondCurrency = 'VND';
    this.WorkingYear = 2021;
    this.Language = 'VN';
    this.DayStartOfFirstYear = new Date(2021, 1, 1);
    this.TPSL = 2;
    this.TPSTVND = 2;
    this.TPSTNTE = 2;
    this.TPSTEXTRA = 2;
    this.TPDGVND = 2;
    this.TPDGNTE = 2;
  }
}
