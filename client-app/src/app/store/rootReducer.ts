import { combineReducers } from '@reduxjs/toolkit';
import postReducer from './reducers/postSlice';
import modalReducer from './reducers/modalSlice';
import authReducer from './reducers/authSlice';

const rootReducer = combineReducers({
   postState: postReducer,
   modalState: modalReducer,
   authState: authReducer
});

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
