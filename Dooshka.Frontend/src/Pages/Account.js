
import React, {useContext, useEffect, useState} from 'react';
import {TabMenu} from "primereact/tabmenu";
import {MenuContext} from "../Contexts/MenuContext";
import {useHistory} from "react-router-dom";
import {Card} from "primereact/card";
import {InputText} from "primereact/inputtext";
import {Button} from "primereact/button";
import UserService from "../Services/UserService";
import {Message} from "primereact/message";
const Account = () => {
    const tabs= useContext(MenuContext)
    const history = useHistory();
    const [password,setPassword] = useState()
    const [name,setName] = useState()
    const [nameStatus,setNameStatus] = useState()
    const [passwordStatus,setPasswordStatus] = useState()
    return (
        <div className="h-screen root">
            <TabMenu model={tabs.data}
                     activeIndex={tabs.activeTab}
                     onTabChange={(e) => {
                         tabs.setActiveTab(e.index);
                         history.push(tabs.data[e.index].redirectUrl)
                     }}/>
            <Card className="flex m-8 p-4">
                <h2>Настройки пользователя</h2>
                <div className="flex align-items-center">
                    <h4  className="w-20rem"> Имя пользователя:</h4 >
                    <InputText value={name}
                               onChange={(e)=>setName(e.target.value)}
                               className="mr-3" />
                    <Button
                        onClick={(e)=>{
                            UserService.changeName(name).then((res)=>{
                                res.status === 200? setNameStatus(true):setNameStatus(false)
                            })
                    }}>Изменить</Button>
                    <Message  className={nameStatus?"block ml-5":"hidden"} severity="success" text="Успешно изменено" />
                </div>
                <div className="flex align-items-center">
                    <h4 className="w-20rem">Пароль:</h4>
                    <InputText value={password}
                               onChange={(e)=>setPassword(e.target.value)}
                               className="mr-3"/>
                    <Button
                        onClick={(e)=>{
                            e.preventDefault()
                            UserService.changePassword(password).then((res)=>{
                                res.status === 200? setPasswordStatus(true):setPasswordStatus(false)
                            })
                        }}
                    >Изменить</Button>
                    <Message  className={passwordStatus?"block ml-5":"hidden"} severity="success" text="Успешно изменено" />

                </div>
            </Card>
            <div className="flex align-items-center justify-content-center w-full">
                    <h5 className="m-8 text-center">Всякий раз, когда два программиста встречаются<br/> для критического анализа своих программ, они оба молчат...<br/>
Алан Перлис (1922–1990) — американский учёный в области информатики</h5>
            </div>
        </div>
    );
};

export default Account;