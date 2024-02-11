import React, {createContext} from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from 'react-router-dom';
import 'primeicons/primeicons.css';
import Store from "./Store/store"
import { PrimeReactProvider } from 'primereact/api';
import {observer} from "mobx-react-lite";
export const store = new Store();
export const Context = createContext({
    store,
})

const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
  <React.StrictMode>
      <BrowserRouter>
          <Context.Provider value={{
              store
          }}>
              <PrimeReactProvider>
                  <App />
              </PrimeReactProvider>
          </Context.Provider>
      </BrowserRouter>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
