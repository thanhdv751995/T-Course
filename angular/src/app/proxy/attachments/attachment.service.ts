import type { AttachmentDto, CreateAttachmentDto, GetAttachmentListDto, UpdateAttachmentDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { localStor } from 'src/app/Services/local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class AttachmentService {
  apiName = 'Default';

  create = (input: CreateAttachmentDto) =>
    this.restService.request<any, AttachmentDto>({
      method: 'POST',
      url: `/api/app/attachment`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/attachment/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, AttachmentDto>({
      method: 'GET',
      url: `/api/app/attachment/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getList = (input: GetAttachmentListDto) =>
    this.restService.request<any, PagedResultDto<AttachmentDto>>({
      method: 'GET',
      url: `/api/app/attachment`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateAttachmentDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/attachment/${id}`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
