import {makeAutoObservable} from "mobx";
import AuthService from "../Services/AuthService";
import axios from 'axios';
import {API_URL} from "../Http/index";
import auth from "../Pages/Auth";

export default class Store {
    user = {} ;
    userId = 0 ;
    history = {};
    isAuth = false;
    isLoading = false;


    constructor() {
        makeAutoObservable(this);
    }

    setAuth(bool) {
        this.isAuth = bool;
    }

    setUserId(id){
        this.userId = id
    }

    setUser(user) {
        this.user = user;
    }

    setLoading(bool) {
        this.isLoading = bool;
    }

    async login(email, password) {
        try {
            const response = await AuthService.login(email, password);
            localStorage.setItem('token', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);
            this.setAuth(true);
            this.setUser(response.data.user);
            this.setUserId(response.data.id);
        } catch (e) {
            console.log(e.response?.data?.message);
        }
    }

    async registration(name,email, password) {
        try {
            const response = await AuthService.registration(name,email, password);
            localStorage.setItem('token', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);
            this.setAuth(true);
            this.setUser(response.data.user);
        } catch (e) {
            console.log(e.response?.data?.message);
        }
    }

    async logout() {
        try {
            const response = await AuthService.logout();
            console.log(response)
            localStorage.removeItem('token');
            localStorage.removeItem('refreshToken');
            this.setAuth(false);
            this.setUser();
        } catch (e) {
            console.log(e.response?.data?.message);
        }
    }

    async checkAuth() {
        this.setLoading(true);
        try {
            const response = await axios.get(`${API_URL}/refresh`, {withCredentials: true})
            console.log(response);
            localStorage.setItem('token', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);
            this.setAuth(true);
            this.setUser(response.data.user);
        } catch (e) {
            console.log(e.response?.data?.message);
        } finally {
            this.setLoading(false);
        }
    }
}