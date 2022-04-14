import type { CategoryDto, CreateCategoryDto, GetCategoryListDto, UpdateCategoryDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CategoryLookupDto } from '../khoa-hocs/models';
import { HttpHeaders } from '@angular/common/http';
import { localStor } from 'src/app/Services/local-storage.service';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
  apiName = 'Default';

  create = (input: CreateCategoryDto) =>
    this.restService.request<any, CategoryDto>({
      method: 'POST',
      url: `/api/app/category`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/category/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, CategoryDto>({
      method: 'GET',
      url: `/api/app/category/${id}`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getID = (id: string) =>
    this.restService.request<any, CategoryDto>({
      method: 'GET',
      url: `/api/app/category/${id}/async-by-id`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getCategoryLookup = () =>
    this.restService.request<any, ListResultDto<CategoryLookupDto>>({
      method: 'GET',
      url: `/api/app/category/category-lookup`,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  getList = (input: GetCategoryListDto) =>
    this.restService.request<any, PagedResultDto<CategoryDto>>({
      method: 'GET',
      url: `/api/app/category`,
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  update = (id: string, input: UpdateCategoryDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/category/${id}`,
      body: input,
      headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
