import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { environment } from 'src/environments/environment';
import { PrintFile } from '../other/PrintFile';
import { PrintTemplate } from '../other/PrintTemplates';
import { DmTemplate } from '../system/dm-template/dm-template';
import { PagingParam } from './paging/paging-param';

@Injectable({ providedIn: 'root' })
export class BaseService<T> {
  //#region ctor
  /**
   * constructor
   * Created by: MTLUC - 01/11/2021
   */
  constructor(public http: HttpServiceModule) {}
  //#endregion

  //#region prop

  /**
   * Link API service sử sụng để gọi lên server, mặc định = defaultAPI
   * Created by: MTLUC - 01/11/2021
   */
  readonly rootAPI: string = environment.defaultAPI;

  /**
   * service URL
   * Created by: MTLUC - 01/11/2021
   */
  readonly serviceUrl: string = '/';

  //#endregion

  //#region function
  /**
   * load data
   * @param param param query {}
   * @returns Promise T[]
   * Created by: MTLUC - 01/11/2021
   */
  public loadData(param?: any): Promise<T[]> {
    return this.http.get<T[]>(
      `${this.rootAPI}/${this.serviceUrl}GetAllData`,
      param
    );
  }

  /**
   * Lấy dữ liệu theo paging
   * @param pageIndex trang cần lấy
   * @param pageSize số bản ghi trên 1 trang
   * @param strFilter obj filter
   * @param srtSort  obj sort
   * @returns
   */
  public loadDataPaging(param: PagingParam): Promise<{
    RecordCount: number;
    Data: any[];
    SummaryData: any;
  }> {
    return this.http.get<{
      RecordCount: number;
      Data: any[];
      SummaryData: any;
    }>(`${this.rootAPI}/${this.serviceUrl}LoadPagingData`, {
      tranid: param.TranId,
      fields: JSON.stringify(param.FilterFields) || '',
      summaryfields: JSON.stringify(param.SumaryFields) || '',
      filter: JSON.stringify(param.FilterGroups) || '',
      sorts: JSON.stringify(param.Sorts) || '',
      pagesize: param.PageSize,
      pageindex: param.PageIndex,
    });
  }

  /**
   * load data
   * @param param param query {}
   * @returns Promise T[]
   * Created by: MTLUC - 01/11/2021
   */
  public GetDataListing(param?: any): Promise<T[]> {
    return this.http.get<T[]>(
      `${this.rootAPI}/${this.serviceUrl}GetDataListing`,
      param
    );
  }

  /**
   * Thêm mới 1 bản ghi
   * @param item dl bản ghi
   * @returns Promise T
   * Created by: MTLUC - 01/11/2021
   */
  public Create(item: T): Promise<T> {
    return this.http.post<T>(
      `${this.rootAPI}/${this.serviceUrl}UpdateData`,
      item
    );
  }

  /**
   * AddNewData : lấy dữ liệu khởi tạo khi addNew
   * @returns Promise T[]
   * Created by: TAN.VU - 17/12/2021
   */
  public AddNewData(tranId?: string): Promise<T> {
    return this.http.get<T>(
      `${this.rootAPI}/${this.serviceUrl}AddNewData`,
      tranId ? { tranId: tranId } : undefined
    );
  }

  /**
   * Update 1 bản ghi
   * @param item dl bản ghi
   * @returns Promise T
   * Created by: TAN.VU - 17/12/2021
   */
  public CreateData(item: T): Promise<T> {
    return this.http.post<T>(
      `${this.rootAPI}/${this.serviceUrl}UpdateNewData`,
      item
    );
  }

  /**
   * Update 1 bản ghi
   * @param item dl bản ghi
   * @returns Promise T
   * Created by: MTLUC - 01/11/2021
   */
  public UpdateData(item: T): Promise<T> {
    return this.http.post<T>(
      `${this.rootAPI}/${this.serviceUrl}UpdateEditData`,
      item
    );
  }

  /**
   * Update 1 bản ghi
   * @param item dl bản ghi
   * @returns Promise T
   * Created by: MTLUC - 01/11/2021
   */
  public Update(item: T): Promise<T> {
    return this.http.post<T>(
      `${this.rootAPI}/${this.serviceUrl}UpdateData`,
      item
    );
  }

  /**
   * Xóa 1 bản ghi
   * @param item dl bản ghi
   * @returns Promise T
   * Created by: MTLUC - 01/11/2021
   */
  public Delete(itemId: String): Promise<T> {
    return this.http.delete<T>(
      `${this.rootAPI}/${this.serviceUrl}DeleteData?idvalue=${itemId}`
    );
  }

  /**
   * Xóa 1 bản ghi
   * @param item dl bản ghi
   * @returns Promise T
   * Created by: MTLUC - 01/11/2021
   */
  public DeleteData(itemId: String): Promise<T> {
    return this.http.delete<T>(
      `${this.rootAPI}/${this.serviceUrl}Delete?prKey=${itemId}`
    );
  }

  /**
   * Duyệt chứng từ
   * Create by : TAN.VU
   * @param prKey
   * @returns trả về chứng từ hiện tại
   */
  public approve(prKey: String, tranId?: string): Promise<T> {
    return this.http.post<T>(
      `${this.rootAPI}/${this.serviceUrl}Approve?prKey=${prKey}&tranid=${tranId}`
    );
  }

  /**
   * Duyệt chứng từ
   * Create by : TAN.VU
   * @param prKey
   * @returns trả về chứng từ hiện tại
   */
  public withdraw(prKey: String, tranId?: string): Promise<T> {
    return this.http.post<T>(
      `${this.rootAPI}/${this.serviceUrl}Withdraw?prKey=${prKey}&tranid=${tranId}`
    );
  }

  /**
   * Duyệt chứng từ
   * Create by : TAN.VU
   * @param prKey
   * @returns trả về chứng từ hiện tại
   */
  public nextRecord(param: any): Promise<T> {
    return this.http.get<T>(
      `${this.rootAPI}/${this.serviceUrl}GetDataNextRecord`,
      param
    );
  }

  /**
   * Duyệt chứng từ
   * Create by : TAN.VU
   * @param prKey
   * @returns trả về chứng từ hiện tại
   */
  public previousRecord(param: any): Promise<T> {
    return this.http.get<T>(
      `${this.rootAPI}/${this.serviceUrl}GetDataPreviousRecord`,
      param
    );
  }

  /**
   * Load danh mục
   * @param tableName tên bảng
   * @param param param query
   * @returns Promise K[]
   */
  public loadDm<K>(tableName: string, param?: any): Promise<K[]> {
    return this.http.get<K[]>(`${this.rootAPI}/api/Dm/GetAllData`, {
      ...param,
      tablename: tableName,
    });
  }

  public getTranConfig(tranid: string): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetSysTranConfig`,
      { tranid: tranid }
    );
  }

  /**
   *Lấy danh sách trạng thái của chứng từ
   * @param tranid
   * @returns
   */
  public getTranStatus(tranid: string): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetTranStatus`,
      { tranid: tranid }
    );
  }

  /**
   *
   * @param prKey Lấy Data theo PrKey
   * @returns
   */
  public GetDataByPrKey(prKey: any): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetDataByID`,
      {
        idvalue: prKey,
      }
    );
  }

  /**
   * Lọc dữ liệu theo id và 1 số điều kiện lọc khác
   * @param idvalue :
   * @param filter
   * @returns
   */
  public GetDataByFilter(filter: any): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetDataByFilter`,
      {
        filter: filter,
      }
    );
  }

  /**
   *
   * @param prKey Lấy Data theo tranno
   * @returns
   */
  public GetDataByTranNo(tranNo: any): Promise<any> {
    return this.http.get<any>(
      `${this.rootAPI}/${this.serviceUrl}/GetDataByTranNo`,
      {
        trannovalue: tranNo,
      }
    );
  }

  /**
   * lấy danh sách mẫu in
   * @param tranId id loại chứng từ
   * @returns
   * Created by: MTLUC - 03/12/2021
   */
  public getPrintTemplates(tranId: string): Promise<PrintTemplate[]> {
    return this.http.get<PrintTemplate[]>(
      `${this.rootAPI}/${this.serviceUrl}GetTranOutput`,
      {
        tranid: tranId,
      }
    );
  }

  /**
   * Created by: TAN.VU
   * @param tranId id loại chứng từ
   * @returns List các template import theo tranId
   * */
  public getImportTemplate(tranId: string, tableName: string): Promise<any> {
    return this.http.get<DmTemplate[]>(
      `${this.rootAPI}/${this.serviceUrl}GetImportTemplate`,
      {
        tranid: tranId,
        tablename: tableName,
      }
    );
  }

  /**
   *
   * @param item
   * @returns
   */
  public ImportExcel(item: any): Promise<any> {
    return this.http.post<any>(
      `${this.rootAPI}/${this.serviceUrl}ImportExcel`,
      item
    );
  }

  /**
   * Tạo file Excel Import
   * @param item
   * @returns
   */
  public CreateExcelFile(item: any): Promise<any> {
    return this.http.post<any>(
      `${this.rootAPI}/${this.serviceUrl}CreateExcelFile`,
      item
    );
  }

  /**
   * lấy file in
   * @param prKeyOutput id mẫu in
   * @param prKey id chứng từ
   * @returns
   */
  public getPrint(prKeyOutput: string, prKey: string): Promise<PrintFile> {
    return this.http.get<PrintFile>(`${this.rootAPI}/${this.serviceUrl}Print`, {
      prkeyoutput: prKeyOutput,
      prkey: prKey,
    });
  }

  public copyData(idValue: string): Promise<any> {
    return this.http.get<any>(`${this.rootAPI}/${this.serviceUrl}CopyData`, {
      idvalue: idValue,
    });
  }

  public GetBalanceTypeList(isdebitcredit: boolean): Promise<any> {
    return this.http.get<any>(`${this.rootAPI}/api/CommonFunction/GetBalanceTypeList`, {
      isdebitcredit: isdebitcredit,
    });
  }
  //#endregion
}
