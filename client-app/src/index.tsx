import React from 'react';
import ReactDOM from 'react-dom';
// import * as serviceWorker from './serviceWorker';
import './app/layout/App.css';
import 'react-toastify/dist/ReactToastify.min.css';
import { Provider } from 'react-redux';
import { Router } from 'react-router-dom';
import { createBrowserHistory } from 'history';
import store from './app/store/store';
// import store from './app/store/redux-toolkit';

const history = createBrowserHistory();

const render = () => {
   const App = require('./app/layout/App').default;

   const app = (
      <Router history={history}>
         <Provider store={store}>
            <App />
         </Provider>
      </Router>
   );

   ReactDOM.render(
      app,
      document.getElementById('root')
   );
};

render();

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
// serviceWorker.unregister();

if (process.env.NODE_ENV === 'development' && module.hot) {
   module.hot.accept('./app/layout/App', render);
}

export default history;
