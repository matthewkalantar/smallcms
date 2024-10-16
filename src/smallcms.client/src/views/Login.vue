<template>
  <div class="login-container d-flex justify-content-center align-items-center vh-100">
    <div class="card shadow-sm p-4">
      <div class="card-body">

        <h2 class="text-center mb-4">Login</h2>

        <form @submit.prevent="login">
          <div class="mb-3">
            <label for="email" class="form-label">Email</label>
            <div class="input-group">
              <span class="input-group-text"><i class="bi bi-envelope-fill"></i></span>
              <input type="email" id="email" class="form-control" v-model="email" required placeholder="Enter your email" />
            </div>
          </div>

          <div class="mb-3">
            <label for="password" class="form-label">Password</label>
            <div class="input-group">
              <span class="input-group-text"><i class="bi bi-lock-fill"></i></span>
              <input type="password" id="password" class="form-control" v-model="password" required placeholder="Enter your password" />
            </div>
          </div>

          <div class="d-grid">
            <button type="submit" class="btn btn-primary fw-bold">Login</button>
          </div>
          <div v-if="loginError" class="alert alert-danger mt-3 text-center">
            {{ loginError }}
          </div>

        </form>
      </div>
    </div>
  </div>
</template>
<script>
  import { useAuthStore } from '../store/auth';
  import { ENDPOINTS } from '../config/apiConfig';

  export default {
    data() {
      return {
        email: '',
        password: '',
      };
    },
    methods: {
      async login() {
        const authStore = useAuthStore();
        await authStore.login(this.email, this.password, ENDPOINTS.LOGIN);
        if (authStore.token) {
          this.$router.push({ name: 'Dashboard' });
        } else {
          alert('Login failed');
        }
      },
    },
  };
</script>
