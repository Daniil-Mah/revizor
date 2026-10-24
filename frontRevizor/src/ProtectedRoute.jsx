import { Navigate } from 'react-router-dom';

function ProtectedRoute({ children, role }) {
  const user = getUserFromCookie();
  
  if (!user) return <Navigate to="/login" replace />;

  if (role && user.Role.toLowerCase() !== role.toLowerCase()) {
    return <Navigate to="/login" replace />;
  }
  return children;
}

function getUserFromCookie() {
  const match = document.cookie.split('; ').find(row => row.startsWith('userInfo='));
  if (!match) return null;
  const base64 = match.split('=')[1];
  if (!base64) return null;
  try {
    return JSON.parse(atob(base64));
  } catch {
    return null;
  }
}

export default ProtectedRoute