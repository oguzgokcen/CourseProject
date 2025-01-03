import React, { useEffect, useState } from 'react';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import CourseCard from '../components/CourseCard';
import Spinner from '../components/Spinner';

const MyLearning = () => {
    const [courses, setCourses] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        getMyCourses();
    }, []);

    const getMyCourses = async () => {
        try {
            const response = await apiClient.get(endpoints.myLearning);
            setCourses(response.data.data);
        } catch (error) {
            console.error(error);
        }
        setLoading(false);
    }

    if (loading) return <Spinner loading={loading} />;

    return (
        <>
            <div style={styles.page}>
                <div className="container">
                    <div style={styles.header}>
                        <h1>My Learning</h1>
                        <p>Continue learning where you left off</p>
                    </div>
                </div>
            </div>
            <div className="container" style={styles.coursesContainer}>
                <h2>My Courses</h2>
                <div style={styles.courseGrid}>
                    {courses.length === 0 ? (
                        <div>
                            <h3>You haven't purchased any courses yet.</h3>
                            <p>Browse our courses to start learning!</p>
                        </div>
                    ) : (
                        courses.map(course => (
                            <CourseCard key={course.id} course={course} />
                        ))
                    )}
                </div>
            </div>
        </>
    );
};

const styles = {
    page: {
        backgroundColor: '#1c1c1c',
        padding: '40px 0',
        color: '#fff',
        marginBottom: '40px'
    },
    header: {
        display: 'flex',
        flexDirection: 'column',
        gap: '10px'
    },
    coursesContainer: {
        marginTop: '20px',
        maxWidth: '1200px',
        margin: '20px auto',
        padding: '0 20px'
    },
    courseGrid: {
        display: 'flex',
        flexWrap: 'wrap',
        gap: '20px',
        marginTop: '20px',
        justifyContent: 'flex-start'
    }
};

export default MyLearning;