import React, {useContext, useEffect, useState} from 'react';
import {TabMenu} from "primereact/tabmenu";
import {MenuContext} from "../Contexts/MenuContext";
import {useHistory} from "react-router-dom";
import { Column } from 'primereact/column';
import ToDoService from "../Services/ToDoService";
import {DataTable} from "primereact/datatable";

const Month = () => {
    const tabs= useContext(MenuContext)
    const history = useHistory();

    const [data,setData] = useState([])
    const [refresh,setRefresh] = useState(0)

    useEffect(()=>{
        ToDoService.getAllTasks().then(function(res){
            setData([...res.data])
            console.log([...res.data])
        })
    },[setData,refresh])


    return (
        <div className="h-screen root">
            <TabMenu model={tabs.data}
                     activeIndex={tabs.activeTab}
                     onTabChange={(e) => {
                         tabs.setActiveTab(e.index);
                         history.push(tabs.data[e.index].redirectUrl)
                     }}/>
            <DataTable className="h-1rem" paginator={true} rows={5} rowsPerPageOptions={[5,20,30]} scrollable={true} value={data} >
                <Column   field="title" header="Название задачи" sortable={true}></Column>
                <Column  field="description" header="Описание задачи" sortable={true}></Column>
                <Column  field="completionDate" header="Дата выполнения" sortable={true}></Column>
            </DataTable>
        </div>
    );
};

export default Month;