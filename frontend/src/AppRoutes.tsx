
import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import Layout from './components/Layout';
import EmployeeDashboard from './pages/employee/EmployeeDashboard';
import RequestLeavePage from './pages/employee/RequestLeavePage';
import LeaveHistoryPage from './pages/employee/LeaveHistoryPage';
import ManagerDashboard from './pages/manager/ManagerDashboard';
import ManagerHistoryPage from './pages/manager/ManagerHistoryPage';
import UnauthorizedPage from './pages/UnauthorizedPage';
import ProtectedRoute from './components/ProtectedRoute';

const AppRoutes = () => (
  <Routes>
    <Route path="/login" element={<LoginPage />} />
    <Route path="/register" element={<RegisterPage />} />
    <Route element={<Layout />}>
      <Route path="/employee" element={<ProtectedRoute role="Employee"><EmployeeDashboard /></ProtectedRoute>} />
      <Route path="/employee/request" element={<ProtectedRoute role="Employee"><RequestLeavePage /></ProtectedRoute>} />
      <Route path="/employee/history" element={<ProtectedRoute role="Employee"><LeaveHistoryPage /></ProtectedRoute>} />
      <Route path="/manager" element={<ProtectedRoute role="Manager"><ManagerDashboard /></ProtectedRoute>} />
      <Route path="/manager/history" element={<ProtectedRoute role="Manager"><ManagerHistoryPage /></ProtectedRoute>} />
      <Route path="/unauthorized" element={<UnauthorizedPage />} />
    </Route>
    <Route path="*" element={<Navigate to="/login" />} />
  </Routes>
);

export default AppRoutes;
export {};
