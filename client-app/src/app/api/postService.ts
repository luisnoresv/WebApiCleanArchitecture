import { IPagination } from '../models/Pagination';
import { CURRENT_PAGE_PARAM, PAGE_SIZE_PARAM, POST_ENDPOINT } from '../infrastructure/appConstants';
import baseApi from './baseApi';
import { IPost } from '../models/Post';

const PostService = {
   list: (pagination: IPagination) => {
      const searchParams = new URLSearchParams();

      if (pagination.currentPage) {
         searchParams.append(CURRENT_PAGE_PARAM, pagination.currentPage.toString());
      }

      if (pagination.pageSize) {
         searchParams.append(PAGE_SIZE_PARAM, pagination.pageSize.toString());
      }

      return baseApi.listWithPaging(`${POST_ENDPOINT}`, searchParams);
   },
   detail: (id: string) => baseApi.get(`${POST_ENDPOINT}/${id}`),
   add: (post: IPost) => baseApi.post(POST_ENDPOINT, post),
   update: (post: IPost) => baseApi.put(POST_ENDPOINT, post),
   delete: (id: string) => baseApi.delete(`${POST_ENDPOINT}/${id}`)
};

export default PostService;
