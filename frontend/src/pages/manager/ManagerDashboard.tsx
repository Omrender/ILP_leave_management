import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuth } from '../../context/AuthContext';

const ManagerDashboard = () => {
  const { user } = useAuth();
  const [requests, setRequests] = useState([]);

  const fetchData = async () => {
    const res = await axios.get('http://localhost:5190/api/LeaveRequests/pending', {
      headers: { Authorization: `Bearer ${user.token}` }
    });
    setRequests(res.data);
  };

  const handleApprove = async (id: number, status: string) => {
    await axios.put('http://localhost:5190/api/LeaveRequests/approve', {
      leaveRequestId: id,
      status
    }, {
      headers: { Authorization: `Bearer ${user.token}` }
    });
    fetchData();
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div>
      <h2>Pending Leave Requests</h2>
      <ul>
        {requests.map((req: any) => (
          <li key={req.id}>
            {req.fromDate} to {req.toDate} — {req.reason}
            <button onClick={() => handleApprove(req.id, 'Approved')}>Approve</button>
            <button onClick={() => handleApprove(req.id, 'Rejected')}>Reject</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ManagerDashboard;
