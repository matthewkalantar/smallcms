
<template>
  <header class="page-header-ui page-header-ui-dark bg-gradient-primary-to-secondary">
    <div class="page-header-ui-content pt-10">
      <div class="container px-5">
        <div class="row gx-5 align-items-center">
          <div class="col-lg-6">
            <h1 class="page-header-ui-title">
              This is a simple CMS blog website
            </h1>
            <p class="page-header-ui-text mb-5">
              in the dashboar panel you can add or edit your posts
            </p>
        
            <router-link v-if="!isAuthenticated" class="btn fw-500 ms-lg-4 btn-cta fw-500 me-2" to="/login">Get Started</router-link>
          </div>
          <div class="col-lg-6 d-none d-lg-block">
          <img class="img-fluid" src="../assets/header.svg">
          </div>
        </div>
      </div>
    </div>
    <div class="svg-border-rounded text-white">
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 144.54 17.34" preserveAspectRatio="none" fill="currentColor"><path d="M144.54,17.34H0V0H144.54ZM0,0S32.36,17.34,72.27,17.34,144.54,0,144.54,0"></path></svg>
    </div>
  </header>
  <div class="container mt-5">
    <h1 class="text-center mb-4 section-title" id="bloglist">All Blog Posts</h1>
    <!--<div v-if="posts.length === 0" class="alert alert-warning text-center">
    No blog posts available.
  </div>
  <div v-else class="row">
    <BlogPostCard v-for="post in posts" :key="post.id" :post="post" />
  </div>-->
    <div v-if="posts.length === 0" class="alert alert-warning text-center">
      No blog posts available.
    </div>

    <div v-else class="row">
      <BlogPostCard v-for="post in posts" :key="post.id" :post="post" />
    </div>

    <!-- Pagination -->
    <nav v-if="totalPages > 1" class="d-flex justify-content-center mt-4">
      <ul class="pagination">
        <li class="page-item" :class="{ disabled: pageNumber === 1 }">
          <button class="page-link" @click="previousPage" :disabled="pageNumber === 1">Previous</button>
        </li>
        <li class="page-item disabled">
          <span class="page-link">Page {{ pageNumber }} of {{ totalPages }}</span>
        </li>
        <li class="page-item" :class="{ disabled: pageNumber === totalPages }">
          <button class="page-link" @click="nextPage" :disabled="pageNumber === totalPages">Next</button>
        </li>
      </ul>
    </nav>
  </div>
</template>


<script>
  import { blogService } from '../services/blogService';
  import BlogPostCard from '../components/BlogPostCard.vue'; 
  import { ref, watch, onMounted } from 'vue';
  import { useRoute } from 'vue-router';

  export default {
    components: {
      BlogPostCard,
    },
    setup() {
      // Reactive references
      const route = useRoute();
      const posts = ref([]);
      const pageNumber = ref(1);
      const pageSize = ref(5);
      const totalPages = ref(1);
      const isLoading = ref(false);

      // Search term reference, derived from the route
      const searchTerm = ref(route.query.searchTerm || '');

      // Function to load blog posts
      const loadPosts = async () => {
        try {
          isLoading.value = true;

          // Call the service with the search term
          const response = await blogService.getAllBlogPosts(
            pageNumber.value,
            pageSize.value,
            searchTerm.value.trim() || '' // Handle empty search term
          );

          // Set posts and pagination details
          posts.value = Array.isArray(response.items) ? response.items : [];
          pageNumber.value = response.pageNumber || 1;
          totalPages.value = response.totalPages || 1;
        } catch (error) {
          console.error('Failed to load posts', error);
        } finally {
          isLoading.value = false;
        }
      };

      // Load posts when the component is mounted
      onMounted(() => {
        loadPosts();
      });

      // Watch route changes and re-load posts accordingly
      watch(
        () => route.query.searchTerm,
        (newTerm) => {
          searchTerm.value = newTerm || '';
          loadPosts();
        }
      );

      // Pagination controls
      const nextPage = async () => {
        if (pageNumber.value < totalPages.value) {
          pageNumber.value++;
          await loadPosts();
        }
      };

      const previousPage = async () => {
        if (pageNumber.value > 1) {
          pageNumber.value--;
          await loadPosts();
        }
      };

      return {
        posts,
        pageNumber,
        pageSize,
        totalPages,
        isLoading,
        searchTerm,
        loadPosts,
        nextPage,
        previousPage,
      };
    },
  };
</script>
