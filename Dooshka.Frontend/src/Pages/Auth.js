import React, {useContext, useEffect, useState} from 'react';
import {TabPanel, TabView} from "primereact/tabview";
import { InputText } from 'primereact/inputtext';
import {Button} from "primereact/button";
import {Link, useHistory} from "react-router-dom";
import {Context} from "../index";
import {observer} from "mobx-react-lite";
import {Password} from "primereact/password";
import {Message} from "primereact/message";
import {MenuContext} from "../Contexts/MenuContext";
const Auth = () => {

    const tabs= useContext(MenuContext)

    const [loginData,setLoginData] = useState({
        email:"",
        password:""
    })
    const [registrationData,setRegistrationData] = useState({
        name:"",
        email:"",
        password:"",
        passwordRepeat:""
    })
    function handleChangeLogin(e,target){
        setLoginData((prev)=>{
            return {...prev,[target]:e.target.value}
        })
    }

    function handleChangeRegistration(e,target){
        setRegistrationData((prev)=>{
            return {...prev,[target]:e.target.value}
        })
    }

    const {store} = useContext(Context);
    const history = useHistory()
    const [passwordsError,setPasswordsError] = useState(false)

    return (
        <div className="flex h-screen">
            <div>
                <img src={"./Assets/authBg.png"} className="h-full" alt=""/>
            </div>
            <div className="w-full flex justify-content-center align-items-center">
                <div className="h-30rem" >
                    <TabView>
                        <TabPanel header="Вход">
                            <form action="" className="flex flex-column w-30rem gap-2">
                                <InputText value={loginData.email} onChange={(e)=>handleChangeLogin(e,'email')} placeholder="Введите E-mail"/>
                                <InputText value={loginData.password} onChange={(e)=>handleChangeLogin(e,'password')} placeholder="Введите пароль"/>
                                <div className="flex justify-content-end">
                                    <Button link><Link to={"/"}>Вы забыли пароль?</Link></Button>
                                    <Button onClick={(e) => {
                                        e.preventDefault()
                                        store.login(loginData.email,loginData.password).then(()=>{
                                            history.push("/")
                                            tabs.setActiveTab(0)
                                        })
                                    }} label="Готово" />
                                </div>
                            </form>
                        </TabPanel>
                        <TabPanel header="Регистрация">
                            <form action="" className="flex flex-column w-30rem gap-2">
                                <InputText value={registrationData.name} onChange={(e)=>handleChangeRegistration(e,'name')} placeholder="Введите имя"/>
                                <InputText value={registrationData.email} onChange={(e)=>handleChangeRegistration(e,'email')} placeholder="Введите Email"/>
                                <Password inputClassName={"col-12"} toggleMask feedback={false} value={registrationData.password} onChange={(e)=>handleChangeRegistration(e,'password')} placeholder="Введите пароль"/>
                                <Password inputClassName={"col-12"} toggleMask feedback={false} value={registrationData.passwordRepeat} onChange={(e)=>handleChangeRegistration(e,'passwordRepeat')} placeholder="Введите пароль повторно"/>
                                <Message className={passwordsError?"block":"hidden"} severity="error" text="Введены не одинаковые пароли"></Message>
                                <div className="flex justify-content-end">
                                    <Button link><Link to={"/"}>Вы забыли пароль?</Link></Button>
                                    <Button onClick={(e) => {
                                        e.preventDefault()
                                        if (registrationData.password != registrationData.passwordRepeat){
                                            setPasswordsError(true)
                                            return
                                        }
                                        store.registration(registrationData.name,registrationData.email,registrationData.password).then(()=>{
                                            history.push("/")
                                            tabs.setActiveTab(0)
                                        })
                                    }}  label="Готово" />

                                </div>
                            </form>
                        </TabPanel>
                    </TabView>
                </div>
            </div>
        </div>
    );
};

export default observer(Auth);