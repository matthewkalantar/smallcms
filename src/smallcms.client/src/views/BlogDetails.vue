<template>
  <div class="container mt-5">
    <div v-if="loading" class="text-center">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>
    <div v-else-if="error" class="alert alert-danger text-center">
      {{ error }}
    </div>
    <div v-else class="card shadow-sm">
      <div class="card-body">
        <h1 class="card-title">{{ blogPost.title }}</h1>
        <p class="card-text">{{ blogPost.body }}</p>
        <p class="text-muted">Published on: {{ formattedDate }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import { blogService } from '../services/blogService';

export default {
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      blogPost: null,
      loading: true,
      error: '',
    };
  },
  computed: {
    formattedDate() {
      return this.blogPost ? new Date(this.blogPost.createdDateUtc).toLocaleDateString() : '';
    },
  },
  async created() {
    try {

      this.blogPost = await blogService.getBlogPostById(this.id);
      this.loading = false;
    } catch (err) {
      console.error('Failed to load blog post:', err);
      this.error = 'Failed to load blog post. Please try again later.';
      this.loading = false;
    }
  },
};
</script>

<style scoped>
  .container {
    max-width: 800px;
    margin: 0 auto;
  }
</style>
