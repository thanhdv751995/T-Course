import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { CreateUserDto, GetUserListDto, UpdateUserDto, UserDto } from './models';
import { HttpHeaders } from '@angular/common/http';
import { localStor } from 'src/app/Services/local-storage.service';
@Injectable({
    providedIn: 'root',
})
export class UserService {
    apiName = 'Default';
    get = (id: string) =>
        this.restService.request<any, UserDto>({
            method: 'GET',
            url: `/api/identity/users/${id}`,
            headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
        },
            { apiName: this.apiName });
    getList = (input: GetUserListDto) =>
        this.restService.request<any, PagedResultDto<UserDto>>({
            method: 'GET',
            url: `/api/identity/users`,
            params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount=1000 },
            headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
        },
            { apiName: this.apiName });
    create = (input: CreateUserDto) =>
        this.restService.request<any, UserDto>({
            method: 'POST',
            url: `/api/app/user/register`,
            body: input,
            headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
        },
            { apiName: this.apiName });

    delete = (id: string) =>
        this.restService.request<any, void>({
            method: 'DELETE',
            url: `/api/identity/users/${id}`,
            headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
        },
            { apiName: this.apiName });
    update = (id: string, data: UpdateUserDto) =>
    this.restService.request<any, void>({
    method: 'PUT',
    url: `/api/identity/users/${id}/roles`,
    body: data,
    headers: new HttpHeaders({'Authorization':`Bearer ${localStor.getToken()}`})
    },
    { apiName: this.apiName });
    constructor(private restService: RestService) { }
}