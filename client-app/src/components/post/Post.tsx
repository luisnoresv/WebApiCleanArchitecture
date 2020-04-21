import React from 'react';
import {
   Item, Icon, Label, Button
} from 'semantic-ui-react';
import { useDispatch } from 'react-redux';
import { IPost } from '../../app/models/Post';
// import { detailPostActionCreator, removePostActionCreator } from '../../app/store/redux-toolkit';
import { detailPostActionCreator, removePostActionCreator } from '../../app/store/reducers/postSlice';

interface IProps {
   post: IPost;
}

const Post: React.FC<IProps> = ({ post }) => {
   const dispatch = useDispatch();

   const handlePostDetail = (id: string) => {
      dispatch(detailPostActionCreator({ id }));
   };

   const handleRemovePost = (id: string) => {
      dispatch(removePostActionCreator({ id }));
   };

   return (
      <Item>
         <Item.Image size="small" src={post.photoUrl} circular />
         <Item.Content>
            <Item.Header onClick={() => handlePostDetail(post.id)} as="a">{post.displayName}</Item.Header>
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
               <Button onClick={() => handleRemovePost(post.id)} circular color='google plus' icon='remove circle' />
            </Item.Extra>
         </Item.Content>
      </Item>
   );
};

export default Post;
