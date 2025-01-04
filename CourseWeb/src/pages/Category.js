import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import CourseItem from '../components/CourseItem';
import Spinner from '../components/Spinner';
import RatingFilter from '../components/RatingFilter';
import alertify from 'alertifyjs';
import Pagination from '@mui/material/Pagination';

const Category = () => {
    const { keyword } = useParams();
    const [courses, setCourses] = useState([]);
    const [totalCourses, setTotalCourses] = useState(0);
    const [loading, setLoading] = useState(true);
    const [selectedRating, setSelectedRating] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const pageSize = 4;

    useEffect(() => {
        fetchCoursesByCategory();
    }, [keyword, selectedRating, currentPage]);

    const fetchCoursesByCategory = async () => {
        try {
            setLoading(true);
            let url = `${endpoints.category}?searchTerm=${keyword}&pageSize=${pageSize}&pageNumber=${currentPage}`;
            if (selectedRating) {
                url += `&minRating=${selectedRating}`;
            }
            const response = await apiClient.get(url);
            setCourses(response.data.data.data);
            setTotalCourses(response.data.data.totalCount);
        } catch (error) {
            alertify.error('Error fetching category courses:', error);
            setCourses([]);
        } finally {
            setLoading(false);
        }
    };

    const handleRatingChange = (rating) => {
        setSelectedRating(rating);
        setCurrentPage(1); 
    };

    const handlePageChange = (pageNumber) => {
        setCurrentPage(pageNumber);
        window.scrollTo(0, 0); 
    };

    const handleResetFilters = () => {
        setSelectedRating(null);
        setCurrentPage(1);
    };

    const totalPages = Math.ceil(totalCourses / pageSize);

    return (
        <>
            <div style={styles.page}>
                <div className="container">
                    <div style={styles.header}>
                        <h1>{keyword.replace(/-/g, ' ')}</h1>
                        <p>Explore our {keyword.replace(/-/g, ' ')} courses</p>
                    </div>
                </div>
            </div>
            <div className="container" style={styles.coursesContainer}>
                <div className="row">
                    <div className="col-md-3 col-sm-12">
                        <RatingFilter
                            selectedRating={selectedRating}
                            onRatingChange={handleRatingChange}
                            onResetFilters={handleResetFilters}
                        />
                    </div>
                    <div className="col-md-9 col-sm-12">
                        <div style={styles.resultHeader}>
                            <span style={styles.totalCount}>{totalCourses} result</span>
                        </div>
                        {loading ? (
                            <div style={styles.spinnerContainer}>
                                <Spinner loading={loading} />
                            </div>
                        ) : (
                            <>
                                <div style={styles.courseGrid}>
                                    {courses.map(course => (
                                        <CourseItem key={course.id} course={course} />
                                    ))}
                                </div>
                            </>
                        )}
                    </div>
                </div>
                <div style={styles.pagination}>
                    <Pagination 
                        count={totalPages}
                        page={currentPage}
                        onChange={(e, page) => handlePageChange(page)}
                        color="primary"
                    />
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
        flexDirection: 'column',
        gap: '20px',
        marginTop: '20px',
        width: '100%'
    },
    spinnerContainer: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        minHeight: '400px'
    },
    resultHeader: {
        display: 'flex',
        justifyContent: 'flex-end',
        marginBottom: '20px',
        paddingRight: '20px'
    },
    totalCount: {
        color: '#6a6f73',
        fontSize: '14px'
    },
    pagination: {
        display: 'flex',
        justifyContent: 'center',
        marginTop: '40px',
        marginBottom: '20px',
        gap: '5px'
    },
    paginationLink: {
        border: 'none',
        backgroundColor: 'transparent',
        color: '#1c1d1f',
        padding: '8px 12px',
        minWidth: '32px',
        textAlign: 'center',
        textDecoration: 'none',
        fontSize: '14px',
        fontWeight: 400,
        ':hover': {
            backgroundColor: '#f7f9fa'
        }
    },
    paginationArrow: {
        border: '1px solid #d1d7dc',
        borderRadius: '50%',
        padding: '8px 12px',
        backgroundColor: 'white',
        fontSize: '18px',
        lineHeight: '1',
        color: '#1c1d1f'
    }
};

export default Category;