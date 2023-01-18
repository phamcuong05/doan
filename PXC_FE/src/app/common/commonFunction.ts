import { FTSMain } from '../base/ftsmain';
import { Period } from '../model/other/period';
import { ResourceManager } from './resource-manager';

/**
 * các function dùng chung
 */
export const commonFunction = {
  /**
   * format chuỗi string "{0} - {1} ... {n}"
   * @param str
   * @param val
   * @returns
   */
  stringFormat(str: string, ...val: string[]) {
    for (let index = 0; index < val.length; index++) {
      str = str.replace(`{${index}}`, val[index]);
    }
    return str;
  },

  /**
   * Lấy giá trị đầu ngày
   * @param day
   * @returns
   */
  getStartDay(day: Date) {
    return new Date(day.getFullYear(), day.getMonth(), day.getDate());
  },

  /**
   * Lấy giá trị cuối ngày
   * @param date
   * @returns
   */
  getEndDay(date: Date) {
    return new Date(
      date.getFullYear(),
      date.getMonth(),
      date.getDate(),
      23,
      59,
      59,
      999
    );
  },

  /**
   * add day
   * @param date
   * @param daynumber
   * @returns
   */
  addDay(date: Date, daynumber: number) {
    return new Date(
      date.getFullYear(),
      date.getMonth(),
      date.getDate() + daynumber,
      date.getHours(),
      date.getMinutes(),
      date.getSeconds(),
      date.getMilliseconds()
    );
  },

  /**
   * Lấy ngày thứ 2
   * @param d
   * @returns
   */
  getMonday(d: Date) {
    d = new Date(d);
    var day = d.getDay(),
      diff = d.getDate() - day + (day == 0 ? -6 : 1);
    return new Date(d.setDate(diff));
  },

  /**
   * Lấy data kỳ số liệu
   */
  getPeriod(ftsMain: FTSMain, resourceManager: ResourceManager, isAny: boolean = true): Array<Period> {
    const that = this;
    let result: Array<Period> = [];
    const dateNow = new Date();
    const startDay = that.getStartDay(dateNow);
    const endDay = that.addDay(startDay, 1);
    const workingYear = ftsMain.WorkingYear;
    let thisQuarter = Math.floor((dateNow.getMonth() + 1) / 3) + 1;
    thisQuarter = thisQuarter > 4 ? 4 : thisQuarter;

    const resource = resourceManager.CommonResource.MyResource;
    if (isAny) {
      // Tuỳ ý
      result.push({
        Id: 'Any',
        Text: resource.Any,
        FromDate: startDay,
        ToDate: startDay,
      });
    }
    //Tháng 1 -> 12
    for (let index = 0; index < 12; index++) {
      result.push({
        Id: `Month${index + 1}`,
        Text:
          resource[`Month${index + 1}`] +
          ' ' +
          resource[`Year`] +
          ' ' +
          workingYear,
        FromDate: new Date(workingYear, index, 1),
        ToDate: that.addDay(new Date(workingYear, index + 1, 1), -1),
      });
    }
    //Quý 1 -> 4
    for (let index = 0; index < 4; index++) {
      result.push({
        Id: `Quarter${index + 1}`,
        Text:
          resource[`Quarter${index + 1}`] +
          ' ' +
          resource[`Year`] +
          ' ' +
          workingYear,
        FromDate: new Date(workingYear, 3 * index, 1),
        ToDate: that.addDay(new Date(workingYear, 3 * (index + 1), 1), -1),
      });
    }
    //6 tháng
    result.push({
      Id: 'SixMonth',
      Text: resource.SixMonth + ' ' + workingYear,
      FromDate: new Date(workingYear, 0, 1),
      ToDate: that.addDay(new Date(workingYear, 6, 1), -1),
    });
    //9 tháng
    result.push({
      Id: 'NineMonth',
      Text: resource.NineMonth + ' ' + workingYear,
      FromDate: new Date(workingYear, 0, 1),
      ToDate: that.addDay(new Date(workingYear, 9, 1), -1),
    });
    //Cả năm
    result.push({
      Id: `AllYear`,
      Text: resource.AllYear + ' ' + workingYear,
      FromDate: new Date(workingYear, 0, 1),
      ToDate: that.addDay(new Date(workingYear, 12, 1), -1),
    });
    return result;
  },

  /**
   * Tạo 1 chuỗi GUID
   * @returns GUID string
   */
  newGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(
      /[xy]/g,
      function (c) {
        var r = (Math.random() * 16) | 0,
          v = c == 'x' ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      }
    );
  },

  getControlName(formControls: any, formControl: any){
    if(!formControls || !formControl){
      return '';
    }

    const keys = Object.keys(formControls);

    for (let i = 0; i < keys.length; i++) {
      if(formControls[keys[i]] == formControl){
        return keys[i];
      }
    }
    return '';
  },

  /**
   * matches element
   * @param el
   * @param selector
   * @returns
   */
  matches(el: any, selector: string) {
    return (el.matches || el.msMatchesSelector).call(el, selector);
  },

  setDarkMode(used: boolean) {
    const bodyEl = document.getElementsByTagName('body')[0];
    if (used) {
      bodyEl.classList.add('dark-mode');
    } else {
      bodyEl.classList.remove('dark-mode');
    }
  },

  setFontSize(fontSize: number) {
    const bodyEl = document.getElementsByTagName('body')[0];
    bodyEl.setAttribute('style', '');
    bodyEl.setAttribute(
      'style',
      bodyEl.getAttribute('style') +
      '--font-size-small: ' +
      (fontSize - 3) +
      'px;'
    );
    bodyEl.setAttribute(
      'style',
      bodyEl.getAttribute('style') + '--font-size-nomal: ' + fontSize + 'px;'
    );
    bodyEl.setAttribute(
      'style',
      bodyEl.getAttribute('style') +
      '--font-size-large: ' +
      (fontSize + 4) +
      'px;'
    );
    bodyEl.setAttribute(
      'style',
      bodyEl.getAttribute('style') +
      '--font-size-extralarge: ' +
      (fontSize + 8) +
      'px;'
    );
  },

  removeVietnameseTones(text: string) {
    return text.normalize('NFD').replace(/[\u0300-\u036f]/g, '');
  },

  blobToBase64(data: Blob) {
    return new Promise<string>((resolve) => {
      const reader = new FileReader();
      reader.onloadend = () => resolve(reader.result as string);
      reader.readAsDataURL(data);
    });
  },

  printFile(data: Blob) {
    const iframePrintTag = document.getElementById(
      'iframePrint'
    ) as HTMLIFrameElement;
    iframePrintTag.src = URL.createObjectURL(data);

    setTimeout(() => {
      (iframePrintTag as any).contentWindow.focus();
      (iframePrintTag as any).contentWindow.print();
    }, 100);
  },

  /**
   * @returns Lấy tiêu đề chức năng hiện tại tại class: page-title
   */
  getPageTitle(): string {
    let title: string = 'PAGE_TITLE_NONE';
    let pageTitle =
      document.getElementsByClassName('page-title')[0].textContent;
    if (pageTitle != null) {
      title = pageTitle;
    }
    return title;
  },

  /**
   * làm tròn số
   * @param num
   * @param dp
   * @returns
   */
  round(num: object, dp: number) {
    let numToFixedDp = Number(num).toFixed(dp);
    return Number(numToFixedDp);
  },

  parseQueryString(url: string) {
    let queryParams: any = {};
    if (url) {
      const idx = url.indexOf('?');
      if (idx >= 0) {
        url = url.slice(idx + 1, url.length);
        const vars = url.split('&');
        for (var i = 0; i < vars.length; i++) {
          const pair = vars[i].split('=');
          const key = decodeURIComponent(pair.shift() || '');
          const value = decodeURIComponent(pair.join('='));
          if (!queryParams.hasOwnProperty(key)) {
            queryParams[key] = value;
          }
        }
      }
    }
    return queryParams;
  },
};
