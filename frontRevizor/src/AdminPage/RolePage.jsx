import { useState, useEffect } from 'react';
import styles from './RolePage.module.css'
import { apiFetch } from '../api';

function RolePage() {
  const [roles, setRoles] = useState([]);
  const [selectedRole, setSelectedRole] = useState(null);

  useEffect(() => {
    apiFetch('/roles')
      .then(res => res.json())
      .then(data => setRoles(data.data));
  }, []);

    const handleDelete = async (id) => {
      if (!confirm('Удалить роль?')) return;
      await fetch(`http://localhost:5196/roles/${id}`, { 
      method: 'DELETE',
      headers: {
        'Authorization': `Bearer ${localStorage.getItem('token')}`
      }
    });
    setRoles(roles.filter(r => r.id !== id));
    setSelectedRole(null);
  };

  const handleEdit = (role) => {
    console.log('Изменить:', role);
  };

  return (
    <div>
      <div className={styles.header}>
        <h2>Роли</h2>
        <button>+ Добавить</button>
      </div>

      {selectedRole && (
        <div className={styles.actions}>
          <span>Выбрано: {selectedRole.name}</span>
          <button onClick={() => handleEdit(selectedRole)}>Изменить</button>
          <button onClick={() => handleDelete(selectedRole.id)}>Удалить</button>
          <button onClick={() => setSelectedRole(null)}>Отмена</button>
        </div>
      )}

      <table className={styles.table}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Название</th>
          </tr>
        </thead>
        <tbody>
          {roles.map(role => (
            <tr
              key={role.id}
              onClick={() => setSelectedRole(role)}
              className={selectedRole?.id === role.id ? styles.selected : ''}
            >
              <td>{role.id}</td>
              <td>{role.name}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default RolePage;