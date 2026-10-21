import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function LoginPage() {
  const [name, setName] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  
  const handleSubmit = async (e) => {
      e.preventDefault();
      try {
        const res = await fetch('http://localhost:5196/login', 
            {
                method: 'POST',
                credentials: 'include',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ name, password }),
            });
    
    if (!res.ok) throw new Error('Неверный логин или пароль');
    
    const data = await res.json();
    localStorage.setItem('token', data.data.accessToken);
    navigate('/roles');
  } catch (err) {
    console.log(err.message);
  }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h1>Форма входа</h1>
      <label>Имя</label>
      <br/>
      <input 
        type="text" 
        value={name} 
        onChange={(e) => setName(e.target.value)} 
      />
      <br/>
      <label>Пароль</label>
      <br/>
      <input 
        type="password" 
        value={password} 
        onChange={(e) => setPassword(e.target.value)} 
      />
      <br/>
      <button type="submit">Войти</button>
    </form>
  );
}

export default LoginPage;