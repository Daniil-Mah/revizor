import { Routes, Route } from 'react-router-dom';
import LoginPage from './LoginPage';
import AdminPage from './AdminPage/AdminPage';
import RolePage from './AdminPage/RolePage';


function App() {
  return (
    <Routes>
        <Route path="/roles" element={<RolePage />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/admin" element={<AdminPage />} />
    </Routes>
  );
}

export default App;