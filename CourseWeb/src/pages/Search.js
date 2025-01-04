import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import CourseItem from '../components/CourseItem';
import Spinner from '../components/Spinner';
import RatingFilter from '../components/RatingFilter';
import LanguageDropdown from '../components/LanguageDropdown';
import Pagination from '@mui/material/Pagination';

const Search = () => {
    const [searchParams] = useSearchParams();
    const searchQuery = searchParams.get('q') || '';
    const [courses, setCourses] = useState([]);
    const [totalCourses, setTotalCourses] = useState(0);
    const [loading, setLoading] = useState(true);
    const [selectedRating, setSelectedRating] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const pageSize = 4;
    const [selectedLanguage, setSelectedLanguage] = useState(null);

    useEffect(() => {
        fetchSearchResults();
    }, [searchQuery, selectedRating, currentPage, selectedLanguage]);

    const fetchSearchResults = async () => {
        try {
            setLoading(true);
            let url = `${endpoints.courses}/search?keyword=${searchQuery}&pageSize=${pageSize}&pageNumber=${currentPage}`;
            if (selectedRating) {
                url += `&minRating=${selectedRating}`;
            }
            if (selectedLanguage) {
                url += `&language=${selectedLanguage}`;
            }
            const response = await apiClient.get(url);
            setCourses(response.data.data.data);
            setTotalCourses(response.data.data.totalCount);
        } catch (error) {
            setCourses([]);
            setTotalCourses(0);
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
        setSelectedLanguage(null);
        setCurrentPage(1);
    };

    const handleLanguageSelect = (language) => {
        setSelectedLanguage(language);
        setCurrentPage(1);
    };

    const totalPages = Math.ceil(totalCourses / pageSize);

    return (
        <>
            <div style={styles.page}>
                <div className="container">
                    <div style={styles.header}>
                        <h1>Search Results</h1>
                        {searchQuery && <p>{totalCourses} results for "{searchQuery}"</p>}
                    </div>
                </div>
            </div>
            <div className="container" style={styles.coursesContainer}>
                <div className="row">
                    <div className="col-md-3 col-sm-12">
                        <RatingFilter
                            selectedRating={selectedRating}
                            onRatingChange={handleRatingChange}
                        />
                        <LanguageDropdown
                            selectedLanguage={selectedLanguage}
                            onLanguageSelect={handleLanguageSelect}
                        />
                        <button 
                            onClick={handleResetFilters}
                            className='btn-purple p-3 py-2'
                        >
                            Reset filters
                        </button>
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
                                {courses.length === 0 ? (
                                    <div style={styles.noResults}>
                                        <h3>No courses found</h3>
                                        <p>Try different keywords or browse our course catalog</p>
                                    </div>
                                ) : (
                                    <div style={styles.courseGrid}>
                                        {courses.map(course => (
                                            <CourseItem key={course.id} course={course} />
                                        ))}
                                    </div>
                                )}
                            </>
                        )}
                    </div>
                </div>
                {courses.length > 0 && (
                    <div style={styles.pagination}>
                        <Pagination 
                            count={totalPages}
                            page={currentPage}
                            onChange={(e, page) => handlePageChange(page)}
                            color="primary"
                        />
                    </div>
                )}
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
    noResults: {
        textAlign: 'center',
        padding: '40px 20px',
        color: '#1c1d1f',
        '& h3': {
            marginBottom: '16px',
            fontSize: '24px',
            fontWeight: 600
        },
        '& p': {
            color: '#6a6f73',
            marginBottom: '8px'
        }
    },
    filterHeader: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginTop: '40px'
    },
    title: {
        fontSize: '18px',
        fontWeight: 600,
        margin: 0
    },
};

export default Search;

