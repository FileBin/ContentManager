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
    name?: string;
    description?: string;
    tags?: string[];

    preview_uuid: string;
    
    contentVariants?: ContentVariantResponse[];
    isPublic: boolean;
    isDraft: boolean;
    canUsersEditTags?: boolean;
    readerGroupName?: string;
    editorGroupName?: string;
    ownerGroupName?: string;
}