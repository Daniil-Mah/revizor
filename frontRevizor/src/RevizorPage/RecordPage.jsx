import { useEffect, useState } from 'react';
import { useNavigate, useParams, useLocation } from 'react-router-dom';
import { apiFetch } from '../api';
import UserPanel from './UserPanel';
import styles from './RecordPage.module.css';

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

function RecordPage() {
  const { assigmentId } = useParams();
  const location = useLocation();
  const navigate = useNavigate();

  const [assigment, setAssigment] = useState(location.state || null);
  const [loading, setLoading] = useState(!location.state);
  const [error, setError] = useState(null);

  const [endAt, setEndAt] = useState('');
  const [passageWay, setPassageWay] = useState('');
  const [submitting, setSubmitting] = useState(false);
  const [formError, setFormError] = useState(null);

  useEffect(() => {
    if (assigment) return;

    let cancelled = false;
    apiFetch(`${API_BASE}/plan/current`, { method: 'GET' })
      .then((res) => {
        if (!res.ok) throw new Error(`Ошибка ${res.status}`);
        return res.json();
      })
      .then((body) => {
        if (cancelled) return;
        const list = Array.isArray(body?.data) ? body.data : [];
        const found = list.find((a) => a.id === assigmentId);
        if (!found) throw new Error('Назначение не найдено');
        setAssigment({
          assigmentId: found.id,
          facilityTitle: found.facility?.title ?? '',
          facilityLocation: found.facility?.location ?? '',
          passAt: found.passAt,
        });
      })
      .catch((err) => {
        if (!cancelled) setError(err.message);
      })
      .finally(() => {
        if (!cancelled) setLoading(false);
      });

    return () => { cancelled = true; };
  }, [assigment, assigmentId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setFormError(null);

    try {
      if (!/^\d{2}:\d{2}$/.test(endAt)) {
        throw new Error('Укажите время окончания в формате HH:MM');
      }
      const [h, m] = endAt.split(':').map(Number);
      if (h < 0 || h > 23 || m < 0 || m > 59) {
        throw new Error('Некорректное время окончания');
      }
      const endAtTime = `${String(h).padStart(2, '0')}:${String(m).padStart(2, '0')}:00`;

      let passageWayValue;
      if (passageWay !== '') {
        const num = Number(passageWay);
        if (!Number.isFinite(num) || num < 0) {
          throw new Error('Стоимость проезда должна быть неотрицательным числом');
        }
        passageWayValue = num;
      }

      const payload = {
        planAssigmentId: assigment.assigmentId,
        endAt: endAtTime,
      };
      if (passageWayValue !== undefined) payload.passageWay = passageWayValue;

      setSubmitting(true);
      const res = await apiFetch(`${API_BASE}/record/`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      if (!res.ok) {
        throw new Error(`Ошибка сервера: ${res.status}`);
      }

      navigate('/revizor', { replace: true });
    } catch (err) {
      setFormError(err.message);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className={styles.page}>
      <header className={styles.header}>
        <button className={styles.navButton} onClick={() => navigate('/revizor')}>
          ← На главную
        </button>
        <UserPanel />
      </header>

      <main className={styles.content}>
        {loading && <div className={styles.message}>Загрузка…</div>}
        {!loading && error && <div className={styles.error}>{error}</div>}
        {!loading && !error && assigment && (
          <form className={styles.form} onSubmit={handleSubmit}>
            <div className={styles.field}>
              <span className={styles.label}>Дата</span>
              <div className={styles.readonly}>{formatDate(assigment.passAt)}</div>
            </div>

            <div className={styles.field}>
              <span className={styles.label}>Объект</span>
              <div className={styles.readonly}>
                {assigment.facilityTitle || '—'}
                {assigment.facilityLocation ? `, ${assigment.facilityLocation}` : ''}
              </div>
            </div>

            <div className={styles.field}>
              <span className={styles.label}>Время начала</span>
              <div className={styles.readonly}>{formatTime(assigment.passAt)}</div>
            </div>

            <div className={styles.field}>
              <span className={styles.label}>Время окончания</span>
              <span className={styles.hint}>Формат: 14:30</span>
              <input
                className={styles.input}
                type="time"
                value={endAt}
                onChange={(e) => setEndAt(e.target.value)}
                required
              />
            </div>

            <div className={styles.field}>
              <span className={styles.label}>Стоимость проезда</span>
              <span className={styles.hint}>Например: 150.5</span>
              <input
                className={styles.input}
                type="number"
                min="0"
                step="0.01"
                value={passageWay}
                onChange={(e) => setPassageWay(e.target.value)}
                placeholder="0"
              />
            </div>

            {formError && <div className={styles.formError}>{formError}</div>}

            <button className={styles.submit} type="submit" disabled={submitting}>
              {submitting ? 'Отправка…' : 'Отправить запись'}
            </button>
          </form>
        )}
      </main>
    </div>
  );
}

export default RecordPage;