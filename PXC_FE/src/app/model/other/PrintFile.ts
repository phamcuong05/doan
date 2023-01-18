/**
 * Thông tin file in
 * Created by: MTLUC - 09/12/2021
 */
export interface PrintFile {
  /**
   * Đường dẫn file pdf - file này để in
   * Created by: MTLUC - 09/12/2021
   */
  PDF_FILE: string | undefined;

  /**
   * Đường dẫn file excel - Export excel
   * Created by: MTLUC - 09/12/2021
   */
  EXCEL_FILE: string | undefined;

  /**
   * Đường dẫn file word - Export word
   * Created by: MTLUC - 09/12/2021
   */
  DOC_FILE: string | undefined;

  ORTHER_FILE: string | undefined;
}
