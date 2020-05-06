import React from 'react';
import { Segment, Container, Header, Button, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { getToken } from '../../app/config/auth/securityConfiguration';
import { RootState } from '../../app/store/rootReducer';
import { openModal } from '../../app/store/reducers/modalSlice';
import LoginForm from '../auth/LoginForm';

interface IProps { }

const HomePage: React.FC<IProps> = () => {
   const user = useSelector((state: RootState) => state.authState.user);
   const token = getToken();
   const dispatch = useDispatch();

   return (
      <Segment inverted textAlign="center" vertical className="masthead">
         <Container text>
            <Header as="h1" inverted>
               <Icon name="list alternate outline" /> Post App
            </Header>
            {user && token ? (
               <>
                  <Header as="h2" inverted content="Welcome back username" />
                  <Button as={Link} to="/posts" size="huge" inverted>
                     Go to the latest Posts
                  </Button>
               </>
            ) : (
                  <>
                     <Header as="h2" inverted content="Welcome to CookBook" />
                     <Button onClick={() => dispatch(openModal({ body: <LoginForm /> }))} size="huge" inverted>
                        Login
                     </Button>
                     <Button onClick={() => dispatch(openModal({ body: <div>Register Form</div> }))} size="huge" inverted>
                        Register
                     </Button>
                  </>
               )}
         </Container>
      </Segment>
   );
};


export default HomePage;
