import { AUTH_ENDPOINT } from '../infrastructure/appConstants';
import baseApi from './baseApi';
import { setToken, removeToken } from '../config/auth/securityConfiguration';
import { ILoginUser, IRegisterUser, IUser } from '../models/User';

const AuthService = {
   login: async (loginUserModel: ILoginUser) => {
      try {
         const response: IUser = await baseApi.post(`${AUTH_ENDPOINT}/login`, loginUserModel);
         if (response) setToken(response.token);
         return response;
      } catch (error) {
         throw error;
      }
   },
   register: async (registerUserModel: IRegisterUser) => {
      try {
         const response: IUser = await baseApi.post(`${AUTH_ENDPOINT}/register`, registerUserModel);
         if (response) setToken(response.token);
         return response;
      } catch (error) {
         throw error;
      }
   },
   currentUser: async () => {
      try {
         const response: IUser = await baseApi.get(AUTH_ENDPOINT);
         return response;
      } catch (error) {
         throw error;
      }
   },
   logout: () => removeToken()
};

export default AuthService;
