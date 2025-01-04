import {React, useState, useEffect} from 'react';
import Carousel from '../components/Carousel';
import { endpoints } from '../api/endpoints';
import apiClient from '../api/apiClient';
import CourseSideScrollList from '../components/CourseSideScrollList';



const HomePage = () => {

    const [courses, setCourses] = useState([]);

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const response = await apiClient.get(`${endpoints.courses}/search?pageNumber=1&pageSize=3`);
                setCourses(response.data.data.data);
            } catch (error) {
                console.error("Error fetching courses:", error);
            }
        };
        fetchCourses();
    }, []);

    return (
        <div>
            <Carousel />
            <CourseSideScrollList courses={courses} />
        </div>
    );
}

export default HomePage;


