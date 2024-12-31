import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { endpoints } from "../api/endpoints";
import apiClient from "../api/apiClient";
import Spinner from "../components/Spinner";
import CategoryKeywords from "../components/CategoryKeywords";
import { Card, CardBody, CardImg, CardText } from "reactstrap";
import { FaStar } from 'react-icons/fa';
import { TbWorld } from 'react-icons/tb';

const CourseDetail = () => {
    const { courseId } = useParams();

    const [course, setCourse] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getCourseDetail(courseId);
    }, [courseId]);

    const getCourseDetail = async (courseId) => {
        try {
            const response = await apiClient.get(`${endpoints.courseDetail}/${courseId}`);
            setCourse(response.data.data);
        } catch (error) {
            console.error("Error fetching course detail:", error);
        }
        setLoading(false);
    }

    if (loading) return <Spinner loading={loading} />;

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
                            <CardImg src={course.imageUrl} alt={course.name} style={styles.courseImage} />
                            <CardBody>
                                <CardText>
                                    <strong style={styles.price}>£{course.price.toFixed(2)}</strong>
                                </CardText>
                                <div style={styles.buttonContainer}>
                                    <a href={`/course/${course.id}`} className="add-basket-button">Sepete Ekle</a>
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
        height: '350px',
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
    buttonContainer: {
        marginTop: 'auto',
        marginBottom: '50px',
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
    }
};

export default CourseDetail;