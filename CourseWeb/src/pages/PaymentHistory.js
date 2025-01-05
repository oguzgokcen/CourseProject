import React, { useState, useEffect } from 'react';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import Spinner from '../components/Spinner';
import alertify from 'alertifyjs';

const PaymentHistory = () => {
    const [payments, setPayments] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchPaymentHistory();
    }, []);

    const fetchPaymentHistory = async () => {
        try {
            setLoading(true);
            const response = await apiClient.get(`${endpoints.paymentHistory}`);
            setPayments(response.data.data);
        } catch (error) {
            alertify.error('Error fetching payment history');
        } finally {
            setLoading(false);
        }
    };

    const formatDate = (dateString) => {
        return new Date(dateString).toLocaleDateString('en-GB', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    if (loading) return <Spinner loading={loading} />;

    return (
        <div className="container" style={styles.container}>
            <h2 style={styles.title}>Payment History</h2>
            {payments.length === 0 ? (
                <div style={styles.noPayments}>
                    <p>No payment history found.</p>
                </div>
            ) : (
                <div style={styles.paymentList}>
                    {payments.map((payment) => (
                        <div key={payment.id} style={styles.paymentCard}>
                            <div style={styles.paymentHeader}>
                                <div>
                                    <span style={styles.date}>
                                        {formatDate(payment.createdOnUtc)}
                                    </span>
                                    <span style={styles.status}>
                                        Status: {payment.paymentStatus === 1 ? 'Completed' : 'Pending'}
                                    </span>
                                </div>
                                <span style={styles.price}>
                                    £{payment.totalPrice.toFixed(2)}
                                </span>
                            </div>
                            <div style={styles.courseList}>
                                {payment.boughtCourses.map((item) => (
                                    <div key={item.course.id} style={styles.courseItem}>
                                        <img 
                                            src={item.course.imageUrl || "/DefaultCourseImg.png"} 
                                            alt={item.course.title}
                                            style={styles.courseImage}
                                        />
                                        <div style={styles.courseInfo}>
                                            <h4 style={styles.courseTitle}>{item.course.title}</h4>
                                            <p style={styles.instructor}>
                                                Instructor: {item.course.instructor.fullName}
                                            </p>
                                            <p style={styles.purchaseDate}>
                                                Purchased: {formatDate(item.boughtDate)}
                                            </p>
                                        </div>
                                    </div>
                                ))}
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

const styles = {
    container: {
        padding: '40px 20px',
        maxWidth: '1000px',
        margin: '0 auto'
    },
    title: {
        marginBottom: '30px',
        fontSize: '24px',
        fontWeight: '600'
    },
    paymentList: {
        display: 'flex',
        flexDirection: 'column',
        gap: '20px'
    },
    paymentCard: {
        border: '1px solid #d1d7dc',
        borderRadius: '8px',
        padding: '20px',
        backgroundColor: '#fff'
    },
    paymentHeader: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: '15px',
        paddingBottom: '15px',
        borderBottom: '1px solid #d1d7dc'
    },
    date: {
        color: '#1c1d1f',
        marginRight: '15px'
    },
    status: {
        color: '#3c3c3c',
        backgroundColor: '#eceb98',
        padding: '4px 8px',
        borderRadius: '4px',
        fontSize: '14px'
    },
    price: {
        fontSize: '18px',
        fontWeight: '700',
        color: '#1c1d1f'
    },
    courseList: {
        display: 'flex',
        flexDirection: 'column',
        gap: '15px'
    },
    courseItem: {
        display: 'flex',
        gap: '15px',
        padding: '10px',
        backgroundColor: '#f7f9fa',
        borderRadius: '4px'
    },
    courseImage: {
        width: '120px',
        height: '68px',
        objectFit: 'cover',
        borderRadius: '4px'
    },
    courseInfo: {
        flex: 1
    },
    courseTitle: {
        fontSize: '16px',
        fontWeight: '600',
        marginBottom: '5px',
        color: '#1c1d1f'
    },
    instructor: {
        fontSize: '14px',
        color: '#6a6f73',
        marginBottom: '5px'
    },
    purchaseDate: {
        fontSize: '14px',
        color: '#6a6f73'
    },
    noPayments: {
        textAlign: 'center',
        padding: '40px',
        color: '#6a6f73'
    }
};

export default PaymentHistory;