import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { IUserState, ILoginUser, IUser } from '../../models/User';
import AuthService from '../../api/authService';

const initialState: IUserState = {
   user: null,
   error: null
};

export const login = createAsyncThunk<IUser, ILoginUser>('auth/login', async (credentials) => {
   const response = await AuthService.login(credentials);
   return response;
});

export const getCurrentUser = createAsyncThunk('auth/getCurrentUser', async () => {
   const user = await AuthService.currentUser();
   return user;
});

const authSlice = createSlice({
   name: 'auth',
   initialState,
   reducers: {
      logout: (state) => {
         AuthService.logout();
         state.user = null;
      }
   },
   extraReducers: (builder) => {
      builder.addCase(login.fulfilled, (state, { payload }) => {
         state.user = payload;
      });
      builder.addCase(login.rejected, (state, { error }) => {
         state.error = error;
      });
      builder.addCase(getCurrentUser.fulfilled, (state, { payload }) => {
         state.user = payload;
      });
      builder.addCase(getCurrentUser.rejected, (state, { error }) => {
         state.error = error;
      });
   }
});

export const { logout } = authSlice.actions;

export default authSlice.reducer;
