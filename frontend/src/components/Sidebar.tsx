import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Sidebar = () => {
  const { user, logout } = useAuth();

  return (
    <aside style={{ width: '200px', padding: '20px', backgroundColor: '#f2f2f2' }}>
      <nav>
        {user?.role === 'Employee' && (
          <>
            <Link to="/employee">Dashboard</Link><br/>
            <Link to="/employee/request">Request Leave</Link><br/>
            <Link to="/employee/history">Leave History</Link><br/>
          </>
        )}
        {user?.role === 'Manager' && (
          <>
            <Link to="/manager">Pending Approvals</Link><br/>
            <Link to="/manager/history">All History</Link><br/>
          </>
        )}
        <button onClick={logout}>Logout</button>
      </nav>
    </aside>
  );
};

export default Sidebar;