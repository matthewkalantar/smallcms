<template>
  <div class="col-md-4 mb-4">
    <div class="post-item h-100">
      <div class="post-item-body">
        <div class="img-container mb-3">
        </div>
        <div class="post-detail">
          <h5 class="post-title">{{ post.title }}</h5>
          <router-link :to="{ name: 'BlogDetails', params: { id: post.id } }" class="read-more">
            Read more
          </router-link>
        </div>
      
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    props: {
      post: {
        type: Object,
        required: true,
      },
    },
  };
</script>

