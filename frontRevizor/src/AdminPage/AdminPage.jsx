import { useState } from 'react';
import RolePage from './RolePage';
import styles from './AdminPage.module.css'
import MainContent from './MainContent';

function AdminPage() {
  const [activeTab, setActiveTab] = useState('main');

  return (
    <div className={styles.layout}>
      <aside className={styles.sidebar}>
        <h2 className={styles.labelSideBar}>Админ-панель</h2>
        <nav className ={styles.element}>
          <button onClick={() => setActiveTab('main')}>Главная</button>
          <button onClick={() => setActiveTab('role')}>Роли</button>
          <button onClick={() => setActiveTab('user')}>Пользователи</button>
          <button onClick={() => setActiveTab('facility')}>Объекты</button>
          <button onClick={() => setActiveTab('plan')}>Планы</button>
        </nav>
      </aside>
      
      <main className={styles.content}>
        {activeTab === 'main' && <MainContent/>}
        {activeTab === 'role' && <RolePage />}
        {activeTab === 'user' && <h1>Пользователи</h1>}
        {activeTab === 'facility' && <h1>Объекты</h1>}
        {activeTab === 'plan' && <h1>Планы</h1>}
      </main>
    </div>
  );
}

export default AdminPage;