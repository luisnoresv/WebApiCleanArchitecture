import React from 'react';
import { Form as FinalForm, Field } from 'react-final-form';
import { Form, Button, Header } from 'semantic-ui-react';
import { FORM_ERROR } from 'final-form';
import { combineValidators, isRequired } from 'revalidate';
import { useDispatch } from 'react-redux';
import { unwrapResult } from '@reduxjs/toolkit';
import { toast } from 'react-toastify';
import { closeModal } from '../../app/store/reducers/modalSlice';
import TextInput from '../common/form/TextInput';
import { ILoginUser } from '../../app/models/User';
import ErrorMessage from '../common/form/ErrorMessage';
import { login } from '../../app/store/reducers/authSlice';
import history from '../..';


const validate = combineValidators({
  email: isRequired('email'),
  password: isRequired('password')
});

const LoginForm = () => {
  const dispatch = useDispatch();

  const handleLogin = async (values: ILoginUser) => {
    const resultAction: any = await dispatch(login(values));
    if (login.fulfilled.match(resultAction)) {
      const user = unwrapResult(resultAction);
      toast.success(`Welcome ${user.userName}`);
      history.push('/posts');
      dispatch(closeModal());
    } else return { [FORM_ERROR]: resultAction.error.message };
  };


  return (
    <FinalForm
      onSubmit={handleLogin}
      validate={validate}
      render={({
        handleSubmit,
        submitting,
        submitError,
        invalid,
        pristine,
        dirtySinceLastSubmit
      }) => (
          <Form onSubmit={handleSubmit} error>
            <Header
              as='h2'
              content='Add your credentials'
              color='teal'
              textAlign='center'
            />
            <Field name='email' component={TextInput} placeholder='Email' />
            <Field
              name='password'
              component={TextInput}
              placeholder='Password'
              type='password'
            />
            {submitError && !dirtySinceLastSubmit && (
              <ErrorMessage
                error={submitError}
                text='Invalid email or password provided'
              />
            )}
            <Button
              fluid
              disabled={(invalid && !dirtySinceLastSubmit) || pristine}
              loading={submitting}
              color='pink'
              content='Login'
            />
            {/* <pre>{JSON.stringify(form.getState(), null, 2)}</pre> */}
          </Form>
        )}
    />
  );
};

export default LoginForm;
