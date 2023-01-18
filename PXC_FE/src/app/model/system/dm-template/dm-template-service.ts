import { Injectable } from '@angular/core';
import { HttpServiceModule } from 'src/app/common/http-service.module';
import { BaseService } from '../../base/BaseService';
import { DmTemplate } from './dm-template';
import { DmTemplateDetail } from './dm-template-detail';

export interface DmTemplateManager {
  FieldName: string;
  dmTemplate: DmTemplate;
  dmTemplateDetail: DmTemplateDetail[];
}

@Injectable({
  providedIn: 'root',
})
export class DmTemplateService extends BaseService<DmTemplateManager> {
  readonly serviceUrl = 'api/Dm_Template/';
  constructor(_http: HttpServiceModule) {
    super(_http);
    this.http = _http;
  }
}
