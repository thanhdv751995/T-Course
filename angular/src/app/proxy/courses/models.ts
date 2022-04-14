import type { EntityDto } from '@abp/ng.core';

export interface AttachmentLookupDto extends EntityDto<string> {
  url?: string;
}
