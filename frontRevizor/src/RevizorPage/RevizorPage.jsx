import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { apiFetch } from '../api';
import UserPanel from './UserPanel';
import styles from './RevizorPage.module.css';

const API_BASE = 'http://localhost:5196';

function formatDate(value) {
  if (!value) return '—';
  const d = new Date(value);
  return isNaN(d.getTime()) ? '—' : d.toLocaleDateString('ru-RU');
}

function formatTime(value) {
  if (!value) return '—';
  const d = new Date(value);
  return isNaN(d.getTime())
    ? '—'
    : d.toLocaleTimeString('ru-RU', { hour: '2-digit', minute: '2-digit' });
}

function RevizorPage() {
  const [assigments, setAssigments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    let cancelled = false;

    apiFetch(`${API_BASE}/plan/current`, { method: 'GET' })
      .then((res) => {
        if (res.status === 404) return { data: [] };
        if (!res.ok) throw new Error(`Ошибка ${res.status}`);
        return res.json();
      })
      .then((body) => {
        if (cancelled) return;
        const raw = Array.isArray(body?.data) ? body.data : [];
        const sorted = [...raw].sort(
          (a, b) => new Date(b.passAt).getTime() - new Date(a.passAt).getTime()
        );
        setAssigments(sorted);
      })
      .catch((err) => {
        if (!cancelled) setError(err.message || 'Не удалось загрузить план');
      })
      .finally(() => {
        if (!cancelled) setLoading(false);
      });

    return () => { cancelled = true; };
  }, []);

  const handleCardClick = (a) => {
    navigate(`/record/${a.id}`, {
      state: {
        assigmentId: a.id,
        facilityTitle: a.facility?.title ?? '',
        facilityLocation: a.facility?.location ?? '',
        passAt: a.passAt,
      },
    });
  };

  return (
    <div className={styles.page}>
      <header className={styles.header}>
        <button className={styles.navButton} onClick={() => navigate('/reports')}>
          Отчёты
        </button>
        <UserPanel />
      </header>

      <main className={styles.content}>
        {loading && <div className={styles.message}>Загрузка плана…</div>}
        {!loading && error && <div className={styles.error}>{error}</div>}
        {!loading && !error && assigments.length === 0 && (
          <div className={styles.message}>Активного плана нет</div>
        )}
        {!loading && !error && assigments.length > 0 && (
          <div className={styles.list}>
            {assigments.map((a, index) => (
              <button
                key={a.id}
                className={styles.card}
                onClick={() => handleCardClick(a)}
                style={{ animationDelay: `${index * 0.06}s` }}
              >
                <span className={styles.cardRow}>
                  <span className={styles.cardLabel}>Дата</span>
                  <span className={styles.cardValue}>{formatDate(a.passAt)}</span>
                </span>
                <span className={styles.cardRow}>
                  <span className={styles.cardLabel}>Объект</span>
                  <span className={styles.cardValue}>
                    {a.facility?.title || '—'}
                    {a.facility?.location ? `, ${a.facility.location}` : ''}
                  </span>
                </span>
                <span className={styles.cardRow}>
                  <span className={styles.cardLabel}>Время начала</span>
                  <span className={styles.cardValue}>{formatTime(a.passAt)}</span>
                </span>
              </button>
            ))}
          </div>
        )}
      </main>
    </div>
  );
}

export default RevizorPage;