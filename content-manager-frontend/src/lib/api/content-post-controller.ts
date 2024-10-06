import axios from "axios";

import config from '@/config.json';
import type {
  ContentPostCreateRequest,
  ContentPostResponse,
  ContentPostUpdateRequest,
  PageDesc
} from "./models";

const BASE_URL = `${config.apiUrl}/api/posts`;


export const getPage = async (pageDesc: PageDesc) => {
  const response = await axios.get<ContentPostResponse[]>(BASE_URL, { params: pageDesc });
  return response.data;
};

export const getCount = async () => {
  const response = await axios.get<number>(`${BASE_URL}/count`);
  return response.data;
};

export const getPost = async (id: string) => {
  const response = await axios.get<ContentPostResponse>(`${BASE_URL}/${id}`);
  return response.data;
};

export const createPost = async (createRequest: ContentPostCreateRequest) => {
  const response = await axios.post<string>(BASE_URL, createRequest);
  return response.data;
};

export const updatePost = async (id: string, updateRequest: ContentPostUpdateRequest) => {
  await axios.put(`${BASE_URL}/${id}`, updateRequest);
};

export const deletePost = async (id: string) => {
  await axios.delete(`${BASE_URL}/${id}`);
};