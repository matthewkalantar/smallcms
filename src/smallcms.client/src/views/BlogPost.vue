<template>
  <div>
    <Navbar />
    <div v-if="loading">
      <p>Loading post...</p>
    </div>
    <div v-else-if="error">
      <p>Error loading post: {{ error }}</p>
    </div>
    <div v-else>
      <h1>{{ post.title }}</h1>
      <p>{{ post.content }}</p>
      <small>Created on: {{ new Date(post.createdDate).toLocaleDateString() }}</small>
    </div>
  </div>
</template>

<script>
  import Navbar from '../components/Navbar.vue';
  import axios from '../axios';

  export default {
    components: {
      Navbar,
    },
    data() {
      return {
        post: null,
        loading: true,
        error: null,
      };
    },
    async created() {
      try {
        const { id } = this.$route.params;
        const response = await axios.get(`/blogposts/${id}`);
        this.post = response.data;
      } catch (error) {
        this.error = 'Could not load the blog post.';
        console.error(error);
      } finally {
        this.loading = false;
      }
    },
  };
</script>
