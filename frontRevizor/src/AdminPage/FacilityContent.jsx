import { useEffect, useState } from "react";
import styles from './RolePage.module.css'
import { apiFetch } from "../api";
import FacilityAddModal from "./FacilityAdd";
import EditFacility from "./EditFacility";

function FacilityContent () {
    const [facility, setFacility] = useState([])
    const [loading, setLoading] = useState(true)
    const [IsAdd, setIsAdd] = useState(false)
    const [selectedFacility,setSelectedFacility] = useState(null)
    const [edit, setEdit] = useState(false)

    useEffect(() => {
      apiFetch("http://localhost:5196/facilities", {
        headers: {'Authorization' : `Bearer ${localStorage.getItem('token')}`}
      }).then(res=> res.json()).then(data=> {setFacility(data.data),setLoading(false)})},[])

    const handleAddSave = async () => {
        const res = await apiFetch('http://localhost:5196/facilities');
        const data = await res.json();
        setFacility(data.data);
        setIsAdd(false);
    }
    
    const handleDelete = async () => {
        if (!confirm('Удалить объект?')) { setSelectedFacility(null); return }
        apiFetch(`http://localhost:5196/facilities/${selectedFacility.id}`,
            {
                method: "DELETE"
            }
        )
        setFacility(facility.filter(f=> f.id !== selectedFacility.id)) 
        setSelectedFacility(null)
    }; 

    const handleEditSave = (updatedFacility) => {
        setFacility(facility.map(f => 
            f.id === updatedFacility.id ? updatedFacility : f
        ));
        setSelectedFacility(null);
        setEdit(false);
        };

    if (loading) return <h1>Загрузка...</h1>;
    

    return (
        <div>
            <div className={styles.header}>
            <h2>Объекты</h2>
            <button onClick={() => setIsAdd(true)}>+ Добавить</button>
            </div>

            {selectedFacility && (
                <div className={styles.actions}>
                    <span>Выбрано: {selectedFacility.id}</span>
                    <button onClick={() => setEdit(true)}> Изменить</button>
                    <button onClick={() => handleDelete()}>Удалить</button>
                    <button onClick={() => setSelectedFacility(null)}>Отмена</button>
                </div>
                )}
    
            <table className={styles.table}>
            <thead>
                <tr>
                <th>ID</th>
                <th>Название</th>
                <th>Адрес</th>
                </tr>
            </thead>
            <tbody>
                {facility.map(f => (
                <tr
                    key={f.id}
                    onClick={() => setSelectedFacility(f)}
                >
                    <td>{f.id}</td>
                    <td>{f.title}</td>
                    <td>{f.location}</td>
                </tr>
                ))}
            </tbody>
            </table>
            {edit && <EditFacility 
                facility={selectedFacility} 
                onSave={handleEditSave}
                onClose={ () => {setEdit(false), setSelectedFacility(null)} }
            />}
            {IsAdd && <FacilityAddModal onClose={() => setIsAdd(false)} onSave={handleAddSave}/>}
        </div>
    )
} 

export default FacilityContent;