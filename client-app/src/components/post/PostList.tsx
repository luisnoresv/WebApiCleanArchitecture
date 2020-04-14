import React from 'react';
import { useSelector } from 'react-redux';
import { RootState } from '../../app/store/rootReducer';
import { Segment, Header, Container, Item } from 'semantic-ui-react';
import { IPostState } from '../../app/models/Post';
import Post from './Post';

interface IProps { }

const PostList: React.FC<IProps> = () => {
  // const posts = useSelector((state: IPostState) => state.posts);
  const posts = useSelector((state: RootState) => state.postState.posts);

  return (
    <Segment>
      <Header textAlign="center" content="Posts" />
      <Container>
        <Segment>
          <Item.Group divided>{posts && posts.map((post) => <Post key={post.id} post={post} />)}</Item.Group>
        </Segment>
      </Container>
    </Segment>
  );
};

export default PostList;
