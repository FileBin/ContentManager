type ContentVariantResponse = { [order: number]: string };

export interface PageDesc {
  pageNumber: number;
  pageSize: number;
}

export interface ContentPostCreateRequest {
  name: string;
  description?: string;
  tags: string[];
  isPublic: boolean;
  isDraft: boolean;
  canUsersEditTags?: boolean;
  readerGroupName?: string;
  editorGroupName?: string;
  ownerGroupName?: string;
}

export interface ContentPostUpdateRequest {
  name?: string;
  description?: string;
  canUsersEditTags?: boolean;
  tags?: string[];
  readerGroupName?: string;
  editorGroupName?: string;
  ownerGroupName?: string;
}

export interface ContentPostResponse {
  id: string;
  name: string;
  description?: string;
  tags: string[];

  previewId: string;

  contentVariants: ContentVariantResponse[];
  isPublic: boolean;
  isDraft: boolean;
  canUsersEditTags?: boolean;
  readerGroupName?: string;
  editorGroupName?: string;
  ownerGroupName?: string;
}

export enum Quality {
  icon = 'icon',
  small = 'small',
  medium = 'medium',
  large = 'large',
  _2k='2k',
  _4k='4k',
}