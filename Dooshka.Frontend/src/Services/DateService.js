import date from "date-and-time";
export default class DateService{

    static getTodayDate () {
        const today = new Date();
        return date.format(today,"YYYY-MM-DD")
    }
    static getTomorrowDate(){
        const today = new Date();
        return date.format(date.addDays(today,1),"YYYY-MM-DD")
    }
    static getAfterTomorrowDate(){
       const today = new Date();
        return date.format(date.addDays(today,2),"YYYY-MM-DD")
    }
    static formatInputDate(date){
        return date.getFullYear() + '.'
            + ('0' + (date.getMonth()+1)).slice(-2) + '.'
            + ('0' + date.getDate()).slice(-2);
    }

}