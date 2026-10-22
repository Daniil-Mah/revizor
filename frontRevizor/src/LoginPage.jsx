import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './LoginPage.css'; // Импортируем стили

function LoginPage() {
  const [name, setName] = useState('');
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
        body: JSON.stringify({ name, password }),
      });
  
      if (!res.ok) throw new Error('Неверный логин или пароль');
  
      const data = await res.json();
      localStorage.setItem('token', data.data.accessToken);
      
      const me = await fetch('http://localhost:5196/users/me', {
        headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
      });
      const result = await me.json();

      if (result.data.role.name === "admin") {
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
              value={name} 
              onChange={(e) => setName(e.target.value)} 
              placeholder="Введите имя"
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

export default LoginPage;