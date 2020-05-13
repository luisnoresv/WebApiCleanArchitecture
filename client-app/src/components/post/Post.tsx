import React from 'react';
import {
  Item, Icon, Label
} from 'semantic-ui-react';
import { Link } from 'react-router-dom';
import { IPost } from '../../app/models/Post';

interface IProps {
  post: IPost;
}

const Post: React.FC<IProps> = ({ post }) => (
  <Item>
    <Item.Image size="small" src={post.photoUrl} circular />
    <Item.Content>
      <Item.Header>{post.displayName}</Item.Header>
      <Item.Meta>
        @{post.userName} - <Icon name="calendar alternate outline" /> Posted on {post.postedOn}
      </Item.Meta>
      <Item.Description>
        <Item.Meta as={Link} to={`/posts/${post.id}`}>{post.title}</Item.Meta>
        <p>{post.content}</p>
      </Item.Description>
      <Item.Extra>
        <Label>
          <Icon name='mail' />
                  Mail
        </Label>
        <Label icon="globe" content="Additional Languages" />
      </Item.Extra>
    </Item.Content>
  </Item>
);

export default Post;
