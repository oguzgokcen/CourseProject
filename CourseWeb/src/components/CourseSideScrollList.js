import React from "react";
import CourseCard from "./CourseCard";
const CourseSideScrollList = ({ courses }) => {
    return (
        <div className="container w-100">
            <h1>Recommended for you</h1>
            <div style={styles.courseContainer}>
                {courses.map((course) => (
                    <CourseCard key={course.id} course={course} />
                ))}
            </div>
        </div>
    );
}


const styles = {
    courseContainer: {
        display: "flex",
        flexDirection: "row",
        gap: "10px",
    },
};

export default CourseSideScrollList;