import React from "react";

const CourseCard = ({course}) => {
    return(
        <Card>
            <CardBody>
                <CardTitle>{course.name}</CardTitle>
            </CardBody>
        </Card>
    );
}
