import { useState, useEffect } from 'react';
import RoleList from './RoleList';

function Role() {
  const [roles, setRoles] = useState([]);

  useEffect(() => {
    fetch('/roles')
      .then(res =>res.json())
      .then(response => {
        setRoles(response.data);
      })
  }, []);

  return (
    <div>
      <h1>Роли</h1> 
      <RoleList roles={roles}/>
    </div>
  );
}

export default Role;