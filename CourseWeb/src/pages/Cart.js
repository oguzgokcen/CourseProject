import React, { useEffect, useState } from 'react'
import CardItemCourse from '../components/CardItemCourse';
import '../css/carditem.css';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import Spinner from '../components/Spinner';
import NotFound from './NotFound';
import alertify from "alertifyjs";

const Cart = () => {
    
    const [courseItems, setCourseItems] = useState([]);
    const [totalPrice, setTotalPrice] = useState(0);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getCartItems();
    }, []);

    const getCartItems = async () => {
        try {
            const response = await apiClient.get(endpoints.cart);
            setCourseItems(response.data.data.courses);
            setTotalPrice(response.data.data.totalPrice);
        } catch (error) {
            setError(error);
        }
        setLoading(false);
    }

    const handleRemoveFromCart = async (courseId) => {
        try {
            const response = await apiClient.delete(`${endpoints.cart}/${courseId}`);
            if(response.status === 200){
                alertify.success("Course removed from cart");
                getCartItems();
            }else{
                alertify.error(response.data.problemDetails.errors[0]);
            }
        } catch (error) {
            alertify.error(error);
        }
    }


    if (loading) return <Spinner loading={loading} />;

    return (
        <div className='container mt-5'>
            <div className='row'>
                <div className='col-lg-8 col-12 h-100'>
                    <h1 style={styles.headerText}>Shopping Cart</h1>
                    <h4 style={styles.subHeaderText}>Your Cart</h4>
                    <hr />
                    {courseItems.length === 0 ? (
                        <div>
                            <h1>No items in cart</h1>
                        </div>
                    ) : (
                        courseItems.map((course) => (
                            <CardItemCourse course={course} handleRemoveFromCart={() => handleRemoveFromCart(course.id)} />
                        ))
                    )}
                </div>

                <div className='col-lg-4 col-12 basketInfoContainer'>
                    <div className='basketInfo'>
                        <h5>Total:</h5>
                        <h1 style={styles.headerText}>£{totalPrice}</h1>
                        <button className='btn-purple w-100' disabled={courseItems.length === 0}>Checkout</button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Cart

const styles = {
    headerText: {
        fontWeight: 'bold',
    },
    subHeaderText: {
        fontWeight: 'bold',
        fontSize: '20px',
        marginTop: '30px',
    },
    cardItemContainer:
    {
        display: 'flex',
        flexWrap: 'wrap',
        flexDirection: 'column',
        justifyContent: 'flex-start',
        alignItems: 'flex-start',
        marginTop: '30px',
    },
}
