import $api from "../Http/index";

export default class UserService {
    static async changeName(newName) {
        return $api.put('/UserAccount/ChangeName', {newName})
    }

    static async changePassword(newPassword) {
        return $api.put('/UserAccount/ChangePassword', {newPassword})
    }

}