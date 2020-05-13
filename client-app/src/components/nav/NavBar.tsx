import React from 'react';
import { Menu, Container, Icon, Button, Dropdown, Image } from 'semantic-ui-react';
import { Link, NavLink } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../../app/store/rootReducer';
import { logout } from '../../app/store/reducers/authSlice';
import history from '../..';


interface IProps { }

const NavBar: React.FC<IProps> = () => {
  const user = useSelector((state: RootState) => state.authState.user);
  const dispatch = useDispatch();

  const handlerLogout = () => {
    dispatch(logout());
    history.push('/');
  };

  return (
    <Menu fixed="top" inverted>
      <Container>
        <Menu.Item header as={Link} to="/">
          <Icon name="list alternate outline" /> Post App
        </Menu.Item>
        <Menu.Item name="Posts" as={Link} to="/posts" />
        <Menu.Item>
          <Button as={NavLink} to="/createPost" color="pink" content="Write a Post" />
        </Menu.Item>
        <Menu.Item position="right">
          <Image avatar spaced="right" src="/assets/user.png" />
          <Dropdown pointing="top left" text={user?.userName}>
            <Dropdown.Menu>
              <Dropdown.Item text="Logout" icon="log out" onClick={handlerLogout} />
            </Dropdown.Menu>
          </Dropdown>
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default NavBar;
