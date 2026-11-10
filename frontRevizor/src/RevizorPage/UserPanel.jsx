import { useNavigate } from 'react-router-dom';
import styles from './UserPanel.module.css';

function getUserFromCookie() {
  const match = document.cookie.split('; ').find((row) => row.startsWith('userInfo='));
  if (!match) return null;
  const base64 = match.split('=')[1];
  if (!base64) return null;
  try {
    const padded = base64 + '='.repeat((4 - (base64.length % 4)) % 4);
    return JSON.parse(atob(padded));
  } catch {
    return null;
  }
}

function UserPanel() {
  const navigate = useNavigate();
  const user = getUserFromCookie();

  const handleLogout = () => {
    localStorage.removeItem('token');
    document.cookie = 'userInfo=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/';
    navigate('/login', { replace: true });
  };

  if (!user) return null;

  return (
    <div className={styles.panel}>
      <div className={styles.fields}>
        <div className={styles.field}>
          <span className={styles.fieldLabel}>Имя</span>
          <span className={styles.fieldValue}>{user.FirstName || '—'}</span>
        </div>
        <div className={styles.field}>
          <span className={styles.fieldLabel}>Отчество</span>
          <span className={styles.fieldValue}>{user.SecondName || '—'}</span>
        </div>
        <div className={styles.field}>
          <span className={styles.fieldLabel}>Фамилия</span>
          <span className={styles.fieldValue}>{user.LastName || '—'}</span>
        </div>
        <div className={styles.field}>
          <span className={styles.fieldLabel}>Роль</span>
          <span className={styles.fieldValue}>{user.Role || '—'}</span>
        </div>
      </div>
      <button className={styles.logout} onClick={handleLogout}>
        Выйти
      </button>
    </div>
  );
}

export default UserPanel;