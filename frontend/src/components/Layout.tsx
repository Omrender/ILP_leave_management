import React from 'react';
import { Outlet } from 'react-router-dom';
import Sidebar from './Sidebar';
import Navbar from './Navbar';
import './Layout.css';

const Layout = () => (
  <div className="layout">
    <Navbar />
    <div className="main">
      <Sidebar />
      <div className="content">
        <Outlet />
      </div>
    </div>
  </div>
);

export default Layout;