export interface IToken {
   id: string;
   userName: string;
   role: string[];
}

export interface ILoginUser {
   email: string;
   password: string;
}

export interface IRegisterUser {
   userName: string;
   email: string;
   password: string;
}

export interface IUser {
   token: string;
   userName: string;
}

export interface IUserRequestError {
   errorMessage: string;
}

export interface IUserState {
   user: IUser | null;
   error: any;
}
