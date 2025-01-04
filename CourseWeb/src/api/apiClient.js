import axios from "axios";
import { Navigate } from "react-router-dom";

const apiClient = axios.create({
    baseURL: "http://localhost:7001/api/v1",
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
    (error) => {
      if(error.code === "ECONNABORTED"){
        return {code: 408};
      }
      if (error.response) {
        const { status } = error.response;
        if (status === 401) {
          console.log('Unauthorized - Redirecting to login.');
          window.location.href = '/login';
        } else if (status === 403) {
          console.log('Forbidden - Displaying error message.');
          alert('You do not have permission to perform this action.');
        }
      }
      return Promise.reject(error.response.data.problemDetails.errors[0]);
    }
  );

export default apiClient;