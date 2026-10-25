import {useEffect, useState } from "react";
import styles from './RolePage.module.css'
import { useNavigate } from "react-router-dom";


function UserContent() {
    const [user, setUser] = useState([])
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

   useEffect(() => {
    loadUsers();
  }, []);

  async function loadUsers() {
    try {
      let token = localStorage.getItem('token');
      
      let res = await fetch('http://localhost:5196/users', {
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
        
        res = await fetch('http://localhost:5196/users', {
          headers: { 'Authorization': `Bearer ${token}` }
        });
      }
      
      if (!res.ok) throw new Error('Ошибка загрузки');
      
      const data = await res.json();
      setUser(data.data || []);
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
            <h2>Пользователи</h2>
            <button>+ Добавить</button>
            </div>
    
            <table className={styles.table}>
            <thead>
                <tr>
                <th>ID</th>
                <th>ФИО</th>
                <th>Роль</th>
                </tr>
            </thead>
            <tbody>
                {user.map(user => (
                <tr
                    key={user.id}
                >
                    <td>{user.id}</td>
                    <td>{user.fullName}</td>
                    <td>{user.role}</td>
                </tr>
                ))}
            </tbody>
            </table>
        </div>
    )
}

export default UserContent;