<template>
  <div class="container mt-5">
    <h1 class="text-center mb-4 section-title">Dashboard</h1>
    <div v-if="message" class="alert alert-success text-center mt-3">
      {{ message }}
    </div>

    <div class="row justify-content-center mb-5">
      <div class="col-md-8">
        <div class="card shadow-lg">
          <div class="card-header bg-primary text-white text-center">
            <h4 class="mb-0">{{ selectedPost ? 'Edit Blog Post' : 'Create New Blog Post' }}</h4>
          </div>
          <div class="card-body">
            <BlogPostForm :blogPost="selectedPost ? selectedPost : newPost"
                          :isEditMode="!!selectedPost"
                          @update-blog="updateBlogPost"
                          @add-blog="addBlogPost" />
          </div>
        </div>
      </div>
    </div>

    <BlogPostList :posts="posts"
                  @edit-blog="editBlogPost"
                  @delete-blog="deleteBlogPost" />
  </div>
</template>

<script>
  import BlogPostForm from '../components/DashboardBlogPostForm.vue';
  import BlogPostList from '../components/DashboardBlogPostList.vue';
  import { blogService } from '../services/blogService';

  export default {
    components: {
      BlogPostForm,
      BlogPostList,
    },
    data() {
      return {
        posts: [],
        selectedPost: null,
        newPost: { title: '', body: '' },
        message: '',
        pageNumber: 1,
        pageSize: 50,
        totalPages: 1,
      };
    },
    methods: {
      async loadUserPosts() {
        try {
          const response = await blogService.getUserBlogPosts(this.pageNumber, this.pageSize);
          this.posts = Array.isArray(response.items) ? response.items : [];
        } catch (error) {
          this.setMessage('Error loading blog posts.');
        }
      },
      async addBlogPost(post) {
        try {
          await blogService.addBlogPost(post);
          await this.loadUserPosts();
          this.setMessage('Blog post added successfully.');
          this.resetNewPost();
        } catch (error) {
          this.setMessage('Error adding blog post.');
        }
      },
      async updateBlogPost(post) {
        try {
          await blogService.updateBlogPost(post);
          await this.loadUserPosts();
          this.setMessage('Blog post updated successfully.');
          this.selectedPost = null;
        } catch (error) {
          this.setMessage('Error updating blog post.');
        }
      },
      async deleteBlogPost(postId) {
        try {
          await blogService.deleteBlogPost(postId);
          await this.loadUserPosts();
          this.setMessage('Blog post deleted successfully.');
        } catch (error) {
          this.setMessage('Error deleting blog post.');
        }
      },
      editBlogPost(post) {
        this.selectedPost = post;
      },
      resetNewPost() {
        this.newPost = { title: '', body: '' };
      },
      setMessage(msg) {
        this.message = msg;
        setTimeout(() => {
          this.message = '';
        }, 3000);
      },
    },
    async created() {
      await this.loadUserPosts();
    },
  };
</script>


