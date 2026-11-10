import { Routes, Route } from 'react-router-dom';
import LoginPage from './LoginPage';
import AdminPage from './AdminPage/AdminPage';
import RevizorPage from './RevizorPage/RevizorPage';
import RecordPage from './RevizorPage/RecordPage';
import ReportsPage from './RevizorPage/ReportsPage';
import ProtectedRoute from './ProtectedRoute';

function App() {
  return (
    <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route 
          path="/admin" 
          element={
          <ProtectedRoute role="admin">
            <AdminPage />
          </ProtectedRoute>
          }
        />
        <Route 
          path="/revizor" 
          element={
          <ProtectedRoute role="revizor">
            <RevizorPage />
          </ProtectedRoute>
          }
        />
        <Route 
          path="/record/:assigmentId" 
          element={
          <ProtectedRoute role="revizor">
            <RecordPage />
          </ProtectedRoute>
          }
        />
        <Route 
          path="/reports" 
          element={
          <ProtectedRoute role="revizor">
            <ReportsPage />
          </ProtectedRoute>
          }
        />
    </Routes>
  );
}

export default App;