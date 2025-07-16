import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5190/api',
});

// Attach JWT token from localStorage (AuthContext)
axiosInstance.interceptors.request.use((config) => {
  const user = localStorage.getItem('user');
  if (user) {
    const { token } = JSON.parse(user);
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default axiosInstance;
export {};