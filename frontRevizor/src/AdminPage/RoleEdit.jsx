import { useState } from 'react';
import styles from './RoleEdit.module.css';
import { apiFetch } from '../api';

function EditRoleModal({ role, onClose, onSave }) {
  const [name, setName] = useState(role.name);

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    const res = await apiFetch(`http://localhost:5196/roles/${role.id}`, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name })
    });

    if (res.ok) {
      onSave({ ...role, name });
      onClose()
    } else {
      alert('Ошибка сохранения');
    }
  };

  return (
    <div className={styles.overlay} onClick={onClose}>
      <div className={styles.modal} onClick={(e) => e.stopPropagation()}>
        <h3>Изменить роль</h3>
        <form onSubmit={handleSubmit}>
          <label>
            Название:
            <input
              type="text"
              value={name}
              onChange={(e) => setName(e.target.value)}
              autoFocus
              required
            />
          </label>
          <div className={styles.buttons}>
            <button type="button" onClick={onClose}>Отмена</button>
            <button type="submit">Сохранить</button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default EditRoleModal;