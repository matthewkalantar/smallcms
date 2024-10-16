import { defineStore } from 'pinia';
import axios from '../axios';
import { ENDPOINTS } from '../config/apiConfig';

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: null,
    token: localStorage.getItem('token') || null,
  }),
  actions: {
    async login(email, password) {
      try {
        const response = await axios.post(ENDPOINTS.LOGIN, { email, password });
        this.token = response.data.token;
        this.user = email; 

        localStorage.setItem('token', this.token);
        localStorage.setItem('user', email);

        axios.defaults.headers.common['Authorization'] = `Bearer ${this.token}`;
      } catch (error) {
        console.error('Login failed', error);
      }
    },
    logout() {
      this.user = null;
      this.token = null;

      localStorage.removeItem('token');
      localStorage.removeItem('user');

      delete axios.defaults.headers.common['Authorization'];
    },
  },
});
