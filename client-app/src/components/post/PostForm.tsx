import React, { useEffect } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { combineValidators, isRequired, composeValidators, hasLengthGreaterThan } from 'revalidate';
import { Grid, Segment, Form, Button } from 'semantic-ui-react';
import { Form as FinalForm, Field } from 'react-final-form';
import { detailPost } from '../../app/store/reducers/postSlice';
import { RootState } from '../../app/store/rootReducer';
import TextInput from '../common/form/TextInput';
import TextAreaInput from '../common/form/TextAreaInput';

const validate = combineValidators({
  displayName: isRequired({ message: 'The displayName is required' }),
  userName: isRequired('userName'),
  photoUrl: composeValidators(
    isRequired('photoUrl'),
    hasLengthGreaterThan(4)({
      message: 'Photo Url needs to be at least 5 characters'
    })
  )(),
  title: isRequired('title'),
  content: isRequired('content')
});

interface IProps {
  id: string;
}

const PostForm: React.FC<RouteComponentProps<IProps>> = ({ match, history }) => {
  const dispatch = useDispatch();

  const post = useSelector((state: RootState) => state.postState.post);

  useEffect(() => {
    dispatch(detailPost({ id: match.params.id }));
  });

  const handleFinalFormSubmit = (values: any) => {
    console.log('values: ', values);
  };

  return (
    <Grid>
      <Grid.Column width={10}>
        <Segment clearing>
          <FinalForm
            validate={validate}
            initialValues={post}
            onSubmit={handleFinalFormSubmit}
            render={({ handleSubmit, invalid, pristine }) => (
              <Form onSubmit={handleSubmit}>
                <Field
                  placeholder='DisplayName'
                  value={post?.displayName}
                  name='displayName'
                  component={TextInput}
                />
                <Field
                  placeholder='UserName'
                  value={post?.userName}
                  name='userName'
                  rows={3}
                  component={TextInput}
                />
                <Field
                  placeholder='PhotoURL'
                  value={post?.photoUrl}
                  name='photoUrl'
                  component={TextInput}
                />
                <Field
                  placeholder='Title'
                  value={post?.title}
                  name='title'
                  component={TextInput}
                />
                <Field
                  placeholder='Content'
                  value={post?.content}
                  name='content'
                  component={TextAreaInput}
                />
                <Button
                  disabled={invalid || pristine}
                  floated='right'
                  positive
                  type='submit'
                  content='Submit'
                />
                <Button
                  floated='right'
                  // disabled={loading}
                  color='grey'
                  type='button'
                  content='Cancel'
                  onClick={
                    post?.id ?
                      () => history.push(`/posts/${post?.id}`) :
                      () => history.push('/posts')
                  }
                />
              </Form>
            )}
          />
        </Segment>
      </Grid.Column>
    </Grid>
  );
};

export default PostForm;
