import { useState, useEffect } from 'react';
import styles from './RolePage.module.css'
import { apiFetch } from '../api';
import EditRoleModal from './RoleEdit';
import RoleAddModal from './RoleAdd';

function RolePage() {
  const [roles, setRoles] = useState([]);
  const [selectedRole, setSelectedRole] = useState(null);
  const [editingRole, setEditingRole] = useState(null);
  const [IsAdd, setIsAdd] = useState(false)

  useEffect(() => {
    apiFetch('/roles')
      .then(res => res.json())
      .then(data => setRoles(data.data));
  }, []);

  const handleAddSave = async () => {
    const res = await apiFetch('http://localhost:5196/roles');
    const data = await res.json();
    setRoles(data.data);
    setIsAdd(false);
  };

    const handleDelete = async (id) => {
      if (!confirm('Удалить роль?')) return;
      const res = apiFetch(`http://localhost:5196/roles/${id}`, { 
      method: 'DELETE'
    });
    if ((await res).status !== 200 ) {
      alert ("Нельзя удалить запись")
      return;
    }
    setRoles(roles.filter(r => r.id !== id));
    setSelectedRole(null);
  };

  const handleSave = (updatedRole) => {
    setRoles(roles.map(r => r.id === updatedRole.id ? updatedRole : r));
    setSelectedRole(null);
  };

  return (
    <div>
      <div className={styles.header}>
        <h2>Роли</h2>
        <button onClick={() => setIsAdd(true)}>+ Добавить</button>
      </div>

      {selectedRole && (
        <div className={styles.actions}>
          <span>Выбрано: {selectedRole.name}</span>
          <button onClick={() => setEditingRole(selectedRole)}>Изменить</button>
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
      {editingRole && (
        <EditRoleModal
          role={editingRole}
          onClose={() => setEditingRole(null)}
          onSave={handleSave}
        />
      )}
      {IsAdd && (
      <RoleAddModal
        onClose={() => setIsAdd(false)}
        onSave={handleAddSave}
      />
    )}
    </div>
  );
}

export default RolePage;