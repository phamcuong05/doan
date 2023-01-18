import { FtsException } from './fts-exception';

/**
 * Xử lý exception
 */
export class ExceptionManager {
  constructor() {}

  /**
   * Lấy lỗi trả về trong exception
   * @param err
   * @returns
   */
  processException(err: any): string {
    let ftsException: FtsException = JSON.parse(err.error);

    return (
      ftsException.mMessage ||
      (ftsException as any).MessageDetail ||
      (ftsException as any).Message
    );
  }
}
