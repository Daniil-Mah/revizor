import { useEffect, useState } from 'react';
import styles from './ReportContent.module.css';

function ReportContent() {
    const [report, setReport] = useState([])

    useEffect(()=> {
        fetch('http://localhost:5196/report')
    })

  return (
    <div className={styles.buttonContainer}>
      <button className={styles.button}>Кнопка 1</button>
      <button className={styles.button}>Кнопка 2</button>
      <button className={styles.button}>Кнопка 3</button>
      <button className={styles.button}>Кнопка 4</button>
      <button className={styles.button}>Кнопка 5</button>
      <button className={styles.button}>Кнопка 6</button>
      <button className={styles.button}>Кнопка 7</button>
      <button className={styles.button}>Кнопка 8</button>
    </div>
  );
}

export default ReportContent;