import { useEffect, useState } from 'react';
import styles from './ReportContent.module.css';
import { apiFetch } from '../api';

function ReportContent() {
  const [report, setReport] = useState([]);
  const [selectReport, setSelectReport] = useState(null);
  const [reportData, setReportData] = useState(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    apiFetch('http://localhost:5196/get-reports', {
      headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
    })
      .then(res => res.json())
      .then(data => setReport(data.data || []))
      .catch(err => console.error(err));
  }, []);

  useEffect(() => {
    if (!selectReport) {
      setReportData(null);
      return;
    }
    
    setLoading(true);
    fetch(`http://localhost:5196/get-reports/${selectReport.planId}`, {
      headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
    })
      .then(res => res.json())
      .then(data => {
        setReportData(data.data);
        setLoading(false);
      })
      .catch(err => {
        console.error(err);
        setLoading(false);
      });
  }, [selectReport]);

  async function downloadReport(planId) {
    const token = localStorage.getItem('token');
    
    const res = await fetch(`http://localhost:5196/download-report/${planId}`, {
      headers: { 'Authorization': `Bearer ${token}` }
    });
    
    if (!res.ok) throw new Error('Ошибка скачивания');
    
  const contentDisposition = res.headers.get('Content-Disposition');
  let fileName = `report_${planId}.xlsx`;
    if (contentDisposition) {
      const match = contentDisposition.match(/filename="?([^";]+)"?/);
      if (match) fileName = match[1];
    }

    const blob = await res.blob();
    const url = window.URL.createObjectURL(blob);
    
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    
    link.remove();
    window.URL.revokeObjectURL(url);
  }

  if (selectReport) {
    return (
      <div>
        <button onClick={() => setSelectReport(null)}>← Назад к списку</button>
        {loading ? ( <h1>Загрузка...</h1> ) : reportData ? 
        (  
        <pre>{JSON.stringify(reportData, null, 2)}</pre> ) : ( <h1>Нет данных</h1>)}
      </div>
    );
  }
  return (
    <div>
      <h1>Отчеты</h1>
      <div>
        <button>Сгенерировать отчет</button>
      </div>

      <div className={styles.buttonContainer}>
        {report.length === 0 ? (
          <h1>Отчетов еще нет</h1>
        ) : (
          report.map(element => (
            <button
              key={element.planId}
              className={styles.button}
              onClick={() => downloadReport(element.planId)}
            >
              {element.name}
            </button>
          ))
        )}
      </div>
    </div>
  );
}

export default ReportContent;