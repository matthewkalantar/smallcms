<template>
  <div class="dashboard-list">
    <h2 class="text-center mb-4">Your Blog Posts</h2>
    <div v-if="posts && posts.length > 0" class="row">
      <div class="col-md-6 col-lg-4 mb-4" v-for="post in posts" :key="post.id">

        <div class="card h-100 shadow-sm post-card">
          <div class="card-body d-flex flex-column">
            <h5 class="card-title">{{ post.title }}</h5>
            <p class="card-text">{{ post.body ? post.body.substring(0, 100) : '' }}...</p>

            <div class="mt-auto">
              <div class="d-flex justify-content-between">
                <button class="btn btn-outline-primary btn-sm" @click="editPost(post)">
                  <i class="bi bi-pencil-square"></i> Edit
                </button>
                <button class="btn btn-outline-danger btn-sm" @click="deletePost(post.id)">
                  <i class="bi bi-trash"></i> Delete
                </button>
              </div>
            </div>

          </div>
          <div class="card-footer text-muted text-center">
            Created on {{ new Date(post.createdDateUtc).toLocaleDateString() }}
          </div>
        </div>

      </div>
    </div>
    <div v-else class="alert alert-warning text-center">No blog posts available. Create one now!</div>
  </div>
</template>

<script>
  export default {
    props: {
      posts: {
        type: Array,
        required: true,
      },
    },
    methods: {
      editPost(post) {
        this.$emit('edit-blog', post);
      },
      deletePost(postId) {
        this.$emit('delete-blog', postId);
      },
    },
  };
</script>

