import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { Modal } from 'semantic-ui-react';
import { RootState } from '../../../app/store/rootReducer';
import { closeModal } from '../../../app/store/reducers/modalSlice';

interface IProps {
   modalSize?: any;
}

const ModalContainer: React.FC<IProps> = ({ modalSize = 'mini' }) => {
   const dispatch = useDispatch();
   const rootState = useSelector((state: RootState) => state);

   const { open, body } = rootState.modalState;

   const handleCloseModal = () => {
      dispatch(closeModal());
   };

   return (
      <Modal open={open} onClose={handleCloseModal} size={modalSize}>
         <Modal.Content>{body}</Modal.Content>
      </Modal>
   );
};

export default ModalContainer;
