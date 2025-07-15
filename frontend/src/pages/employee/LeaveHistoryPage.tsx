import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuth } from '../../context/AuthContext';

const LeaveHistoryPage = () => {
  const { user } = useAuth();
  const [requests, setRequests] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:5190/api/LeaveRequests/mine', {
      headers: { Authorization: `Bearer ${user.token}` }
    }).then(res => setRequests(res.data));
  }, [user.token]);

  return (
    <div>
      <h2>My Leave History</h2>
      <ul>
        {requests.map((req: any) => (
          <li key={req.id}>{req.fromDate} to {req.toDate} — {req.status}</li>
        ))}
      </ul>
    </div>
  );
};

export default LeaveHistoryPage;
