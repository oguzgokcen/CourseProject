import { createContext, useState, useContext, useEffect } from 'react';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import alertify from 'alertifyjs';
import { useAuth } from '../context/AuthContext';

const CartContext = createContext();

export const useCart = () => useContext(CartContext);

export const CartProvider = ({ children }) => {
    const [cart, setCart] = useState([]);
    const [totalPrice, setTotalPrice] = useState(0);
    const { user } = useAuth();

    useEffect(() => {
        if (user) {
            getCartItems();
        } else {
            setCart([]);
            setTotalPrice(0);
        }
    }, [user]);

    const getCartItems = async () => {
        try {
            const response = await apiClient.get(endpoints.cart);
            setCart(response.data.data.courses);
            setTotalPrice(response.data.data.totalPrice);
            return response.data.data.courses;
        } catch (error) {
            alertify.error(error);
            return [];
        }
    }

    const handleRemoveFromCart = async (courseId) => {
        try {
            const response = await apiClient.delete(`${endpoints.cart}/${courseId}`);
            if(response.status === 200){
                alertify.success("Course removed from cart");
                await getCartItems();  // Refresh cart after removal
                return true;
            }else{
                alertify.error(response.data.problemDetails.errors[0]);
                return false;
            }
        } catch (error) {
            alertify.error(error);
            return false;
        }
    }

    const handleAddToCart = async (courseId) => {
        try {
            const response = await apiClient.post(`${endpoints.cart}/${courseId}`);
            if(response.status === 200){
                alertify.success("Course added to basket");
                await getCartItems();  // Refresh cart after adding
                return true;
            }else{
                alertify.error(response);
                return false;
            }
        } catch (error) {
            alertify.error(error);
            return false;
        }
    }

    const checkCourseInCartOrBought = async (courseId) => {
        try {
            const response = await apiClient.get(`${endpoints.checkIfBought}/${courseId}`);
            return response.data.data;
        } catch (error) {
            alertify.error(error);
            return { isBought: false, isOnCart: false };
        }
    }

    const clearCart = () => {
        setCart([]);
        setTotalPrice(0);
    };

    return (
        <CartContext.Provider value={{ 
            cart, 
            totalPrice,
            getCartItems,
            handleAddToCart,
            handleRemoveFromCart,
            checkCourseInCartOrBought,
            clearCart
        }}>
            {children}
        </CartContext.Provider>
    );
};