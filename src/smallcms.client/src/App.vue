<template>
    <main>
      <Navbar />
      <router-view />
    </main>
</template>

<script>
  import Navbar from './components/Navbar.vue';
  import { useAuthStore } from './store/auth';

  export default {
    name: 'App',
    components: {
      Navbar,
    },
    mounted() {
    },
  };
</script>

