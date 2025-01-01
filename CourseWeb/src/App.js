import React from 'react';
import { Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import Login from './pages/Login';
import Profile from './pages/Profile';
import ProtectedRoute from './components/ProtectedRoute';
import CourseDetail from './pages/CourseDetail';
import Cart from './pages/Cart';
import NotFound from './pages/NotFound';
function App() {
  return (
    <AuthProvider>
      <Navbar/>
      <Routes>
        <Route path="/login" element={<Login/>}></Route>
        <Route path="/profile" element={<ProtectedRoute><Profile/></ProtectedRoute>}></Route>
        <Route path="/cart" element={<Cart/>}></Route>
        <Route path="/" element={<HomePage/>}></Route>
        <Route path="/course/:courseId" element={<CourseDetail/>}></Route>
        <Route path="*" element={<NotFound/>}></Route>
      </Routes>
    </AuthProvider>
  );
}

export default App;
