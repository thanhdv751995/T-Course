import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
export interface RoleLookupDto extends EntityDto<string> {
    name?: string;
}
export interface UserDto extends EntityDto<string> {
    userName?: string;
    name?: string;
    email?: string;
    concurrencyStamp?: string;
    creationTime?: Date;
    role: Role;
}
export enum Role {
    User = 'User',
    Admin = 'admin',
    
}
export interface CreateUserDto {
    userName: string;
    name: string;
    email: string;
    dateOfBirth: Date;
    password: string;
    avatar: string;
    role : string;
}
export interface GetUserListDto extends PagedAndSortedResultRequestDto {
    filter?: string;
}
export interface UpdateUserDto {
    roleName: Array<string> ,
}