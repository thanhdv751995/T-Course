import type { RoleDto, CreateRoleDto, GetRoleListDto, UpdateRoleDto } from './models';
import {  RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import { localStor } from 'src/app/Services/local-storage.service';
@Injectable({
    providedIn: 'root',
})
export class RoleService {
    apiName = 'Default';

    create = (input: CreateRoleDto) =>
    this.restService.request<any, RoleDto>({
      method: 'POST',
      url: `/api/identity/roles`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

    delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/identity/roles/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

    get = (id: string) =>
    this.restService.request<any, RoleDto>({
      method: 'GET',
      url: `/api/identity/roles/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

    getList = (input: GetRoleListDto) =>
    this.restService.request<any, PagedResultDto<RoleDto>>({
      method: 'GET',
      url: `/api/identity/roles`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

    update = (id: string, input: UpdateRoleDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/identity/roles/${id}`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });
    constructor(private restService: RestService) {}
}