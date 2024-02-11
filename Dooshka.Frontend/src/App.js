import logo from './logo.svg';
import './App.css';
import "primereact/resources/themes/lara-light-cyan/theme.css";
import { Button } from 'primereact/button';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link,
} from 'react-router-dom';
import Auth from "./Pages/Auth";
import Home from "./Pages/Home";
import {MenuContext} from "./Contexts/MenuContext";
import {useState} from "react";
import Month from "./Pages/Month";
import Account from "./Pages/Account";
import NotFound from "./Pages/NotFound";

function App() {
    const [activeTab,setActiveTab] = useState(1)
    const menuItems = [
        {
            id:1,
            label:"Ближайшее",
            icon:"pi pi-calendar",
            redirectUrl:"/",
        },
        {
            id:2,
            label:"Таблица",
            icon:"pi pi-table",
            redirectUrl:"/month",
        },
        {
            id:3,
            label: "Аккаунт",
            icon:"pi pi-user",
            redirectUrl:"/account"
        },
        {
            id:4,
            label: "Выход",
            icon:"pi pi-sign-out",
            redirectUrl:"/auth"
        }
    ]
      return (
          <MenuContext.Provider value={
              {
                  activeTab:activeTab,
                  setActiveTab:setActiveTab,
                  data:menuItems
              }
          }>
            <div className="App">
                <Router>
                    <Switch>
                        <Route exact path="/" component={Home}/>
                        <Route exact path="/auth" component={Auth}/>
                        <Route exact path="/month" component={Month}/>
                        <Route exact path="/account" component={Account}/>>
                        <Route path="/" component={NotFound}/>
                    </Switch>
                </Router>
            </div>
          </MenuContext.Provider>
      );
}

export default App;
