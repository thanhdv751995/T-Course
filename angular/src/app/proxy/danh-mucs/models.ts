import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CategoryDto extends EntityDto<string> {
  idParent?: string;
  panrentName?: string;
  name?: string;
  description?: string;
  url?: string;
  fileName?: string;
}

export interface CreateCategoryDto {
  idParent?: string;
  name: string;
  description: string;
}

export interface GetCategoryListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateCategoryDto {
  idParent?: string;
  name: string;
  description: string;
}
