import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { apiFetch } from '../api';
import UserPanel from './UserPanel';
import styles from './ReportsPage.module.css';

const API_BASE = 'http://localhost:5196';

function ReportsPage() {
  const [reports, setReports] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [downloadingId, setDownloadingId] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    let cancelled = false;

    const url = `${API_BASE}/get-reports/my?reportId=00000000-0000-0000-0000-000000000000`;

    apiFetch(url, { method: 'GET' })
      .then((res) => {
        if (!res.ok) throw new Error(`Ошибка ${res.status}`);
        return res.json();
      })
      .then((body) => {
        if (cancelled) return;
        const list = Array.isArray(body?.data) ? body.data : [];
        setReports(list);
      })
      .catch((err) => {
        if (!cancelled) setError(err.message);
      })
      .finally(() => {
        if (!cancelled) setLoading(false);
      });

    return () => { cancelled = true; };
  }, []);

  const handleDownload = async (report) => {
    setError(null);
    setDownloadingId(report.id);
    try {
      const res = await apiFetch(`${API_BASE}/download-report/${report.id}`, {
        method: 'GET',
      });

      if (!res.ok) {
        const body = await res.json().catch(() => null);
        throw new Error(body?.message || `Ошибка ${res.status}`);
      }

      const blob = await res.blob();
      const url = URL.createObjectURL(blob);

      const disposition = res.headers.get('Content-Disposition') || '';
      const match = disposition.match(/filename\*?=(?:UTF-8''|")?([^";]+)/i);
      const filename = match
        ? decodeURIComponent(match[1].replace(/"/g, ''))
        : report.fileName || `report_${report.id}.xlsx`;

      const a = document.createElement('a');
      a.href = url;
      a.download = filename;
      document.body.appendChild(a);
      a.click();
      a.remove();
      URL.revokeObjectURL(url);
    } catch (err) {
      setError(err.message);
    } finally {
      setDownloadingId(null);
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
        {loading && <div className={styles.message}>Загрузка отчётов…</div>}
        {!loading && error && <div className={styles.error}>{error}</div>}
        {!loading && !error && reports.length === 0 && (
          <div className={styles.message}>Отчётов пока нет</div>
        )}
        {!loading && !error && reports.length > 0 && (
          <div className={styles.grid}>
            {reports.map((r, index) => (
              <button
                key={r.id}
                className={styles.card}
                onClick={() => handleDownload(r)}
                disabled={downloadingId === r.id}
                style={{ animationDelay: `${index * 0.05}s` }}
                title={r.fileName || r.id}
              >
                <span className={styles.cardTitle}>
                  {r.fileName || `Отчёт ${r.id.slice(0, 8)}`}
                </span>
                <span className={styles.cardStatus}>
                  {downloadingId === r.id ? 'Скачивание…' : 'Скачать'}
                </span>
              </button>
            ))}
          </div>
        )}
      </main>
    </div>
  );
}

export default ReportsPage;