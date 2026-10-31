import { useState } from 'react';
import styles from './RoleEdit.module.css';
import { apiFetch } from '../api';

function EditFacility({ facility, onClose, onSave }) {
  const [title, setTitle] = useState(facility.title);
  const [location, setLocation] = useState(facility.location);

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    const res = await apiFetch(`http://localhost:5196/facilities/${facility.id}`, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ title, location})
    });

    if (res.ok) {
      onSave({ ...facility,title,location  });
      onClose()
    } else {
      alert('Ошибка сохранения');
    }
  };

  return (
    <div className={styles.overlay} onClick={onClose}>
      <div className={styles.modal} onClick={(e) => e.stopPropagation()}>
        <h3>Изменить объект</h3>
        <form onSubmit={handleSubmit}>
          <label>
            Название:
            <input
              type="text"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              autoFocus
              required
            />
          </label>
          <label>
            Адрес:
            <input
              type="text"
              value={location}
              onChange={(e) => setLocation(e.target.value)}
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

export default EditFacility;