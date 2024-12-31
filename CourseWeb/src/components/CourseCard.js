import React from "react";
import { Card, CardBody, CardTitle, CardText, CardSubtitle, CardImg } from "reactstrap";
import { FaStar } from "react-icons/fa";

const CourseCard = ({ course }) => {
    const imageUrl = course.imageUrl || "/DefaultCourseImg2.jpg";

    return (
        <Card style={styles.card}>
            <CardImg top width="100%" src={imageUrl} alt="Course image" />
            <CardBody className="d-flex flex-column">
                <CardTitle tag="h5">{course.title}</CardTitle>
                <CardSubtitle tag="h6" className="mb-2 text-muted">{course.instructor.fullName}</CardSubtitle>
                <CardText>
                    <strong>{course.rating}</strong> <FaStar color="gold" /> ({course.numberOfRatings.toLocaleString()})
                </CardText>
                <CardText className="text-success">
                    <strong>£{course.price.toFixed(2)}</strong>
                </CardText>
                <div style={styles.buttonContainer}>
                    <a href={`/course/${course.id}`} className="btn btn-info" style={styles.button}>Detaya git</a>
                </div>
            </CardBody>
        </Card>
    );
}

const styles = {
    buttonContainer: {
        marginTop: 'auto',
        display: 'flex',
        justifyContent: 'start',
        alignItems: 'flex-start',
    },
    button: {
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        padding: '8px 16px',
    },
    card: {
        display: 'flex',
        flexDirection: 'column',
        width: '20rem',
        margin: '1rem',
        alignItems: 'flex-start',
    }
}
export default CourseCard;