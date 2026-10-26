import { useEffect, useState } from "react";
import styles from './RolePage.module.css'

function FacilityContent () {
    const [facility, setFacility] = useState([])
    const [loading, setLoading] = useState(null)
    
    useEffect(() => {loadFacilities()},[])

    async function loadFacilities() {
    try {
      let token = localStorage.getItem('token');
      
      let res = await fetch('http://localhost:5196/facilities', {
        headers: { 'Authorization': `Bearer ${token}` }
      });

      if (res.status === 401) {
        const refreshRes = await fetch('http://localhost:5196/refresh', {
          method: 'POST',
          credentials: 'include'
        });
        
        if (!refreshRes.ok) {
          navigate('login')
          return;
        }
        
        const refreshData = await refreshRes.json();
        token = refreshData.data.accessToken;
        localStorage.setItem('token', token);
        
        res = await fetch('http://localhost:5196/facilities', {
          headers: { 'Authorization': `Bearer ${token}` }
        });
      }
      
      if (!res.ok) throw new Error('Ошибка загрузки');
      
      const data = await res.json();
      setFacility(data.data || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoading(false);
    }
  }

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