// eslint-disable-next-line object-curly-newline
import { createSlice, PayloadAction, configureStore, getDefaultMiddleware } from '@reduxjs/toolkit';
import { logger } from 'redux-logger';
import { IPost, IPostState } from '../models/Post';

const initialState: IPostState = {
   posts: [
      {
         id: 'ebf8f6f1-8cd1-431d-94cf-caa8d8f4fc17',
         displayName: 'Ander',
         userName: '@ander27',
         photoUrl: 'https://randomuser.me/api/portraits/men/19.jpg',
         title: 'sunt aut facere repellat provident occaecati excepturi optio reprehenderit',
         content:
            'quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto',
         postedOn: '27/09/2019'
      },
      {
         id: 'df00321d-4db3-4c6c-bd4b-2f920c47de99',
         displayName: 'Michael',
         userName: '@deadHorse',
         photoUrl: 'https://randomuser.me/api/portraits/men/31.jpg',
         title: 'qui est esse',
         content:
            'est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla',
         postedOn: '13/04/2020'
      },
      {
         id: 'a14d8f97-3203-417c-a4a9-5360197e712f',
         displayName: 'Daniel',
         userName: '@dboy27',
         photoUrl: 'https://randomuser.me/api/portraits/men/45.jpg',
         title: 'ea molestias quasi exercitationem repellat qui ipsa sit aut',
         content:
            'et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut',
         postedOn: '11/02/2020'
      }
   ],
   post: null,
   pagination: null,
   error: null
};

const postSlice = createSlice({
   name: 'posts',
   initialState,
   reducers: {
      detail: (state, { payload }: PayloadAction<{ id: string }>) => {
         const selectedPost = state.posts.find((post) => post.id === payload.id);
         if (selectedPost) state.post = selectedPost;
      },
      create: (state, { payload }: PayloadAction<IPost>) => {
         state.posts.push(payload);
      },
      edit: (state, { payload }: PayloadAction<IPost>) => {
         const postToEdit = state.posts.find((post) => post.id === payload.id);
         if (postToEdit) {
            postToEdit.displayName = payload.displayName;
            postToEdit.userName = payload.userName;
            postToEdit.photoUrl = payload.photoUrl;
            postToEdit.title = payload.title;
            postToEdit.content = payload.content;
         }
      },
      delete: (state, { payload }: PayloadAction<{ id: string }>) => {
         const index = state.posts.findIndex((p) => p.id === payload.id);
         if (index > -1) state.posts.splice(index, 1);
      }
   }
});

export const {
   detail: detailPostActionCreator,
   create: createPostActionCreator,
   edit: editPostActionCreator,
   delete: removePostActionCreator
} = postSlice.actions;

const middleware = [...getDefaultMiddleware(), logger];

export default configureStore({
   reducer: postSlice.reducer,
   middleware,
   devTools: process.env.NODE_ENV !== 'production'
});
