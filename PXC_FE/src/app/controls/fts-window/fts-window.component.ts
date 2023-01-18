import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { commonFunction } from 'src/app/common/commonFunction';
import { EventManager } from 'src/app/common/eventManager';

@Component({
  selector: 'fts-window',
  templateUrl: './fts-window.component.html',
  styleUrls: ['./fts-window.component.scss'],
})
export class FtsWindowComponent implements OnInit {
  @ViewChild('btnMaximize') btnMaximizeRef!: ElementRef;

  /**
   * Tiêu đề.
   */
  @Input() title!: string;

  /**
   * Độ rộng tối thiểu.
   */
  @Input() minWidth!: number;

  /**
   * Chiều cao thối thiểu
   */
  @Input() minHeight!: number;

  /**
   * Độ rộng tối đa.
   */
  @Input() maxWidth!: number;

  /**
   * Chiều cao tối đa.
   */
  @Input() maxHeight!: number;

  /**
   * Độ rộng.
   */
  @Input() width!: number;

  /**
   * Chiều cao.
   */
  @Input() height!: number;

  /**
   * Có cho phép resize?.
   */
  @Input() resizable: boolean = false;

  /**
   * Có cho phép maximize?
   */
  @Input() maximize: boolean = false;

  /**
   * Trạng thái show.
   */
  @Input() show: boolean = false;

  @Output() showChange = new EventEmitter<boolean>();

  /**
   * function được gọi trước khi đóng cửa sổ.
   * @returns false sẽ hủy sự kiện đóng của sổ.
   */
  @Input() preventClose = () => {
    return true;
  };

  /**
   * Đóng cửa sổ.
   */
  @Input() close = () => {
    if (this.preventClose()) {
      this.show = false;
      this.showChange.emit(false);
    }
  };

  /**
   * id component
   */
  id = commonFunction.newGuid();

  private onDestroy$ = new Subject<void>();

  constructor(private _eventManager: EventManager) {}

  ngOnInit(): void {
    this.showChange.pipe(takeUntil(this.onDestroy$)).subscribe((isShow) => {
      if (isShow) {
        this.handleKeyDown();
      } else {
        this._eventManager.UnSubcriberKeyDown(this.id);
      }
    });
  }

  ngOnDestroy(): void {
    this._eventManager.UnSubcriberKeyDown(this.id);
    this.onDestroy$.next();
  }

  /**
   * Đăng ký sự kiện keydown trên window.
   */
  handleKeyDown() {
    const that = this;
    that._eventManager.SubcriberKeyDown(that.id, (e: KeyboardEvent) => {
      const strKey = e.key.toUpperCase();
      let stopEvt = false;

      //esc
      if (strKey == 'ESCAPE') {
        this.close();
        stopEvt = true;
      }

      //
      if (stopEvt) {
        e.preventDefault();
      }
    });
  }

  /**
   * Mở cửa sổ.
   */
  open() {
    this.show = true;
    this.showChange.emit(true);
  }

  get styleWapper(): string {
    let result: string = '';
    if (this.maxWidth) {
      result += 'max-width:' + this.maxWidth + 'px;';
    }
    if (this.maxHeight) {
      result += 'max-width:' + this.maxHeight + 'px;';
    }
    return result;
  }
}
