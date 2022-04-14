import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ContactDto, CreateContactDto, GetContactListDto, UpdateContactDto } from '../contact/models';
import { HttpHeaders } from '@angular/common/http';
import { localStor } from 'src/app/Services/local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  apiName = 'Default';

  create = (input: CreateContactDto) =>
    this.restService.request<any, ContactDto>({
      method: 'POST',
      url: `/api/app/contact`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/contact/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ContactDto>({
      method: 'GET',
      url: `/api/app/contact/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getList = (input: GetContactListDto) =>
    this.restService.request<any, PagedResultDto<ContactDto>>({
      method: 'GET',
      url: `/api/app/contact`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateContactDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/contact/${id}`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
