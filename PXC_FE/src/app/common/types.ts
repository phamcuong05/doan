/**
 * kiểu column
 */
export type ColumnType =
  | 'boolean'
  | 'numeric'
  | 'date'
  | 'combo'
  | 'multicolumncombobox'
  | 'textlookup'
  | 'default';

/**
 * Kiểu editor của column
 */
export type ColumnEditorType = 'text' | 'numeric' | 'date' | 'boolean';

export type ActionType =
  | 'VIEW' //Xem
  | 'ADD' //Thêm
  | 'EDIT' //Sửa
  | 'DELETE' //Xóa
  | 'DUPLICATE' //Nhân bản
  | 'SAVE' //Lưu
  | 'CLOSE' //Đóng
  | 'REFRESH' //Nạp
  | 'IMPORT_EXCEL' //Nhập khẩu Excel
  | 'EXPORT_EXCEL' //Export Excel
  | 'DOCUMENT' //Tài liệu
  | 'PRINT' //In
  | 'APPROVE' //Duyệt
  | 'WITHDRAW' //Hủy duyệt
  | 'EMAIL' //Email
  | 'ADD_ROW' //Thêm dòng
  | 'REMOVE_ROW' //Xóa dòng
  | 'UPLOAD' //Upload
  | 'DOWNLOAD' //Download
  | 'PREVIOUS_RECORD' //Previous
  | 'NEXT_RECORD' //Next
  | 'UNDO' //Undo
  | 'NONE';
