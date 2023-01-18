import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { saveAs } from '@progress/kendo-file-saver';
import { FTSMain } from 'src/app/base/ftsmain';
import { FtsDialogService } from '../fts-dialog/fts-dialog.service';

@Injectable({
  providedIn: 'root',
})
export class FtsFileSaveService {
  constructor(
    private http: HttpClient,
    private ftsDialog: FtsDialogService,
    private ftsMain: FTSMain
  ) {}

  saveAsByUrl(url: string) {
    if (url) {
      const splitUrl = url.split('/');
      const fileName = splitUrl[splitUrl.length - 1];
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        responseType: 'blob',
      });

      this.http
        .get<Blob>(url, { headers: headers, responseType: 'blob' as 'json' })
        .toPromise()
        .then((data) => {
          saveAs(data, fileName);
        })
        .catch((err) => {
          this.ftsDialog.alert.show({
            icon: 'warning',
            maxWidth: 300,
            content: this.ftsMain.ExceptionManager.processException(err),
          });
          console.error(err);
        });
    }
  }

  saveAs(data: string | Blob, fileName: string) {
    saveAs(data, fileName);
  }

  getBase64File(url: string) {
    return new Promise<string>((resolve, rejects) => {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        responseType: 'blob',
      });

      this.http
        .get<Blob>(url, { headers: headers, responseType: 'blob' as 'json' })
        .toPromise()
        .then((blob) => {
          const reader = new FileReader();
          reader.onloadend = () => resolve(reader.result as string);
          reader.readAsDataURL(blob);
        })
        .catch((err) => {
          this.ftsDialog.alert.show({
            icon: 'warning',
            maxWidth: 300,
            content: this.ftsMain.ExceptionManager.processException(err),
          });
          rejects(err);
        });
    });
  }

  getBlobFile(url: string) {
    return new Promise<Blob>((resolve, rejects) => {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        responseType: 'blob',
      });

      this.http
        .get<Blob>(url, { headers: headers, responseType: 'blob' as 'json' })
        .toPromise()
        .then((blob) => {
          resolve(blob);
        })
        .catch((err) => {
          this.ftsDialog.alert.show({
            icon: 'warning',
            maxWidth: 300,
            content: this.ftsMain.ExceptionManager.processException(err),
          });
          rejects(err);
        });
    });
  }
}
