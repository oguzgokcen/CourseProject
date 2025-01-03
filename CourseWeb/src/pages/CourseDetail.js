import React, { useEffect, useState, useContext } from "react";
import { useParams } from "react-router-dom";
import { endpoints } from "../api/endpoints";
import apiClient from "../api/apiClient";
import Spinner from "../components/Spinner";
import CategoryKeywords from "../components/CategoryKeywords";
import { Card, CardBody, CardImg, CardText } from "reactstrap";
import { FaStar } from 'react-icons/fa';
import { TbWorld } from 'react-icons/tb';
import alertify from "alertifyjs";
import NotFound from "./NotFound";
import { useAuth } from "../context/AuthContext";
import { useCart } from "../context/CartContext";

const CourseStatus = {
  BOUGHT: 1,
  IN_CART: 2,
  NONE: null
};

const CourseDetail = () => {
    const { courseId } = useParams();
    const { handleAddToCart, checkCourseInCartOrBought } = useCart();

    const [course, setCourse] = useState(null);
    const [loading, setLoading] = useState(true);
    const [notFound, setNotFound] = useState(false);
    const [isBought, setIsBought] = useState(null);
    const [checkBoughtLoading, setCheckBoughtLoading] = useState(false);
    const [boughtDate, setBoughtDate] = useState(null);
    const [isOnCart, setIsOnCart] = useState(false);
    const { user } = useAuth();

    useEffect(() => {
        getCourseDetail(courseId);
        checkStatus();
    }, [courseId]);

    const getCourseDetail = async (courseId) => {
        try {
            const response = await apiClient.get(`${endpoints.courseDetail}/${courseId}`);
            setCourse(response.data.data);
        } catch (error) {
            setNotFound(true);
        }
        setLoading(false);
    }

    const checkStatus = async () => {
        if(user){
            try {
                setCheckBoughtLoading(true);
                const status = await checkCourseInCartOrBought(courseId);
                setIsBought(status.isBought);
                if(status.isBought){
                    setBoughtDate(new Date(status.boughtTime));
                }else if(status.isOnCart){
                    setIsOnCart(true);
                }
            } catch (error) {
                alertify.error(error);
            }
            setCheckBoughtLoading(false);
        }
    }

    const handleAddBasket = async () => {
        await handleAddToCart(courseId);
        setIsOnCart(true);
    }

    const getCourseStatus = () => {
        if (isBought) {
            return CourseStatus.BOUGHT;
        } else if (isOnCart) {
            return CourseStatus.IN_CART;
        }
        return CourseStatus.NONE;
    };

    const getButtonText = () => {
        switch (getCourseStatus()) {
            case CourseStatus.BOUGHT:
                return "Already Bought";
            case CourseStatus.IN_CART:
                return "In Cart";
            case CourseStatus.NONE:
                return "Add to Basket";
            default:
                return "Add to Basket";
        }
    };

    if (loading) return <Spinner loading={loading} />;
    if (notFound) return <NotFound />;
    return (
        <>
            <div style={styles.page}>
                <div className="row">
                    <div className="col-7">
                        <div style={styles.courseDetailHeader}>
                            <h1>{course.title}</h1>
                            <h2>{course.descriptionHeader}</h2>
                            <div style={styles.courseInfo}>
                                <p>
                                    <strong>{course.rating}</strong> <FaStar color="gold" /> ({course.numberOfRatings} ratings) {course.numberOfStudents.toLocaleString()} students
                                </p>
                                <p>Created by <a href="#">{course.instructor.fullName}</a></p>
                                <p>
                                    <span>Last updated: {new Date(course.publishedDate).toLocaleDateString('en-US', { month: '2-digit', year: 'numeric' })}</span>
                                    <span style={styles.language}><TbWorld/> English</span>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div className="col-5" >
                        <Card style={styles.sideCard}>
                            <CardImg src={course.imageUrl?course.imageUrl:"/DefaultCourseImg.png"} alt={course.name} style={styles.courseImage} />
                            <CardBody>
                                {checkBoughtLoading ? <Spinner loading={checkBoughtLoading} style={{marginTop:"10px",marginBottom:"auto"}} /> : (
                                    <CardText>
                                        <strong style={styles.price}>£{course.price.toFixed(2)}</strong>
                                    </CardText>
                                )}
                                <div style={styles.buttonContainer}>
                                    <button 
                                        onClick={getCourseStatus() === CourseStatus.NONE ? handleAddBasket : undefined} 
                                        className={`add-basket-button ${getCourseStatus() !== CourseStatus.NONE ? "disabled" : ""} w-100`}
                                    >
                                        {getButtonText()}
                                    </button>
                                    {getCourseStatus() === CourseStatus.BOUGHT && 
                                        <p>Bought on<strong> {boughtDate.toLocaleString('en-GB', {day: '2-digit', month: '2-digit', year: 'numeric' })}</strong></p>
                                    }
                                </div>
                            </CardBody>
                        </Card>
                    </div>
                </div>
            </div>
            <div className="container" style={styles.courseDetailBody}>
                <h2>İlgili konular</h2>
                <CategoryKeywords categories={course.categories} /> 

                <h2>Açıklama</h2>
                <p>{course.description}</p>

                <h2>Teacher</h2>
                <p>{course.instructor.fullName}</p>

                <h2>Teacher's Other Courses</h2>
                <div style={styles.teacherCourses}>
                    todo
                </div>
            </div>
        </>
    );
}

const styles = {
    page: {
        backgroundColor: '#1c1c1c',
        padding: '20px',
        color: '#fff',
        position: 'relative',
    },
    courseDetailHeader: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'flex-start',
        gap: '10px',
        padding: '20px',
        overflow: 'hidden',
        whiteSpace: 'normal', // Allows text to wrap normally
        wordBreak: 'break-word', // Breaks long words to fit within the container
    },
    sideCard: {
        width: '348px',
        display: 'flex',
        flexDirection: 'column',
        gap: '10px',
        position: 'absolute',
        top: '0',
        right: '0',
        height: '400px',
        borderRadius: '10px',
        color: '#000',
        margin: '20px',
        backgroundColor: '#fff',
        boxShadow: '0 0 10px 0 rgba(0, 0, 0, 0.1)',
        marginRight: '80px',
        height: 'auto',
    },
    courseImage: {
        borderRadius: '10px 10px 0 0',
    },
    courseCardBody: {
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'flex-start',
        justifyContent: 'flex-start',
        padding: '10px 20px',
    },
    price: {
        fontSize: '2rem',
        fontWeight: 'bold',
        color: '#333',
    },
    courseInfo: {
        color: '#ccc',
        fontSize: '0.9rem',
        lineHeight: '1.5',
    },
    language: {
        marginLeft: '10px',
    },
    courseDetailBody: {
        marginTop: '40px',
        marginRight:'100px',
        display: 'flex',
        flexDirection: 'column',
        gap: '20px',
    },
    buttonContainer: {
        display: 'flex',
        flexDirection: 'column',
        gap: '10px',
    },
};

export default CourseDetail;