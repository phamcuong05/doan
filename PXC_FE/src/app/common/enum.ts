/**
 * Enum kiểu dữ liệu
 */
export enum EnumDataType {
  /**
   * Không xác định
   */
  ANY,
  STRING,
  NUMBER,
  DATE,
  DECIMAL,
  BOOLEAN,
}

/**
 * Enum kiểu column
 */
 export enum EnumColumnType {
  /**
   * Không xác định
   */
  ANY,
  STRING,
  NUMBER,
  DATE,
  DECIMAL,
  BOOLEAN,
  COMBOBOX,
  COMBOBOX_POPUP,
}

/**
 * Enum trạng thái load dữ liệu
 */
export enum EnumLoadingState {
  /**
   * Khởi tạo
   */
  Init,

  /**
   * Đang load
   */
  Loading,

  /**
   * Hoàn thành
   */
  Complete,
}

/**
 * Enum Id ngôn ngữ
 */
export enum EnumLangID {
  /**
   * Tiếng việt
   */
  VIE,

  /**
   * Tiếng anh
   */
  ENG,
}

/**
 * enum trạng thái process
 */
export enum ProcessState {
  Success,
  Error,
}


export enum toolbarAction{
  addRow = "addRow",
  removeRow = "removeRow",
  add="add",
  edit="edit",
  delete = "delete",
  refresh="refresh",
  duplicate="duplicate",
  excel="excel",
  import="import",
  close="close",
}

/**
 * enum action type
 */
export enum actionType {
  ADD = "ADD",
  EDIT = "EDIT",
  DELETE = "DELETE",
  DUPLICATE = "DUPLICATE",
  VIEW = "VIEW",
}