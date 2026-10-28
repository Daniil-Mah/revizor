import {useEffect, useState } from "react";
import styles from './RolePage.module.css'
import { apiFetch } from "../api";

function PlanContent () {
    const [plan, setPlan] = useState([])
    const [selectedPlan, setSelectedPlan] = useState(null)
    const [isSelect, setIsSelect] = useState(false)
    const [dataSelectedPlan, setDataSelectedPlan] = useState([])

    useEffect(() => {
        if (!selectedPlan) return; 
        apiFetch(`http://localhost:5196/plan-assigment/${selectedPlan.id}`)
        .then(res=> res.json())
        .then(data=>setDataSelectedPlan(data.data))
    }, [selectedPlan])

    useEffect(() => {
        apiFetch("http://localhost:5196/plan",{
            headers : {
                "Authorization" : `Bearer ${localStorage.getItem('token')}`
            }
        }).then(res=> res.json()).then(data => setPlan(data.data))
    }, [])

    function AddAssigment() {
        apiFetch(`http://localhost:5196/plan-assigment/${selectedPlan.id}`, {
            method: "POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ firstName, secondName, lastName, password }),
        })
    }

    if (isSelect) {
        return (
            <div>
                <button onClick={() => setIsSelect(false)}>← Назад к списку</button>
                <h1>{selectedPlan.name}</h1>
                <button onClick={() => AddAssigment() }> </button>
                <table className={styles.table}>
            <thead>
                <tr>
                <th>Объект</th>
                <th>Начало обхода</th>
                <th>Ревизоры</th>
                <th>Ответственный</th>
                </tr>
            </thead>
            <tbody>
                {dataSelectedPlan.map(dp => (
                <tr
                    key={dp.id}
                    onClick={() => (setIsSelect(!isSelect))}
                >
                    <td>{dp.facility}</td>
                    <td>{dp.passAt}</td>
                    <td>
                        <ul>
                            {dp.revizors.map(rev => (
                                <li key={rev.id}>{rev.fullName}</li>
                            ))}
                        </ul>
                    </td>
                    <td>{dp.responsiblePerson.fullName}</td>
                </tr>
                ))}
            </tbody>
            </table>
            </div>
        ) 
    }

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
                    onClick={() => {setIsSelect(true), setSelectedPlan(p)}}
                >
                    <td>{p.name}</td>
                    <td>{p.createdAt}</td>
                    <td>{p.periodStart}</td>
                    <td>{p.periodEnd}</td>
                    <td>{p.isArchived === true? 'да' :'нет'}</td>
                </tr>
                ))}
            </tbody>
            </table>
        </div>
    )
} 

export default PlanContent;