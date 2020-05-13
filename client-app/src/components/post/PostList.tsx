import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import {
  Segment, Header, Container, Item, Pagination, PaginationProps
} from 'semantic-ui-react';
import { fetchPosts } from '../../app/store/reducers/postSlice';
import { RootState } from '../../app/store/rootReducer';
// import { IPostState } from '../../app/models/Post';
import Post from './Post';
import { IPagination } from '../../app/models/Pagination';

interface IProps { }

const PostList: React.FC<IProps> = () => {
  // const posts = useSelector((state: IPostState) => state.posts);
  const [params, setParams] = useState<IPagination>({
    currentPage: 1,
    pageSize: 3
  });
  const postState = useSelector((state: RootState) => state.postState);
  const { posts, pagination } = postState;

  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchPosts(params));
  }, [dispatch, params]);

  const handlePageChange = (e: React.MouseEvent<HTMLAnchorElement, MouseEvent>,
    { activePage }: PaginationProps) => {
    const newParams = { ...params };
    newParams.currentPage = activePage;
    setParams(newParams);
  };

  let paginationArea = <p>Pagination</p>;

  if (pagination) {
    const { currentPage, totalPages } = pagination;
    paginationArea = (
      <Pagination
        defaultActivePage={currentPage}
        totalPages={totalPages}
        onPageChange={handlePageChange}
      />
    );
  }

  return (
    <Segment>
      <Header textAlign="center" content="Posts" />
      <Container>
        <Segment>
          <Item.Group divided>
            {posts && posts.map((post) => <Post key={post.id} post={post} />)}
          </Item.Group>
          <Container textAlign="center">{paginationArea}</Container>
        </Segment>
      </Container>
    </Segment>
  );
};

export default PostList;
