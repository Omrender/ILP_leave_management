import React, { useState } from 'react';
import axios from 'axios';
import { useAuth } from '../../context/AuthContext';

const RequestLeavePage = () => {
  const [leaveTypeId, setLeaveTypeId] = useState(1);
  const [fromDate, setFromDate] = useState('');
  const [toDate, setToDate] = useState('');
  const [reason, setReason] = useState('');
  const { user } = useAuth();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await axios.post(
        'http://localhost:5190/api/LeaveRequests',
        { leaveTypeId, fromDate, toDate, reason },
        { headers: { Authorization: `Bearer ${user.token}` } }
      );
      alert("Leave request submitted.");
    } catch {
      alert("Failed to request leave.");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Request Leave</h2>
      <label>Leave Type ID: <input type="number" value={leaveTypeId} onChange={e => setLeaveTypeId(+e.target.value)} /></label><br/>
      <label>From: <input type="date" value={fromDate} onChange={e => setFromDate(e.target.value)} /></label><br/>
      <label>To: <input type="date" value={toDate} onChange={e => setToDate(e.target.value)} /></label><br/>
      <label>Reason: <input value={reason} onChange={e => setReason(e.target.value)} /></label><br/>
      <button type="submit">Submit</button>
    </form>
  );
};

export default RequestLeavePage;
