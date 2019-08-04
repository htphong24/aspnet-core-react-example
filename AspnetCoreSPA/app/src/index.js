import "bootstrap/dist/css/bootstrap.min.css";
import "@fortawesome/fontawesome-free/css/all.css";
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
//import './datatables.min.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';

const rootElement = document.getElementById('root');

ReactDOM.render(
  <App />,
  rootElement);

registerServiceWorker();
