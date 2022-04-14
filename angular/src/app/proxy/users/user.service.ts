import type { CreateUserDto, LoginUserDto, Token } from './models';
import { RestService } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  apiName = 'Default';

  loginByInput = (input: LoginUserDto) =>
    this.restService.request<any, Token>({
      method: 'POST',
      url: `/api/app/user/login`,
      body: input,
    },
    { apiName: this.apiName });

  registerByInput = (input: CreateUserDto) =>
    this.restService.request<any, Token>({
      method: 'POST',
      url: `/api/app/user/register`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
