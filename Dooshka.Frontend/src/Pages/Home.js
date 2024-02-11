import React, {useContext, useEffect, useState} from 'react';
import { TabMenu } from 'primereact/tabmenu';
import {MenuContext} from "../Contexts/MenuContext";
import {Card} from "primereact/card";
import {Button} from "primereact/button";
import {ScrollPanel} from "primereact/scrollpanel";
import {Checkbox} from "primereact/checkbox";
import {Divider} from "primereact/divider";
import {ProgressBar} from "primereact/progressbar";
import { useHistory } from 'react-router-dom';
import { Dialog } from 'primereact/dialog'
import {InputText} from "primereact/inputtext";
import {ListBox} from "primereact/listbox";
import ToDoService from "../Services/ToDoService"
import {Calendar} from "primereact/calendar";
import DateService from "../Services/DateService"
import {Context} from "../index";
import date from "date-and-time";
import {set} from "mobx";
const Home = () => {
    const tabs= useContext(MenuContext)
    const {store} = useContext(Context)
    const [refresh,setRefresh] = useState(0)
    const [editing,setEditing] = useState(false)
    const [creatingNew,setCreatingNew] = useState(false)

    const [taskForm,setTaksForm] = useState({
    })

    const [newTask,setNewTask] = useState({
        title:"",
        description:"",
        date:""
    })

    const [selectedSubtask,setSelectedSubtask] = useState(undefined)
    const [data,setData] = useState([])
    const history = useHistory();
    const subtasks = [
        { name: 'Подзадача 1', code: 'NY' },
        { name: 'Подзадача 2', code: 'RM' },
        { name: 'Подзадача 3', code: 'LDN' },
        { name: 'Подзадача 4', code: 'IST' },
        { name: 'Подзадача 5', code: 'PRS' }
    ];
    const [newSubtask,setNewSubtask] = useState("")

    useEffect(()=>{
        if(!store.isAuth){
            history.push("/auth")
        }
    },[])

    useEffect(()=>{
        ToDoService.getAllTasks().then(function(res){
            setData(res.data)
        })
    },[setData,refresh])

    function handleEdit(task){
        if (task.id){
            ToDoService.getToDoItemById(task.id).then((res)=>{
                let taskData = res.data
                taskData.date = new Date (task.completionDate.substring(0,4),task.completionDate.substring(5,7) - 1,task.completionDate.substring(8,10))
                setTaksForm(taskData)
                setEditing(true)
            })
        }else{
            setTaksForm(task)
        }
    }

    function handleCreateChange(e,target){
        setNewTask((prev)=>{
            return {
                ...prev,
                [target]:e.target.value
            }
        })
    }
    function nullifyNewTask(){
        setNewTask({
            title:"",
            description:"",
            date:""
        })
    }

    function handleFormChange(e,target) {
        console.log(e , target)
        console.log(taskForm)
        setTaksForm((prev) => {
                return {
                    ...prev,
                    [target]: e.target.value
                }
            }
        )
    }
    function setChecked(id){

    }

    return (
        <div className="h-screen root">
            <TabMenu model={tabs.data}
                     activeIndex={tabs.activeTab}
                     onTabChange={(e) => {
                         tabs.setActiveTab(e.index);
                         history.push(tabs.data[e.index].redirectUrl);
                     }}/>
            <div className="flex align-items-start justify-content-center mt-8 h-full">
                <Card className="col-3 ml-5 mr-5 relative" title={"Сегодня"} subTitle={DateService.getTodayDate()}>
                    <ScrollPanel className="h-25rem">
                        {
                            data.map(el=>{
                                if(DateService.getTodayDate() === el.completionDate){
                                    return(
                                        <>
                                            <div className="flex justify-content-between align-items-center">
                                                <div className="flex align-items-center">
                                                    <h4>{el.title.length > 20 ? el.title.substring(0,20) + "..." : el.title}</h4>
                                                    <i onClick={()=>handleEdit(el)} className="pi pi-pencil cursor-pointer ml-4"></i>
                                                </div>
                                                <Checkbox checked={el.status === 2}  onClick={()=>{
                                                    ToDoService.changeTaskStatus(el.id,el.status === 2?0:2).then(()=>{
                                                        setRefresh(prev=>prev+1)
                                                    })
                                                }}className="mr-3"></Checkbox>
                                            </div>
                                            {
                                                el.subItems.map((subitem)=>{
                                                    return (
                                                        <>
                                                            <div className="flex justify-content-between align-items-center">
                                                                <p className="ml-4">{subitem.title}</p>
                                                                <Checkbox  checked={subitem.status === 2} onClick={()=>{
                                                                    ToDoService.changeTaskStatus(subitem.id,subitem.status === 2?0:2).then(()=>{
                                                                        setRefresh(prev=>prev+1)
                                                                    })
                                                                }} className="mr-3"></Checkbox>
                                                            </div>
                                                        </>
                                                    )
                                                })
                                            }
                                            <Divider></Divider>
                                        </>
                                    )
                                }
                            })
                        }
                    </ScrollPanel>
                    <Button onClick={()=>setCreatingNew(true)} className="w-2rem h-2rem button_position md:absolute flex align-items-center justify-content-center" icon="pi pi-plus"/>
                    <ProgressBar className="mt-5" value={50}></ProgressBar>
                </Card>
                <Card className="col-3 ml-5 mr-5 relative" title={"Завтра"} subTitle={DateService.getTomorrowDate()}>
                    <ScrollPanel className="h-25rem">
                        {
                            data.map(el=>{
                                if(DateService.getTomorrowDate() === el.completionDate){
                                    return(
                                        <>
                                            <div className="flex justify-content-between align-items-center">
                                                <div className="flex align-items-center">
                                                    <h4>{el.title.length > 20 ? el.title.substring(0,20) + "..." : el.title}</h4>
                                                    <i onClick={()=>handleEdit(el)} className="pi pi-pencil cursor-pointer ml-4"></i>
                                                </div>
                                                <Checkbox checked={el.status === 2}  onClick={()=>{
                                                    ToDoService.changeTaskStatus(el.id,el.status === 2?0:2).then(()=>{
                                                        setRefresh(prev=>prev+1)
                                                    })
                                                }}className="mr-3"></Checkbox>
                                            </div>
                                            {
                                                el.subItems.map((subitem)=>{
                                                    return (
                                                        <>
                                                            <div className="flex justify-content-between align-items-center">
                                                                <p className="ml-4">{subitem.title}</p>
                                                                <Checkbox  checked={subitem.status === 2} onClick={()=>{
                                                                    ToDoService.changeTaskStatus(subitem.id,subitem.status === 2?0:2).then(()=>{
                                                                        setRefresh(prev=>prev+1)
                                                                    })
                                                                }} className="mr-3"></Checkbox>
                                                            </div>
                                                        </>
                                                    )
                                                })
                                            }
                                            <Divider></Divider>
                                        </>
                                    )
                                }
                            })
                        }
                    </ScrollPanel>
                    <Button
                        onClick={()=>{
                        setCreatingNew(true)
                    }} className="w-2rem h-2rem button_position md:absolute flex align-items-center justify-content-center" icon="pi pi-plus"/>
                    <ProgressBar className="mt-5" value={50}></ProgressBar>
                </Card>
                <Card className="col-3 ml-5 mr-5 relative" title={"Послезавтра"} subTitle={DateService.getAfterTomorrowDate()}>
                    <ScrollPanel className="h-25rem">
                        {
                            data.map(el=>{
                                if(DateService.getAfterTomorrowDate()  === el.completionDate){
                                    return(
                                        <>
                                            <div className="flex justify-content-between align-items-center">
                                                <div className="flex align-items-center">
                                                    <h4>{el.title.length > 20 ? el.title.substring(0,20) + "..." : el.title}</h4>
                                                    <i onClick={()=>handleEdit(el)} className="pi pi-pencil cursor-pointer ml-4"></i>
                                                </div>
                                                <Checkbox checked={el.status === 2}  onClick={()=>{
                                                    ToDoService.changeTaskStatus(el.id,el.status === 2?0:2).then(()=>{
                                                        setRefresh(prev=>prev+1)
                                                    })
                                                }}className="mr-3"></Checkbox>
                                            </div>
                                            {
                                                el.subItems.map((subitem)=>{
                                                    return (
                                                        <>
                                                            <div className="flex justify-content-between align-items-center">
                                                                <p className="ml-4">{subitem.title}</p>
                                                                <Checkbox  checked={subitem.status === 2} onClick={()=>{
                                                                    ToDoService.changeTaskStatus(subitem.id,subitem.status === 2?0:2).then(()=>{
                                                                        setRefresh(prev=>prev+1)
                                                                    })
                                                                }} className="mr-3"></Checkbox>
                                                            </div>
                                                        </>
                                                    )
                                                })
                                            }
                                            <Divider></Divider>
                                        </>
                                    )
                                }
                            })
                        }
                    </ScrollPanel>
                    <Button onClick={()=>setCreatingNew(true)} className="w-2rem h-2rem button_position md:absolute flex align-items-center justify-content-center" icon="pi pi-plus"/>
                    <ProgressBar className="mt-5" value={50}></ProgressBar>
                </Card>
            </div>
            <Dialog header="Задача" visible={editing} style={{ width: '40vw' }} onHide={() => setEditing(false)}>
                <div>
                    <form action="" className="flex flex-column gap-2">
                        <InputText value={taskForm.title}
                                   required={true}
                                   placeholder={"Название задачи"}
                                   onChange={(e)=> {
                            handleFormChange(e,"title")
                        }}/>
                        <InputText value={taskForm.description}
                                   required={true}
                                   placeholder={"Описание задачи"}
                                   onChange={(e)=> {
                                       handleFormChange(e,"description")
                        }}/>
                        <Calendar value={taskForm.date}
                                  dateFormat="yy.mm.dd"
                                  required={true}
                                  placeholder={"Дата выполнения"}
                                  onChange={(e)=> {
                                      handleFormChange(e,"date")
                                  }}/>
                        <h3>Подзадачи</h3>
                        <div className="flex">
                            <InputText value={newSubtask}
                                       placeholder={"Введите подзадачу"}
                                       onChange={(e)=>setNewSubtask((e.target.value))}
                                       />
                            <Button severity="success"  icon="pi pi-plus" className="ml-2" onClick={(e)=>{
                                e.preventDefault()
                                ToDoService.createSubtask(newSubtask,DateService.formatInputDate(taskForm.date),taskForm.id).then(()=>
                                    {
                                        setRefresh(prev=>prev+1)
                                        handleEdit(taskForm)
                                    }
                                )
                            }}>
                            </Button>
                            <Button severity="danger" className="ml-2" icon="pi pi-trash" onClick={(e)=> {
                                e.preventDefault()
                                ToDoService.deleteSubtask(selectedSubtask.id).then(()=>
                                {
                                    setRefresh(prev=> prev+1)
                                    handleEdit(taskForm)
                                })
                            }}>
                            </Button>
                        </div>
                        <ListBox value={selectedSubtask}
                                 options={taskForm.subItems}
                                 listStyle={{ maxHeight: '200px' }}
                                 onChange={(e)=>{
                                     setSelectedSubtask(e.value)
                                     console.log(selectedSubtask)
                                 }}
                                 optionLabel="title"
                                 filter>
                        </ListBox>
                        <div className="flex justify-content-between mt-5">
                            <Button severity="danger" icon="pi pi-trash mr-2" onClick={(e)=>{
                                e.preventDefault()
                                ToDoService.deleteTask(taskForm.id).then(()=>{
                                    setRefresh(prev=>prev+1)
                                    setEditing(false)
                                })
                            }}>Удалить задачу</Button>
                            <div>
                                <Button severity="success" className="ml-2" onClick={(e)=>{
                                    e.preventDefault()
                                    taskForm.completionDate = (DateService.formatInputDate(taskForm.date))
                                    ToDoService.updateToDoItem(taskForm).then(()=>{
                                        setEditing(false)
                                        setRefresh(prev=>prev+1)
                                        console.log(taskForm)
                                    })
                                }}>Готово</Button>
                            </div>
                        </div>
                    </form>
                </div>
            </Dialog>

            <Dialog header="Создание задачи" visible={creatingNew} style={{ width: '40vw' }} onHide={() => setCreatingNew(false)}>
                <div>
                    <form action="" className="flex flex-column gap-2">
                        <InputText value={newTask.title}
                                   required={true}
                                   onChange={(e)=>{handleCreateChange(e,"title")}}
                                   placeholder={"Название задачи"}/>
                        <InputText value={newTask.description}
                                   required={true}
                                   onChange={(e)=>{handleCreateChange(e,"description")}}
                                   placeholder={"Описание задачи"}/>
                        <Calendar dateFormat="yy.mm.dd" value={newTask.date}
                                  required={true}
                                  onChange={(e)=>{handleCreateChange(e,"date")}}
                                  placeholder={"Дата выполнения"} />

                        <div className="flex justify-content-end mt-2">
                            <div>
                                <Button severity="success" className="ml-2" onClick={(e)=>{
                                    e.preventDefault()
                                    ToDoService.createTask(newTask.title,newTask.description,DateService.formatInputDate(newTask.date)).then(()=>{
                                        setRefresh(prev=>prev+1)
                                        setCreatingNew(false)
                                        nullifyNewTask()
                                    })
                                }} >Готово
                                </Button>
                            </div>
                        </div>
                    </form>
                </div>
            </Dialog>


        </div>
    );
};

export default Home;