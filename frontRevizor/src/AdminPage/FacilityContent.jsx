import { useEffect, useState } from "react";
import styles from './RolePage.module.css'
import { apiFetch } from "../api";

function FacilityContent () {
    const [facility, setFacility] = useState([])
    const [loading, setLoading] = useState(null)
    
    useEffect(() => {
      apiFetch("http://localhost:5196/facilities", {
        headers: {'Authorization' : `Bearer ${localStorage.getItem('token')}`}
      }).then(res=> res.json()).then(data=> setFacility(data.data),setLoading(false))},[])


    if (loading) return <h1>Загрузка...</h1>;

    return (
        <div>
            <div className={styles.header}>
            <h2>Объекты</h2>
            <button>+ Добавить</button>
            </div>
    
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
                >
                    <td>{f.id}</td>
                    <td>{f.title}</td>
                    <td>{f.location}</td>
                </tr>
                ))}
            </tbody>
            </table>
        </div>
    )
} 

export default FacilityContent;