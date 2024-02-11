import React, {useContext} from 'react';
import {Button} from "primereact/button";
import {useHistory} from "react-router-dom";
import {MenuContext} from "../Contexts/MenuContext";

const NotFound = () => {
    const history = useHistory()
    const tabs= useContext(MenuContext)
    function toMain(){
        history.push("/")
        tabs.setActiveTab(0)
    }
    return (
        <div className="flex align-items-center justify-content-center h-screen">
            <div className="flex flex-column justify-content-center align-items-center">
                <h1>Такая страница не найдена 😔</h1>
                <h2 className="text-center">А вы думали в сказку попали?</h2>
                <Button className="w-10rem mt-5 flex justify-content-center" onClick={toMain}>Вернуться</Button>
            </div>
        </div>
    );
};

export default NotFound;