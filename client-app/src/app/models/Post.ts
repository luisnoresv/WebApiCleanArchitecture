export interface IPost {
  id: string;
  displayName: string;
  userName: string;
  photoUrl: string;
  title: string;
  content: string;
  postedOn: string;
}

export interface IPostState {
  posts: IPost[];
  post: IPost | null;
}
