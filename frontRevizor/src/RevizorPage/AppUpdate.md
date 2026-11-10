```
import { Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './LoginPage';
import AdminPage from './AdminPage/AdminPage';
import RevizorPage from './RevizorPage/RevizorPage';
import RecordPage from './RevizorPage/RecordPage';
import ReportsPage from './RevizorPage/ReportsPage';
import ProtectedRoute from './ProtectedRoute';

function getUserFromCookie() {
  const match = document.cookie
    .split('; ')
    .find((row) => row.startsWith('userInfo='));
  if (!match) return null;
  const base64 = match.split('=')[1];
  if (!base64) return null;
  try {
    const padded = base64 + '='.repeat((4 - (base64.length % 4)) % 4);
    return JSON.parse(atob(padded));
  } catch {
    return null;
  }
}

function RoleRedirect() {
  const user = getUserFromCookie();
  if (!user) return <Navigate to="/login" replace />;
  const role = (user.Role || '').toLowerCase();
  if (role === 'admin') return <Navigate to="/admin" replace />;
  if (role === 'revizor') return <Navigate to="/revizor" replace />;
  return <Navigate to="/login" replace />;
}

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

      <Route path="/" element={<RoleRedirect />} />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>
  );
}

export default App;
```
Проблемы исходника:
1. Нет маршрутов `/revizor`, `/record/:id`, `/reports` — страницы ревизора просто некуда вешать.
2. `<ProtectedRoute>` без `role="admin"` — ревизор, залогинившись, мог открыть `/admin`.
3. Нет маршрута `/` — если пользователь откроет корень, React Router покажет пустоту.
4. Нет `*` — опечатка в URL (`/revizro`) даёт пустой экран вместо редиректа.

## 2. Что стало и почему

### 2.1. Новые импорты (3 строки)

```jsx
import RevizorPage from './RevizorPage/RevizorPage';
import RecordPage from './RevizorPage/RecordPage';
import ReportsPage from './RevizorPage/ReportsPage';
import { Navigate } from 'react-router-dom';
```

Без них маршруты некуда сослаться. `Navigate` нужен для `RoleRedirect` и `*`. **Это неизбежно**, если страницы лежат в отдельной папке.

### 2.2. Три новых маршрута

```jsx
<Route path="/revizor" element={
  <ProtectedRoute role="revizor"><RevizorPage /></ProtectedRoute>
} />

<Route path="/record/:assigmentId" element={
  <ProtectedRoute role="revizor"><RecordPage /></ProtectedRoute>
} />

<Route path="/reports" element={
  <ProtectedRoute role="revizor"><ReportsPage /></ProtectedRoute>
} />
```

Каждый маршрут — одно требование из ТЗ: главная ревизора, форма записи, архивные отчёты. `role="revizor"` гарантирует, что админ или аноним туда не попадут.

### 2.3. `role="admin"` на `/admin` (одна правка в существующем маршруте)

```diff
- <ProtectedRoute>
+ <ProtectedRoute role="admin">
    <AdminPage />
  </ProtectedRoute>
```

Это **исправление бага**, а не расширение. `ProtectedRoute` уже умеет проверять роль — просто в `App.jsx` её не передавали. Без этой строки ревизор после логина мог вручную открыть `/admin` и увидеть интерфейс админки.

### 2.4. `RoleRedirect` на `/`

```jsx
function RoleRedirect() {
  const user = getUserFromCookie();
  if (!user) return <Navigate to="/login" replace />;
  const role = (user.Role || '').toLowerCase();
  if (role === 'admin') return <Navigate to="/admin" replace />;
  if (role === 'revizor') return <Navigate to="/revizor" replace />;
  return <Navigate to="/login" replace />;
}
```

Зачем: `LoginPage` после входа уходит на `/admin` или `/revizor` напрямую. Но если залогиненный пользователь откроет корень `http://localhost:5173/` — React Router без этого маршрута покажет пустоту. `RoleRedirect` разводит по роли.

Если тебе этот маршрут не нужен — можно его удалить, тогда `/` будет пустым. Но это ломает UX: пользователь, открывший корень, не поймёт, что делать.

### 2.5. `*` → `/`

```jsx
<Route path="*" element={<Navigate to="/" replace />} />
```

Ловит опечатки в URL (`/revizro`, `/record` без id). Без него — белый экран.


```

Здесь:
- 4 новых импорта + 3 новых блока `<Route>`.
- Одна правка: `role="admin"` в `/admin`.
- Нет `RoleRedirect`, нет `*`, нет `/`. Если пользователь откроет корень — пусто. Если это неудобно, добавь `RoleRedirect` из полной версии.

**Это единственные изменения, которые нельзя убрать** — без новых маршрутов страницы не откроются. Всё остальное (`RoleRedirect`, `*`, `role="admin"`) — либо исправление бага, либо UX-надстройка.

---

## 4. Итог

| Изменение | Обязательно? | Причина |
|---|---|---|
| Импорты `RevizorPage`/`RecordPage`/`ReportsPage` | Да | Без них маршруты не на что ссылаться |
| 3 новых `<Route>` | Да | Иначе страницы не открываются |
| `role="admin"` в `/admin` | Да (баг-фикс) | Иначе ревизор попадает в админку |
| `RoleRedirect` на `/` | Нет | Только для удобства — развод по роли с корня |
| `*` → `/` | Нет | Только ловит опечатки в URL |

Если хочешь — оставь минимальную версию (раздел 3), и `App.jsx` будет отличаться от исходника на 4 импорта + 3 `<Route>` + одну правку `role="admin"`.