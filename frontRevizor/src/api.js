export async function apiFetch(url, options = {}) {
  const token = localStorage.getItem('token');
  
  let res = await fetch(url, {
    ...options,
    headers: {
      ...options.headers,
      'Authorization': `Bearer ${token}`
    }
  });

  if (res.status === 401) {
    const refreshRes = await fetch('http://localhost:5196/refresh', {
      method: 'POST',
      credentials: 'include'
    });

    if (!refreshRes.ok) {
      localStorage.removeItem('token');
      window.location.href = '/login';
      throw new Error('Unauthorized');
    }

    const data = await refreshRes.json();
    localStorage.setItem('token', data.data.accessToken);

    res = await fetch(url, {
      ...options,
      headers: {
        ...options.headers,
        'Authorization': `Bearer ${data.data.accessToken}`
      }
    });
  }
  return res;
}