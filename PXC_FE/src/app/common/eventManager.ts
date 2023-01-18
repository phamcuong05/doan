import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root',
})
/**
 * quản lý các event đăng ký vs window
 * khi sự kiện xảy ra trên window sẽ gọi event đăng ký gần nhất
 */
export class EventManager {
  /**
   * danh sách sự kiện keydown được đăng ký
   */
  private _keyDownSubcriber: Array<{
    id: string;
    function: Function;
  }> = [];

  public SubcriberKeyDown(id: string, _function: Function) {
    this.UnSubcriberKeyDown(id);
    this._keyDownSubcriber.push({ id: id, function: _function });
  }

  public UnSubcriberKeyDown(id: string) {
    this._keyDownSubcriber = this._keyDownSubcriber.filter((x) => x.id != id);
  }

  public EmitKeyDown(event: KeyboardEvent) {
    if (this._keyDownSubcriber.length > 0) {
      this._keyDownSubcriber[this._keyDownSubcriber.length - 1].function(event);
    }
  }
}
