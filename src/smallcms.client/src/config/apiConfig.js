const BASE_URL = 'http://localhost:5261/api';

const ENDPOINTS = {
  BLOG_POSTS: `${BASE_URL}/blogposts`,
  USER_BLOG_POSTS: `${BASE_URL}/user/blogposts`,
  BLOG_POST: (id) => `${BASE_URL}/blogposts/${id}`,
  LOGIN: `${BASE_URL}/auth/login`,

};

export { BASE_URL, ENDPOINTS };
