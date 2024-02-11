import $api from "../Http/index";

export default class ToDoService{
    static async getAllTasks(){
        return $api.get('/ToDoItem/Get')
    }
    static async createTask(title,description,completionDate){
        console.log({
            "mainItem":{
                title,
                description,
                completionDate
            },
            "subItems":[]
        })
        return $api.post('ToDoItem/Create',{
            "mainItem":{
                title,
                description,
                completionDate
            },
            "subItems":[]
        })
    }
    static async createSubtask(title,completitonDate,parentId){
        return $api.post('/ToDoItem/CreateSubItem',{
            title:title,
            description:"",
            completionDate:completitonDate,
            parentId:parentId
        })
    }
    static async deleteSubtask(id){
        return $api.delete(`/ToDoItem/DeleteItemById?id=${id}`)
    }
    static async deleteTask(id){
        return $api.delete(`/ToDoItem/DeleteItemById?id=${id}`)
    }
    static async getToDoItemById(id){
        return $api.get(`/ToDoItem/GetToDoItemById?id=${id}`)
    }
    static async updateToDoItem(item){
        return $api.put(`/ToDoItem/UpdateToDoItem`,{
            ...item
        })
    }
    static async changeTaskStatus(id,status){
        return $api.put(`/ToDoItem/ChangeToDoItemStatus`,{
            id:id,
            status:status
        })
    }

}