import React from 'react';
import { RouteComponentProps, Route, Switch, withRouter } from 'react-router-dom';
import { Container } from 'semantic-ui-react';
import { useSelector } from 'react-redux';
import PostForm from '../../components/post/PostForm';
import PostDetail from '../../components/post/PostDetail';
import NavBar from '../../components/nav/NavBar';
import PostList from '../../components/post/PostList';
import HomePage from '../../components/home/HomePage';
import NotFound from '../layout/NotFound';
import { RootState } from '../store/rootReducer';


const Routes: React.FC<RouteComponentProps> = ({ location }) => {
  const user = useSelector((state: RootState) => state.authState.user);
  return (
    <>
      <Route exact path='/' component={HomePage} />
      {user && (
        <Route
          path="/(.+)"
          render={() => (
            <>
              <NavBar />
              <Container style={{ marginTop: '7em' }}>
                <Switch>
                  <Route exact path="/" component={HomePage} />
                  <Route exact path='/posts' component={PostList} />
                  <Route path="/posts/:id" component={PostDetail} />
                  <Route key={location.key} path={['/createPost', '/manage/:id']} component={PostForm} />
                  <Route component={NotFound} />
                </Switch>
              </Container>
            </>
          )}
        />
      )}
    </>
  );
};

export default withRouter(Routes);
