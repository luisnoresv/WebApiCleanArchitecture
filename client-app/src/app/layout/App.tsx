import React, { useState, useEffect, useCallback } from 'react';
import './App.css';
import { ToastContainer } from 'react-toastify';
import { useDispatch } from 'react-redux';
import ModalContainer from '../../components/common/modals/ModalContainer';
import Routes from '../config/Routes';
import { getToken } from '../config/auth/securityConfiguration';
import { getCurrentUser } from '../store/reducers/authSlice';
import LoadingComponent from './LoadingComponent';

const App = () => {
  const token = getToken();
  const dispatch = useDispatch();
  const [appLoaded, setAppLoaded] = useState(false);

  const handleGetCurrentUser = useCallback(async () => {
    await dispatch(getCurrentUser());
  }, [dispatch]);

  useEffect(() => {
    if (token) {
      handleGetCurrentUser().finally(() =>
        setAppLoaded(true));
    } else setAppLoaded(true);
  }, [handleGetCurrentUser, token]);

  if (!appLoaded) return <LoadingComponent content="Loading App..." />;
  return (
    <>
      <ModalContainer />
      <ToastContainer position='bottom-right' />
      <Routes />
    </>
  );
};

export default App;
