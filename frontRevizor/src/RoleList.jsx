import RoleItem from './RoleItem';

function RoleList({roles}) {
    return (
        <ul>
            {roles.map(role => <RoleItem key={role.id} role={role}/> )}
        </ul>
    );
}
export default RoleList;