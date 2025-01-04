import React from "react";
import Rating from '@mui/material/Rating';

const CourseItem = ({ course }) => {
    return (
        <div style={styles.courseCard}>
            <img src={course.imageUrl ? course.imageUrl : "/DefaultCourseImg.png"} alt="Product" style={styles.cardImage} />
            <div style={styles.cardContent}>
                <h3 style={styles.cardTitle}>{course.title}</h3>
                <p>{course.descriptionHeader}</p>
                <p>Instructor: {course.instructor.fullName}</p>
                <div style={styles.ratingContainer}>
                    <Rating 
                        value={course.rating} 
                        precision={0.5} 
                        readOnly 
                        size="small"
                    />
                    <span style={{ color: '#b4690e' }}>{course.rating}</span>
                    <span style={{ color: '#6a6f73' }}>({course.numberOfRatings} rated)</span>
                </div>
                <p>{course.totalHours} total hours • {course.totalLessons} lessons • All Levels</p>
            </div>
            <div style={styles.cardActions}>
                <a href={`/course/${course.id}`} style={styles.actionLink}>Go To Course Details</a>
                <p style={styles.price} className='mt-2'>£{course.price}</p>
            </div>
        </div>
    );
};

const styles = {
    cardImage: {
        width: 'auto',
        height: 'auto',
        maxWidth: '240px',
        maxHeight: '240px',
        marginRight: '10px',
        objectFit: 'cover'
    },
    courseCard: {
        display: 'flex',
        flexDirection: 'row',
        flexWrap: 'nowrap',
        alignItems: 'flex-start',
        marginTop: '30px',
        maxHeight: '200px',
        gap: '20px',
        width: '100%',
    },
    cardContent: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'flex-start',
        alignItems: 'flex-start',
        maxHeight: '200px',
        overflow: 'hidden',
        textOverflow: 'ellipsis',
        whiteSpace: 'nowrap',
    },
    cardTitle: {
        fontSize: '16px',
        fontWeight: 'bold',
        width: '100%',
    },
    cardActions: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'flex-end',
        alignItems: 'flex-end',
        marginTop: '10px',
        flexWrap: 'nowrap',
        textAlign: 'right',
        marginLeft: 'auto',
        minWidth: 'fit-content'
    },
    actionLink: {
        marginRight: '10px',
        color: '#6c63ff',
        textDecoration: 'none',
        whiteSpace: 'nowrap',
        fontWeight: 'bold'
    },
    price: {
        fontWeight: 'bold',
        minWidth: 'fit-content'
    },
    ratingContainer: {
        display: 'flex',
        alignItems: 'center',
        gap: '8px'
    }
};

export default CourseItem;