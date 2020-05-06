import axios, { AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import history from '../..';
import { TOKEN_KEY } from '../infrastructure/appConstants';
import { getToken } from '../config/auth/securityConfiguration';

const http = axios.create({
   baseURL: process.env.REACT_APP_API_URL
});

// To add token to the header with bearer schema
http.interceptors.request.use(
   (config) => {
      const token = getToken();
      if (token) config.headers.Authorization = `Bearer ${token}`;
      return config;
   },
   (error) => Promise.reject(error)
);

http.interceptors.response.use(undefined, (error) => {
   if (error.message === 'Network Error' && !error.response) {
      toast.error('Network error - make sure the API server is running');
   }

   const { status, data, config, headers } = error.response;

   if (status === 404) {
      history.push('/notFound');
   }

   if (
      status === 401 &&
      headers['www-authenticate'].includes(
         'Bearer error="invalid_token", error_description="The token expired'
      )
   ) {
      // eslint-disable-next-line no-undef
      window.localStorage.removeItem(TOKEN_KEY);
      history.push('/');
      toast.info('Your session has expired, please login again');
   }

   // eslint-disable-next-line no-prototype-builtins
   if (status === 400 && config.method === 'get' && data.errors.hasOwnProperty('id')) {
      history.push('/notFound');
   }

   if (status === 500) {
      toast.error('Server error - check the terminal for more info!');
   }

   throw error.response;
});

const sleep = (ms: number) => (response: AxiosResponse) =>
   new Promise<AxiosResponse>((resolve) => setTimeout(() => resolve(response), ms));

const responseBody = (response: AxiosResponse) => response.data;

const baseApi = {
   get: (url: string) =>
      http
         .get(url)
         .then(sleep(2000))
         .then(responseBody),
   post: (url: string, body: {}) => http.post(url, body).then(responseBody),
   put: (url: string, body: {}) => http.put(url, body).then(responseBody),
   delete: (url: string) => http.delete(url).then(responseBody),
   // Request: 'GET', allow Pagination on client
   listWithPaging: (url: string, params: {}) =>
      http.get(url, { params }).then((res: AxiosResponse<any>) => ({
         body: res.data,
         headers: res.headers
      }))
};

export default baseApi;
