import React from 'react'
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import { useAuth } from '../context/AuthContext';
import { useState, useEffect } from 'react';
import CourseCard from '../components/CourseCard';
import Spinner from '../components/Spinner';

function TeacherPage() {
    const {user} = useAuth();
    const [courses, setCourses] = useState([]);
    const [loading, setLoading] = useState(true);
    const [isSuccess, setIsSuccess] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await apiClient.get(endpoints.teacherCourse);
                setCourses(response.data.data);
                setIsSuccess(response.data.isSuccess);
            } catch (error) {
                setError(error);
            }
            setLoading(false);
        }
        fetchCourses();
    }, []);
    if (loading) return <Spinner loading={loading} />;
    if (error) return <div>Error: {error.message}</div>;

    return (
        <>
            <div style={styles.page}>
                <div className="container">
                    <div style={styles.header}>
                        <h1>My Courses</h1>
                        <p>Manage your courses here</p>
                    </div>
                </div>
            </div>
            <div className="container" style={styles.coursesContainer}>
                <h2>My Courses</h2>
                <div style={styles.courseGrid}>
                    {courses.length === 0 ? (
                        <div>
                            <h3>You haven't created any courses yet.</h3>
                            <p>Create a course to start teaching!</p>
                        </div>
                    ) : (
                        courses.map(course => (
                            <CourseCard key={course.id} course={course} />
                        ))
                    )}
                </div>
            </div>
            {error && <div className={`p-3 mt-3 ${isSuccess ? "bg-success" : "bg-danger"} text-white`}>{error}</div>}
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

export default TeacherPage