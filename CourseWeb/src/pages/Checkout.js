import React, { useState } from 'react';
import { formatCreditCardNumber, formatExpirationDate, formatCVC , validateCardNumber} from '../components/utils';
import { FaLock, FaCheckCircle } from 'react-icons/fa';
import alertify from 'alertifyjs';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import { useCart } from '../context/CartContext';
import { Modal } from 'reactstrap';

const Checkout = () => {
    const { totalPrice, clearCart } = useCart();
    const [showSuccessModal, setShowSuccessModal] = useState(false);

    const [cardDetails, setCardDetails] = useState({
        number: '',
        name: '',
        expiry: '',
        cvc: '',
    });

    const [errors, setErrors] = useState({});
    const [message, setMessage] = useState('');
    const [isSuccess, setIsSuccess] = useState(false);


    const handleInputChange = ({ target }) => {
        if (target.name === 'number') {
            target.value = formatCreditCardNumber(target.value)
        } else if (target.name === 'expiry') {
            target.value = formatExpirationDate(target.value)
        } else if (target.name === 'cvc') {
            target.value = formatCVC(target.value)
        }

        setCardDetails({ ...cardDetails, [target.name]: target.value })
    }

    const validateForm = () => {
        const newErrors = {};

        if (!cardDetails.name.trim()) {
            newErrors.name = "Name is required";
        }

        if (!cardDetails.number.trim()) {
            newErrors.number = "Card number is required";
        } else if (!/^\d{16,19}$/.test(cardDetails.number.replace(/\s+/g, ''))) {
            newErrors.number = "Card number is invalid";
        } else if (!validateCardNumber(cardDetails.number)) {
            newErrors.number = "Card number is invalid";
        }


        if (!cardDetails.expiry.trim()) {
            newErrors.expiry = "Expiration date is required";
        } else {
            const [month, year] = cardDetails.expiry.split('/');
            const expMonth = parseInt(month, 10);
            const expYear = parseInt(`20${year}`, 10);

            if (
                isNaN(expMonth) ||
                isNaN(expYear) ||
                expMonth < 1 ||
                expMonth > 12
            ) {
                newErrors.expiry = "Invalid expiration date";
            } else {
                const today = new Date();
                const expDate = new Date(expYear, expMonth - 1, 1);
                expDate.setMonth(expDate.getMonth() + 1); 
                expDate.setDate(expDate.getDate() - 1); 

                if (expDate < today) {
                    newErrors.expiry = "Card has expired";
                }
            }
        }

        if (!cardDetails.cvc.trim()) {
            newErrors.cvc = "CVC is required";
        } else if (!/^\d{3,4}$/.test(cardDetails.cvc)) {
            newErrors.cvc = "CVC is invalid";
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    }

    const handleSubmit = async (e) => {
        e.preventDefault()
        if (validateForm()) {
            try {
                const [month, year] = cardDetails.expiry.split('/');
                
                const paymentRequest = {
                    cardName: cardDetails.name,
                    cardNumber: cardDetails.number.replace(/\s+/g, ''),
                    expirationMonth: parseInt(month),
                    expirationYear: parseInt(`20${year}`),
                    cvv: parseInt(cardDetails.cvc),
                    totalPrice: totalPrice
                };

                const response = await apiClient.post(endpoints.payment, paymentRequest);
                
                if(response.status === 200){
                    clearCart();
                    setShowSuccessModal(true);
                    setIsSuccess(true);
                }
            } catch (error) {
                setIsSuccess(false);
                setMessage(error);
            }
        } else {
            alertify.error('Please correct the errors in the form.');
        }
    }

    const SuccessModal = () => (
        <Modal isOpen={showSuccessModal} centered>
            <div style={styles.modalContent}>
                <FaCheckCircle size={50} color="#28a745" style={styles.checkIcon}/>
                <h2>Payment Successful!</h2>
                <p>Thank you for your purchase.</p>
                <p>You can now access your courses in the My Learning section.</p>
                <button 
                    className="btn-purple w-100" 
                    onClick={() => window.location.href = '/my-learning'}
                >
                    Go to My Learning
                </button>
            </div>
        </Modal>
    );

    return (
        <div className='container' style={{ marginTop: '100px' }}>
            <SuccessModal />
            <form onSubmit={handleSubmit}>
                <div className='row gx-5'>
                    <div className='col-md-6'>
                        <div key='Payment'>
                            <div className='App-payment'>
                                <h1>Enter your payment details</h1>
                                <h4>Please input your information below</h4>
                                <div>
                                    <label className="input-label">Name</label>
                                    <input
                                        type='text'
                                        name='name'
                                        placeholder='Name'
                                        pattern='[a-zA-Z\s-]+'
                                        className={`${errors.name ? "is-invalid" : ""}`}
                                        required
                                        onChange={handleInputChange}
                                        value={cardDetails.name}
                                    />
                                    {errors.name && <span className="invalid-feedback">{errors.name}</span>}
                                </div>
                                <div>
                                    <label className="input-label">Card Number</label>
                                    <input
                                        type='tel'
                                        name='number'
                                        placeholder='Card Number'
                                        pattern='[\d| ]{16,19}'
                                        className={`${errors.number ? "is-invalid" : ""}`}
                                        maxLength='19'
                                        required
                                        onChange={handleInputChange}
                                        value={cardDetails.number}
                                    />
                                    {errors.number && <span className="invalid-feedback">{errors.number}</span>}
                                </div>

                                <div>
                                    <label className="input-label">Expiration Date</label>

                                    <input
                                        type='tel'
                                        name='expiry'
                                        placeholder='MM/YY'
                                        pattern='\d\d/\d\d'
                                        className={`${errors.expiry ? "is-invalid" : ""}`}
                                        required
                                        onChange={handleInputChange}
                                        value={cardDetails.expiry}
                                    />
                                    {errors.expiry && <span className="invalid-feedback">{errors.expiry}</span>}
                                </div>
                                <div>
                                    <label className="input-label">CVC</label>
                                    <input
                                        type='tel'
                                        name='cvc'
                                        placeholder='CVC'
                                        pattern='\d{3,4}'
                                        className={`${errors.cvc ? "is-invalid" : ""}`}
                                        required
                                        onChange={handleInputChange}
                                        value={cardDetails.cvc}
                                    />
                                    {errors.cvc && <span className="invalid-feedback">{errors.cvc}</span>}
                                </div>
                            </div>
                            {message && <div className={`p-3 mt-3 ${isSuccess ? "bg-success" : "bg-danger"} text-white`}>{message}</div>}
                        </div>
                    </div>
                    <div className='col-md-6' style={{ marginLeft: '0px' }}>
                        <div className='summary' style={styles.summary}>
                            <h1>Summary</h1>
                            <hr />
                            <div style={styles.summaryHeader}>
                                <h2>Original Price:</h2>
                                <h2>£{totalPrice.toFixed(2)}</h2>
                            </div>
                            <p>With compilation of your courses, you will agree to the terms and conditions of the courses.</p>
                            <button type='submit' className='btn-purple w-100'><FaLock /> Complete Payment</button>
                            <div style={styles.summaryFooter}>
                                <h2>Guaranteed Refund</h2>
                                <p>If you are not satisfied with the course, you can get a refund within 30 days. No questions asked.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default Checkout;

const styles = {
    container: {
        marginTop: '100px',
        width: '80%'
    },
    summary: {
        padding: '20px 60px',
        borderRadius: '10px',
        backgroundColor: '#f0f0f0',
        width: '500px'
    },
    summaryHeader: {
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: '20px'
    },
    summaryFooter: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        textAlign: 'center',
        marginTop: '20px'
    },
    modalContent: {
        padding: '30px',
        textAlign: 'center',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        gap: '20px'
    },
    checkIcon: {
        marginBottom: '10px'
    }
}

