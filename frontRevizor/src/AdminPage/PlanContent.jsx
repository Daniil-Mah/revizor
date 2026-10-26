import { useEffect, useState } from "react";
import styles from './RolePage.module.css'

function PlanContent () {
    const [plan, setPlan] = useState([])
    
    useEffect(() => {
        fetch("http://localhost:5196/plan",{
            headers : {
                "Authorization" : `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res=> res.json()).then(data => setPlan(data.data))
    }, [])

    return (
        <div>
            <div className={styles.header}>
            <h2>Планы</h2>
            <button>+ Добавить</button>
            </div>
    
            <table className={styles.table}>
            <thead>
                <tr>
                <th>Название</th>
                <th>Дата создания</th>
                <th>Начало</th>
                <th>Конец</th>
                <th>Архивный</th>
                </tr>
            </thead>
            <tbody>
                {plan.map(p => (
                <tr
                    key={p.id}
                >
                    <td>{p.name}</td>
                    <td>{p.createdAt}</td>
                    <td>{p.periodStart}</td>
                    <td>{p.periodEnd}</td>
                    <td>{p.isArchived === 'true'? 'да' :"нет"}</td>
                </tr>
                ))}
            </tbody>
            </table>
        </div>
    )
} 

export default PlanContent;