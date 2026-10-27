import { useEffect, useState } from 'react';
import styles from './RolePage.module.css';
import { apiFetch } from '../api';

function MainContent() {
  const [plan, setPlan] = useState(null);
  const token = localStorage.getItem('token');

  useEffect(() => {
    apiFetch('http://localhost:5196/plan/current', {
      method: 'GET',
      headers: { 'Authorization': `Bearer ${token}` }
    })
      .then(res => res.json())
      .then(data => setPlan(data.data));
  }, []);

  if (!plan) return <h1>Загрузка...</h1>;
  
  return (
    <div>
      <div className={styles.header}>
        <h2>План на {plan.namePlan}</h2>
      </div>

      {plan.assigments.length === 0 ? (
        <h1>Plan is empty</h1>
      ) : (
        <table className={styles.table}>
          <thead>
            <tr>
              <th>Объект</th>
              <th>Время начала</th>
              <th>Ревизоры</th>
              <th>Ответственное лицо</th>
            </tr>
          </thead>
          <tbody>
            {plan.assigments.map(pl => (
              <tr key={pl.id}>
                <td>{`${pl.facilityTitle} ${pl.facilityLocation}`}</td>
                <td>{pl.passAt}</td>
                <td>
                  <ul>
                    {pl.revizors.map(rev => (
                      <li key={rev.id}>{rev.fullName}</li>
                    ))}
                  </ul>
                </td>
                <td>{pl.responsiblePerson?.fullName || 'Не назначен'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default MainContent;