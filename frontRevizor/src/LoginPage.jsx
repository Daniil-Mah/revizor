import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './LoginPage.css'; 

function LoginPage() {
  const [firstName, setFirstName] = useState('');
  const [secondName, setSecondName] = useState('');
  const [lastName, setLastName] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState(''); 
  const navigate = useNavigate();


  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    try {
      const res = await fetch('http://localhost:5196/login', {
        method: 'POST',
        credentials: 'include',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ firstName, secondName, lastName, password }),
      });

      if (!res.ok) throw new Error('Неверный логин или пароль');

      const data = await res.json();
      const token = data.data.accessToken;
      localStorage.setItem('token', token);
      await fetch('http://localhost:5196/users/me', {
        method: 'GET',
        credentials: 'include',
        headers: { 'Authorization': `Bearer ${token}` },
      });

      const userInfo = getUserFromCookie();
      if (!userInfo) throw new Error('Не удалось прочитать данные пользователя');

      if (userInfo.Role.toLowerCase() === 'admin') {
        navigate('/admin');
      } else {
        navigate('/revizor');
      }
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div className="login-container">
      <div className="login-card">
        <h1 className="login-title">Добро пожаловать</h1>
        <p className="login-subtitle">Введите свои данные, чтобы продолжить</p>
        <form onSubmit={handleSubmit} className="login-form">
          <div className="input-group">
            <input 
              type="text" 
              value={secondName} 
              onChange={(e) => setSecondName(e.target.value)} 
              placeholder="Введите фамилию"
              required
            />
          </div>
          <div className="input-group">
            <input 
              type="text" 
              value={firstName} 
              onChange={(e) => setFirstName(e.target.value)} 
              placeholder="Введите имя"
              required
            />
          </div>
          <div className="input-group">
            <input 
              type="text" 
              value={lastName} 
              onChange={(e) => setLastName(e.target.value)} 
              placeholder="Введите отчество"
              required
            />
          </div>
          <div className="input-group">
            <input 
              type="password" 
              value={password} 
              onChange={(e) => setPassword(e.target.value)} 
              placeholder="Введите пароль"
              required
            />
          </div>
          {error && <div className="login-error">{error}</div>}
          <button type="submit" className="login-button">Войти</button>
        </form>
      </div>
    </div>
  );
}

function getUserFromCookie() {
  const match = document.cookie.split('; ').find(row => row.startsWith('userInfo='));
  if (!match) return null;
  const base64 = match.split('=')[1];
  try {
    return JSON.parse(atob(base64));
  } catch {
    return null;
  }
}

export default LoginPage;