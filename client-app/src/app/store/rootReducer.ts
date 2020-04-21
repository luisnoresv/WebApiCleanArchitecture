import { combineReducers } from '@reduxjs/toolkit';
import postReducer from './reducers/postSlice';

const rootReducer = combineReducers({
  postState: postReducer
});

export type RootState = ReturnType<typeof rootReducer>;

export default rootReducer;
