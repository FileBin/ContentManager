import axios from "axios";

import config from '@/config.json';

const BASE_URL = `${config.apiUrl}/api/posts`;

export function getContentUrlByUuid(content_uuid: string): string {
    return `${BASE_URL}/contents/${content_uuid}`;
}

export function getContentUrlByPost(postId: string, postOrder: number): string {
    return `${BASE_URL}/${postId}/contents/${postOrder}`
}

export const uploadContent = async (postId: string, file: File): Promise<string> => {
    const formData = new FormData();
    formData.append('file', file);
    const response = await axios.post<string>(`${BASE_URL}/${postId}/contents`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
    return response.data;
};
