import { Routes, Route } from 'react-router-dom';
import LoginPage from './LoginPage';
import AdminPage from './AdminPage/AdminPage';
import ProtectedRoute from './ProtectedRoute';

function App() {
  return (
    <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route 
          path="/admin" 
          element={
          <ProtectedRoute>
            <AdminPage />
          </ProtectedRoute>
          }
        />
    </Routes>
  );
}

export default App;