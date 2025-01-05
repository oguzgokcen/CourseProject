import axios from "axios";
import { endpoints } from "./endpoints";

const apiClient = axios.create({
  baseURL: endpoints.baseUrl,
  headers: {
    "Content-type": "application/json;",
    "Accept": "application/json",
  },
  timeout: 100000,
});

apiClient.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers["Authorization"] = `Bearer ${token}`;
  }
  return config;
}, (error) => {
  return Promise.reject(error);
}
);

apiClient.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.code === "ECONNABORTED") {
      return { code: 408 };
    }
    
    if (error.response) {
      const originalRequest = error.config;
      
      if (error.response.status === 401 && !originalRequest._retry && originalRequest.url !== endpoints.login) {
        originalRequest._retry = true;
        
        const refreshToken = localStorage.getItem('refreshToken');
        if (!refreshToken) {
          if (window.location.pathname !== '/login') {
            localStorage.removeItem('token');
            localStorage.removeItem('user');
            window.location.href = '/login';
          }
          return Promise.reject(error);
        }

        try {
          const response = await axios.post(endpoints.baseUrl + endpoints.getAccessToken, {
            "refreshToken": refreshToken,
          });
          const { accessToken, refreshToken: newRefreshToken } = response.data.data;
          localStorage.setItem('token', accessToken);
          localStorage.setItem('refreshToken', newRefreshToken);
          apiClient.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
          originalRequest.headers['Authorization'] = `Bearer ${accessToken}`;
          return apiClient(originalRequest);
        } catch (refreshError) {
          console.error('Token refresh failed:', refreshError);
          if (window.location.pathname !== '/login') {
            localStorage.removeItem('token');
            localStorage.removeItem('refreshToken');
            localStorage.removeItem('user');
            window.location.href = '/login';
          }
          return Promise.reject(refreshError);
        }
      }
      
      if (error.response.data?.problemDetails?.errors?.[0]) {
        return Promise.reject(error.response.data.problemDetails.errors[0]);
      }
      return Promise.reject(error);
    }
    return Promise.reject(error);
  }
);

export default apiClient;