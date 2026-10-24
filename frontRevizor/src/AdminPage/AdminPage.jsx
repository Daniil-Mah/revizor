import { useState } from 'react';
import RolePage from './RolePage';
import styles from './AdminPage.module.css';
import MainContent from './MainContent';

function AdminPage() {
  const [activeTab, setActiveTab] = useState('main');
  const [profileOpen, setProfileOpen] = useState(false);

  return (
    <div className={styles.layout}>
      <aside className={styles.sidebar}>
        <h2 className={styles.labelSideBar}>Админ-панель</h2>
        <nav className={styles.element}>
          <button className={activeTab === 'main' ? styles.active : ''} onClick={() => setActiveTab('main')}>Главная</button>
          <button className={activeTab === 'role' ? styles.active : ''} onClick={() => setActiveTab('role')}>Роли</button>
          <button className={activeTab === 'user' ? styles.active : ''} onClick={() => setActiveTab('user')}>Пользователи</button>
          <button className={activeTab === 'facility' ? styles.active : ''} onClick={() => setActiveTab('facility')}>Объекты</button>
          <button className={activeTab === 'plan' ? styles.active : ''} onClick={() => setActiveTab('plan')}>Планы</button>
        </nav>
      </aside>
      
      <div className={styles.rightSide}>
        <header className={styles.header}>
            <button className={styles.profileButton} onClick={() => setProfileOpen(!profileOpen)}>
              <span className={styles.profileName}>Имя</span>
              <span className={styles.profileAvatar}>A</span>
            </button>

            {profileOpen && (
              <div className={styles.profileMenu}>
                <button>Профиль</button>
                <button>Настройки</button>
                <button>Выйти</button>
              </div>
            )}
        </header>
        
        <main className={styles.content}>
          {activeTab === 'main' && <MainContent />}
          {activeTab === 'role' && <RolePage />}
          {activeTab === 'user' && <h1>Пользователи</h1>}
          {activeTab === 'facility' && <h1>Объекты</h1>}
          {activeTab === 'plan' && <h1>Планы</h1>}
        </main>
      </div>
    </div>
  );
}

export default AdminPage;