import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface AttachmentDto extends EntityDto<string> {
  url?: string;
  idTable?: string;
}

export interface CreateAttachmentDto {
  url: string;
  idTable: string;
}

export interface GetAttachmentListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateAttachmentDto {
  url: string;
  idTable: string;
}
