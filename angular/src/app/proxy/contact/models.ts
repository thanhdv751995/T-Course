import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface ContactDto extends EntityDto<string> {
  descriptionPrimary?: string;
  emailPrimary?: string;
  phonePrimary?: string;
  addressPrimary?: string;
  descriptionSub?: string;
  emailSub?: string;
  phoneSub?: string;
  addressSub?: string;
  url?: string;
}

export interface CreateContactDto {
  descriptionPrimary: string;
  emailPrimary: string;
  phonePrimary: string;
  addressPrimary: string;
  descriptionSub?: string;
  emailSub?: string;
  phoneSub?: string;
  addressSub?: string;
  url?: string;
}

export interface GetContactListDto extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface UpdateContactDto {
  descriptionPrimary: string;
  emailPrimary: string;
  phonePrimary: string;
  addressPrimary: string;
  descriptionSub?: string;
  emailSub?: string;
  phoneSub?: string;
  addressSub?: string;
  url?: string;
}
