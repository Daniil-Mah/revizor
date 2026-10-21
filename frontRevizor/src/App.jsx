import { Routes, Route } from 'react-router-dom';
import RolePage from './RolePage';
import LoginPage from './LoginPage';

function App() {
  return (
    <Routes>
        <Route path="/roles" element={<RolePage />} />
        <Route path="/login" element={<LoginPage />} />
    </Routes>
  );
}

export default App;