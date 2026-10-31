import { useState } from 'react';
import styles from './RoleEdit.module.css';
import { apiFetch } from '../api';

  function RoleAddModal({onSave, onClose}) {
  const [name, setName] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (name.trim() == "") {
      alert("роль не может быть пустой")
      return ;
    }
    const res = await apiFetch(`http://localhost:5196/roles/`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name })
    });

    if (res.ok) {
      onSave({name});
      onClose()
    } else {
      alert('Ошибка сохранения');
    }
  };

  return (
    <div className={styles.overlay} onClick={onClose}>
      <div className={styles.modal} onClick={(e) => e.stopPropagation()}>
        <h3>Создать роль</h3>
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

export default RoleAddModal;