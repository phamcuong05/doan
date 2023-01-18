import { EnumLangID } from './enum';
import { LocalStorage } from './local-storage';
import { Injectable } from '@angular/core';
import { HttpServiceModule } from './http-service.module';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ResourceManager {
  CommonResource: Resource = {} as Resource;
  LoginResource: Resource = {} as Resource;
  DmPrDetailResource: Resource = {} as Resource;
  SecUserResource: Resource = {} as Resource;
  DmCurrencyResource: Resource = {} as Resource;
  DmExchangeRateResource: Resource = {} as Resource;
  DmExpenseClassResource: Resource = {} as Resource;
  DmExpenseResource: Resource = {} as Resource;
  DmDepartmentResource: Resource = {} as Resource;
  DmEmployeeResource: Resource = {} as Resource;
  DmDistrictResource: Resource = {} as Resource;
  DmProvinceResource: Resource = {} as Resource;
  DmItemClassResource: Resource = {} as Resource;
  DmContractStatusResource: Resource = {} as Resource;
  DmContractClassResource: Resource = {} as Resource;
  DmContractLimitResource: Resource = {} as Resource;
  DmRiskClassResource: Resource = {} as Resource;
  DmUnitResource: Resource = {} as Resource;
  DmVatTaxResource: Resource = {} as Resource;
  DmWarehouseResource: Resource = {} as Resource;
  DmOrganizarionResource: Resource = {} as Resource;
  DmPrDetailClassResource: Resource = {} as Resource;
  DmItemOpResource: Resource = {} as Resource;
  DmItemResource: Resource = {} as Resource;
  DmBankResource: Resource = {} as Resource;
  DmAccountResource: Resource = {} as Resource;
  DmPeriodResource: Resource = {} as Resource;
  DmEstimateTypeResource: Resource = {} as Resource;
  DmCapitalSourceResource: Resource = {} as Resource;
  DmAgentResource: Resource = {} as Resource;
  DmSecurityClassResource: Resource = {} as Resource;
  DmSecurityTypeResource: Resource = {} as Resource;
  DmSecurityResource: Resource = {} as Resource;
  DmAdvanceLimitResource: Resource = {} as Resource;
  DmInvestmenTermResource: Resource={} as Resource;
  DmVatPurchaseResource: Resource = {} as Resource;
  DmCashbankLimitResource: Resource = {} as Resource;
  DmJobResource: Resource  = {} as Resource;

  ListServiceChargeResource: Resource = {} as Resource;
  ListPackageResource: Resource = {} as Resource;
  ListOrderResource: Resource = {} as Resource;
  ListWHResource: Resource = {} as Resource;
  ListMawbResource: Resource = {} as Resource;
  /**
   * ctor
   * id ngôn ngữ sẽ đọc từ localstorage
   */
  constructor(
    private _localStorage: LocalStorage,
    private http: HttpServiceModule
  ) {
    for (const key in this) {
      if (
        Object.prototype.hasOwnProperty.call(this, key) &&
        key.indexOf('Resource') >= 0
      ) {
        (this as any)[key] = new Resource(this._localStorage, this.http, key);
      }
    }
  }
}

export class Resource {
  private resourceFile: any = {};
  private resourceName!: string;
  private langID!: EnumLangID;
  private http!: HttpServiceModule;

  constructor(
    _localStorage: LocalStorage,
    _http: HttpServiceModule,
    resourceName: string
  ) {
    this.resourceName = resourceName;
    this.langID = _localStorage.LangID;
    this.http = _http;
    _localStorage.EventChangeLanguage.subscribe(() => {
      this.langID = _localStorage.LangID;
    });
  }

  public get MyResource(): any {
    let resource = this.resourceFile?.[`Resource${EnumLangID[this.langID]}`];
    if (!resource) {
      this.resourceFile[`Resource${EnumLangID[this.langID]}`] = {};
      this.LoadResouce();
    }
    return resource;
  }

  /**
   * load resource
   */
  private LoadResouce() {
    const that = this;

    that.http
      .get<any>(
        `./assets/resource/${this.resourceName}.${
          EnumLangID[this.langID]
        }.json`,
        {
          v: environment.version,
        }
      )
      .then((data: any) => {
        if (data) {
          that.resourceFile[`Resource${EnumLangID[this.langID]}`] = data;
        }
      })
      .catch((error) => {
        console.error(error);
      });
  }
}
