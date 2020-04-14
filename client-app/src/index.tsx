import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
// import App from './app/layout/App';
// import * as serviceWorker from './serviceWorker';
import { Provider } from 'react-redux';
import store from './app/store/store';
// import store from './app/store/redux-toolkit';

// const app = (
//   <Provider store={store}>
//     <App />
//   </Provider>
// );


// ReactDOM.render(
//   app,
//   document.getElementById('root')
// );

const render = () => {
  const App = require('./app/layout/App').default;

  const app = (
    <Provider store={store}>
      <App />
    </Provider>
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