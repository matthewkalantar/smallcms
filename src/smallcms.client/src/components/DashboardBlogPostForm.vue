<template>
  <div>
    <form @submit.prevent="handleSubmit">
      <div class="mb-4">
        <label for="title" class="form-label">Title</label>
        <input type="text"
               id="title"
               v-model="localBlogPost.title"
               class="form-control form-control-lg"
               placeholder="Enter blog title"
               required />
      </div>
      <div class="mb-4">
        <label for="body" class="form-label">Content</label>
        <textarea id="body"
                  v-model="localBlogPost.body"
                  class="form-control form-control-lg"
                  placeholder="Enter blog content"
                  rows="6"
                  required></textarea>
      </div>
      <div class="text-center">
        <button type="submit" class="btn btn-primary btn-lg w-100">
          {{ isEditMode ? 'Update Blog Post' : 'Create Blog Post' }}
        </button>
      </div>
    </form>
  </div>
</template>

<script>
  export default {
    props: {
      blogPost: {
        type: Object,
        default: () => ({ title: '', body: '' }),
      },
      isEditMode: {
        type: Boolean,
        default: false,
      },
    },
    data() {
      return {
        localBlogPost: { ...this.blogPost },
      };
    },
    watch: {
      blogPost: {
        immediate: true,
        handler(newValue) {
          this.localBlogPost = { ...newValue };
        },
      },
    },
    methods: {
      handleSubmit() {
        // Emit the event with the updated blogPost object
        if (this.isEditMode) {
          this.$emit('update-blog', this.localBlogPost);
        } else {
          this.$emit('add-blog', this.localBlogPost);
        }
      },
    },
  };
</script>
