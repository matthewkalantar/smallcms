import axios from '../axios';
import { ENDPOINTS } from '../config/apiConfig';

export const blogService = {
  async getAllBlogPosts(pageNumber = 1, pageSize = 20, searchTerm = '') {
    try {
      const response = await axios.get(ENDPOINTS.BLOG_POSTS, {
        params: { pageNumber, pageSize, searchTerm, }
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching all blog posts:', error);
      throw error;
    }
  },
  async getUserBlogPosts(pageNumber = 1, pageSize = 20) {
    try {
      const response = await axios.get(ENDPOINTS.USER_BLOG_POSTS, {
        params: { pageNumber, pageSize }
      });
      return response.data;
    } catch (error) {
      console.error('Error fetching user blog posts:', error);
      throw error;
    }
  },
  async addBlogPost(post) {
    try {
      const response = await axios.post(ENDPOINTS.BLOG_POSTS, post);
      return response.data;
    } catch (error) {
      console.error('Error adding blog post:', error);
      throw error;
    }
  },
  async updateBlogPost(post) {
    try {
      await axios.put(ENDPOINTS.BLOG_POST(post.id), post);
    } catch (error) {
      console.error('Error updating blog post:', error);
      throw error;
    }
  },
  async deleteBlogPost(postId) {
    try {
      await axios.delete(ENDPOINTS.BLOG_POST(postId));
    } catch (error) {
      console.error('Error deleting blog post:', error);
      throw error;
    }
  },
  async getBlogPostById(id) {
    try {
      const response = await axios.get(ENDPOINTS.BLOG_POST(id));
      return response.data;
    } catch (error) {
      console.error('Error fetching blog post by ID:', error);
      throw error;
    }
  },
};
