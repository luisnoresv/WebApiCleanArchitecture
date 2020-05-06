import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IModal } from '../../models/Modal';

const initialState: IModal = {
   open: false,
   body: null
};

const modalSlice = createSlice({
   name: 'modal',
   initialState,
   reducers: {
      openModal: (state, { payload }: PayloadAction<{ body: any }>) => {
         state.body = payload.body;
         state.open = true;
      },
      closeModal: (state) => {
         state.open = false;
         state.body = null;
      }
   }
});

export const { openModal, closeModal } = modalSlice.actions;

export default modalSlice.reducer;
