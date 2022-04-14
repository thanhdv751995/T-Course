import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface RoleDto extends EntityDto<string> {
    name?: string;
    concurrencyStamp?: string;
}
export interface CreateRoleDto {   
    name: string;
}
export interface GetRoleListDto extends PagedAndSortedResultRequestDto {
    filter?: string;
}
export interface UpdateRoleDto {
    name: string;
    concurrencyStamp?: string;
}