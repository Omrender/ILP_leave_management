import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import UnauthorizedPage from './pages/UnauthorizedPage';
import EmployeeDashboard from './pages/employee/EmployeeDashboard';
import RequestLeavePage from './pages/employee/RequestLeavePage';
import LeaveHistoryPage from './pages/employee/LeaveHistoryPage';
import ManagerDashboard from './pages/manager/ManagerDashboard';
import ManagerHistoryPage from './pages/manager/ManagerHistoryPage';
import Layout from './components/Layout';
import ProtectedRoute from './components/ProtectedRoute';

const App = () => {
  return (
    <Routes>
      {/* Public Routes */}
      <Route path="/login" element={<LoginPage />} />
      <Route path="/register" element={<RegisterPage />} />
      <Route path="/unauthorized" element={<UnauthorizedPage />} />

      {/* Protected Routes */}
      <Route element={<Layout />}>
        {/* Employee Routes */}
        <Route
          path="/employee"
          element={
            <ProtectedRoute role="Employee">
              <EmployeeDashboard />
            </ProtectedRoute>
          }
        />
        <Route
          path="/employee/request"
          element={
            <ProtectedRoute role="Employee">
              <RequestLeavePage />
            </ProtectedRoute>
          }
        />
        <Route
          path="/employee/history"
          element={
            <ProtectedRoute role="Employee">
              <LeaveHistoryPage />
            </ProtectedRoute>
          }
        />

        {/* Manager Routes */}
        <Route
          path="/manager"
          element={
            <ProtectedRoute role="Manager">
              <ManagerDashboard />
            </ProtectedRoute>
          }
        />
        <Route
          path="/manager/history"
          element={
            <ProtectedRoute role="Manager">
              <ManagerHistoryPage />
            </ProtectedRoute>
          }
        />
      </Route>

      {/* Fallback Route */}
      <Route path="*" element={<Navigate to="/login" />} />
    </Routes>
  );
};

export default App;
