import React, { useEffect, useState } from 'react'
import CardItemCourse from '../components/CardItemCourse';
import '../css/carditem.css';
import Spinner from '../components/Spinner';
import { useNavigate } from 'react-router-dom';
import { useCart } from '../context/CartContext';

const Cart = () => {
    const { cart, totalPrice, getCartItems, handleRemoveFromCart } = useCart();
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        loadCartItems();
    }, []);
 
    const loadCartItems = async () => {
        await getCartItems();
        setLoading(false);
    }

    const handleCheckout = () => {
        navigate("/checkout");
    }

    if (loading) return <Spinner loading={loading} />;

    return (
        <div className='container mt-5'>
            <div className='row'>
                <div className='col-lg-8 col-12 h-100'>
                    <h1 style={styles.headerText}>Shopping Cart</h1>
                    <h4 style={styles.subHeaderText}>Your Cart</h4>
                    <hr />
                    {cart.length === 0 ? (
                        <div>
                            <h1>No items in cart</h1>
                        </div>
                    ) : (
                        cart.map((course) => (
                            <CardItemCourse 
                                key={course.id}
                                course={course} 
                                handleRemoveFromCart={() => handleRemoveFromCart(course.id)} 
                            />
                        ))
                    )}
                </div>

                <div className='col-lg-4 col-12 basketInfoContainer'>
                    <div className='basketInfo'>
                        <h5>Total:</h5>
                        <h1 style={styles.headerText}>£{totalPrice}</h1>
                        <button 
                            className='btn-purple w-100' 
                            onClick={handleCheckout} 
                            disabled={cart.length === 0}
                        >
                            Checkout
                        </button>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Cart;

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
