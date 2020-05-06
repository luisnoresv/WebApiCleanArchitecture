import { IErrorResponse } from './Errors';

export interface IPost {
   id: string;
   displayName: string;
   userName: string;
   photoUrl: string;
   title: string;
   content: string;
   postedOn: string;
}

export interface IPagination {
   currentPage: number;
   itemsPerPage: number;
   totalItems: number;
   totalPages: number;
}

export interface IPostState {
   posts: IPost[];
   post: IPost | null;
   error: IErrorResponse | null;
   pagination: IPagination | null;
}
