import $api from "../Http/index";

export default class AuthService {
        static async login(email, password){
        return $api.post('/Auth/Login', {email, password})
    }

    static async registration(name, email, password){
        return $api.post('/Auth/Register', {name , email, password})
    }

    static async logout() {
        return $api.post(   '/Auth/Logout')
    }

}