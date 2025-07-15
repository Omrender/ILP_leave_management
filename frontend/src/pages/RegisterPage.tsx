import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const RegisterPage = () => {
  const [form, setForm] = useState({ name: '', email: '', password: '', role: 'Employee' });
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await axios.post('http://localhost:5190/api/Users/register', form);
      alert("User registered! Please login.");
      navigate('/login');
    } catch {
      alert("Registration failed.");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Register</h2>
      <input placeholder="Name" value={form.name} onChange={e => setForm({ ...form, name: e.target.value })} required /><br/>
      <input placeholder="Email" value={form.email} onChange={e => setForm({ ...form, email: e.target.value })} required /><br/>
      <input type="password" placeholder="Password" value={form.password} onChange={e => setForm({ ...form, password: e.target.value })} required /><br/>
      <select value={form.role} onChange={e => setForm({ ...form, role: e.target.value })}>
        <option value="Employee">Employee</option>
        <option value="Manager">Manager</option>
      </select><br/>
      <button type="submit">Register</button>
    </form>
  );
};

export default RegisterPage;