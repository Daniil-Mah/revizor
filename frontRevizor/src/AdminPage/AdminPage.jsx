import { useState } from 'react';
import RolePage from './RolePage';
import styles from './AdminPage.module.css';
import MainContent from './MainContent';
import UserContent from './UserContent';
import FacilityContent from './FacilityContent';
import ReportContent from './ReportsContent';
import PlanContent from './PlanContent';

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
          <button className={activeTab === 'report' ? styles.active : ''} onClick={() => setActiveTab('report')}>Отчеты</button>
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
          {activeTab === 'user' && <UserContent />}
          {activeTab === 'facility' && <FacilityContent />}
          {activeTab === 'plan' && <PlanContent />}
          {activeTab === 'report' && <ReportContent/>}
        </main>
      </div>
    </div>
  );
}

export default AdminPage;