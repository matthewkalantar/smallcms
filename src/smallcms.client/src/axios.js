import axios from 'axios';
import { BASE_URL } from './config/apiConfig';

const instance = axios.create({
  baseURL: BASE_URL,
});

instance.interceptors.request.use(config => {
  const token = localStorage.getItem('token');
  if (token) {
    config.headers['Authorization'] = `Bearer ${token}`;
  }
  return config;
});


instance.interceptors.response.use(
  response => response,
  error => {
    console.error('API error occurred:', error.response || error.message);
    return Promise.reject(error);
  }
);

export default instance;
