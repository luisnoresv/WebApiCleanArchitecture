/* eslint-disable no-undef */
import jwtDecode from 'jwt-decode';
import { TOKEN_KEY } from '../../infrastructure/appConstants';
import { IToken } from '../../models/User';

export const setToken = (token: string) => {
   if (token) localStorage.setItem(TOKEN_KEY, token);
};

export const getToken = (): any => localStorage.getItem(TOKEN_KEY);

export const getDecodedToken = () => {
   try {
      const token = getToken();
      const decodedToken: any = jwtDecode(token);
      const user: IToken = {
         id: decodedToken?.nameid,
         // eslint-disable-next-line camelcase
         userName: decodedToken?.unique_name,
         role: decodedToken?.role
      };
      return user;
   } catch (error) {
      throw error;
   }
};

export const removeToken = () => {
   localStorage.removeItem(TOKEN_KEY);
};

export const roleMatch = (allowedRoles: string[]) => {
   let isMatch;

   const userRoles = getDecodedToken().role;

   allowedRoles.forEach((role) => {
      if (userRoles.includes(role)) isMatch = true;
   });

   return isMatch;
};
