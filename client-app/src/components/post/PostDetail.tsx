import React, { useEffect } from 'react';
import { RouteComponentProps, Link } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { Item, Label, Icon, Button, Segment } from 'semantic-ui-react';
import { detailPost, deletePost } from '../../app/store/reducers/postSlice';
import { RootState } from '../../app/store/rootReducer';

interface IProps {
  id: string;
}

const PostDetail: React.FC<RouteComponentProps<IProps>> = ({
  match, history
}) => {
  const dispatch = useDispatch();
  const post = useSelector((state: RootState) => state.postState.post);

  useEffect(() => {
    dispatch(detailPost({ id: match.params.id }));
  });

  const handleRemovePost = (id: string) => {
    dispatch(deletePost({ id }));
  };

  return (
    <Segment>

      <Item>
        {post && (
          <>
            <Item.Image size="small" src={post.photoUrl} circular />
            <Item.Content>
              <Item.Header>{post.displayName}</Item.Header>
              <Item.Meta>
                @{post.userName} - <Icon name="calendar alternate outline" /> Posted on {post.postedOn}
              </Item.Meta>
              <Item.Description>
                <Item.Meta>{post.title}</Item.Meta>
                <p>{post.content}</p>
              </Item.Description>
              <Item.Extra>
                <Label>
                  <Icon name='mail' />
                  Mail
                </Label>
                <Label icon="globe" content="Additional Languages" />
                <Button as={Link} to={`/manage/${post.id}`} circular color='instagram' icon='edit' />
                <Button onClick={() => handleRemovePost(post.id)} circular color='google plus' icon='remove circle' />
              </Item.Extra>
            </Item.Content>
          </>
        )}
      </Item>
    </Segment>
  );
};

export default PostDetail;
