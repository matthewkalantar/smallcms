<template>
  <nav class="navbar navbar-marketing navbar-expand-lg navbar-dark fixed-top">
    <div class="container px-5">
      <a class="navbar-brand text-white" href="/">SMALL CMS BLOG</a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-menu"><line x1="3" y1="12" x2="21" y2="12"></line><line x1="3" y1="6" x2="21" y2="6"></line><line x1="3" y1="18" x2="21" y2="18"></line></svg>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav ms-auto me-lg-5">
          <li class="nav-item">
            <router-link class="nav-link" to="/">Home</router-link>
          </li>
        </ul>

        <!-- Search Box -->
        <form @submit.prevent="onSearch" class="d-flex search-form">
          <input class="form-control me-2" type="search" v-model="searchTerm" placeholder="Search blog posts..." aria-label="Search">
          <button class="btn btn-outline-light" type="submit">Search</button>
        </form>

        <router-link v-if="!isAuthenticated" class="btn fw-500 ms-lg-4 btn-cta" to="/login">Login</router-link>
        <span v-if="isAuthenticated" class="user-info">Welcome, {{ userEmail }}</span>
        <router-link v-if="isAuthenticated" class="btn fw-500 ms-lg-4 btn-cta" to="/dashboard">Dashboard</router-link>
        <button class="btn btn-normal" v-if="isAuthenticated" @click="logout">Logout</button>
      </div>
    </div>
  </nav>
</template>

<script>
  import { computed, ref } from 'vue';
  import { useAuthStore } from '../store/auth';
  import { useRouter } from 'vue-router';

  export default {
    setup() {
      const authStore = useAuthStore();
      const router = useRouter();

      const searchTerm = ref('');

      const isAuthenticated = computed(() => !!localStorage.getItem('token'));
      const userEmail = computed(() => localStorage.getItem('user'));

      const logout = () => {
        authStore.logout();
        window.location.href = '/';
      };

      const onSearch = () => {
        router.push({ name: 'Home', query: { searchTerm: searchTerm.value } });
      };

      return {
        isAuthenticated,
        userEmail,
        logout,
        searchTerm,
        onSearch,
      };
    },
  };
</script>


