import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import axios from 'axios';

const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const { login } = useAuth();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await axios.post('http://localhost:5190/api/Auth/login', { email, password });
      const { token, role } = res.data;
      login({ name: email, email, role, token });
      navigate(role === 'Manager' ? '/manager' : '/employee');
    } catch (err: any) {
      if (err.response?.status === 401) {
        alert("User not found. Redirecting to register...");
        navigate('/register');
      } else {
        alert("Login failed.");
      }
    }
  };

  return (
    <form onSubmit={handleLogin}>
      <h2>Login</h2>
      <input placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} required /><br/>
      <input type="password" placeholder="Password" value={password} onChange={e => setPassword(e.target.value)} required /><br/>
      <button type="submit">Login</button>
    </form>
  );
};

export default LoginPage;