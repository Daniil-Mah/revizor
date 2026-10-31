import { useState } from 'react';
import styles from './RoleEdit.module.css';
import { apiFetch } from '../api';

  function FacilityAddModal({onSave, onClose}) {
  const [title, setTitle] = useState("");
  const [location, setLocation] = useState("");


  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (title.trim() == "" || location.trim() == "") {
      alert("Обьект не может быть пустой")
      return ;
    }
    const res = await apiFetch(`http://localhost:5196/facilities/`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({title,location })
    });

    if (res.ok) {
      onSave({title,location});
      onClose()
    } else {
      alert('Ошибка сохранения');
    }
  };

  return (
    <div className={styles.overlay} onClick={onClose}>
      <div className={styles.modal} onClick={(e) => e.stopPropagation()}>
        <h3>Создать объект</h3>
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

export default FacilityAddModal;