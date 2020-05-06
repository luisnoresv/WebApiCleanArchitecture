import { createSlice, PayloadAction, createAsyncThunk } from '@reduxjs/toolkit';
import { IErrorResponse } from '../../models/Errors';
import PostService from '../../api/postService';
import { IPagination } from '../../models/Pagination';
import { IPostState, IPost } from '../../models/Post';

const initialState: IPostState = {
   posts: [],
   post: null,
   error: null,
   pagination: null
};

// Create thunk
export const fetchPosts = createAsyncThunk<
   { body: any; headers: any },
   IPagination,
   { rejectValue: IErrorResponse }
>('posts/fetchPost', async (pagination: IPagination, thunkApi) => {
   try {
      const posts = await PostService.list(pagination);
      return posts;
   } catch (error) {
      return thunkApi.rejectWithValue(error as IErrorResponse);
   }
});

const postSlice = createSlice({
   name: 'posts',
   initialState,
   reducers: {
      detailPost: (state, { payload }: PayloadAction<{ id: string }>) => {
         const selectedPost = state.posts.find((post) => post.id === payload.id);
         if (selectedPost) state.post = selectedPost;
      },
      createPost: (state, { payload }: PayloadAction<IPost>) => {
         state.posts.push(payload);
      },
      editPost: (state, { payload }: PayloadAction<IPost>) => {
         const postToEdit = state.posts.find((post) => post.id === payload.id);
         if (postToEdit) {
            postToEdit.displayName = payload.displayName;
            postToEdit.userName = payload.userName;
            postToEdit.photoUrl = payload.photoUrl;
            postToEdit.title = payload.title;
            postToEdit.content = payload.content;
         }
      },
      deletePost: (state, { payload }: PayloadAction<{ id: string }>) => {
         const index = state.posts.findIndex((p) => p.id === payload.id);
         if (index > -1) state.posts.splice(index, 1);
      }
   },
   extraReducers: (builder) => {
      builder.addCase(fetchPosts.fulfilled, (state, { payload }) => {
         state.posts = payload.body;
         state.pagination = JSON.parse(payload.headers.pagination);
      });
      builder.addCase(fetchPosts.rejected, (state, { payload, error }) => {
         if (payload) {
            const { data, status, statusText } = payload;
            state.error = { data, status, statusText };
         } else if (state.error) state.error.statusText = error.message;
      });
   }
});

export const { createPost, deletePost, detailPost, editPost } = postSlice.actions;

export default postSlice.reducer;
