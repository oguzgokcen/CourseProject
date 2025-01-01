import React, { useState , useEffect} from "react";
import "../css/inputs.css";
import apiClient from "../api/apiClient";
import { endpoints } from "../api/endpoints";
import alertify from "alertifyjs";

const Profile = () => {
    const [errors, setErrors] = useState({});
    const [message, setMessage] = useState("");
    const [isSuccess, setIsSuccess] = useState(false);

    const [userDetails, setUserDetails] = useState({
        fullName: "",
        email: "",
        title: "",
        website: "",
        description: "",
    });
    useEffect(() => {
        const fetchUserDetails = async () => {
            try {
                const response = await apiClient.get(endpoints.userDetails);
                var data = response.data.data;
                setUserDetails({
                    fullName: data.fullName || "",
                    email: data.email || "",
                    title: data.title || "",
                    website: data.website || "",
                    description: data.description || "",
                });
            } catch (error) {
                console.error("Error fetching user details:", error);
            }
        };
        fetchUserDetails();
    }, []);

    const handleSubmit =async (e) => {
        e.preventDefault();
        if (validateForm()) {
            try {
                const response = await apiClient.put(endpoints.userDetails, userDetails);
                setMessage("Your details updated successfully");
                setIsSuccess(true);
                alertify.success(response.data.data);
                setErrors({});
            } catch (error) {
                setMessage(error);
                setIsSuccess(false);
                alertify.error(error);
            }
        }
    }

    const validateForm = () => {
        const newErrors = {};
        if (!userDetails.fullName) {
            newErrors.fullName = "Full name is required";
        }
        if (!userDetails.email) {
            newErrors.email = "Email is required";
        }
        if (!userDetails.title) {
            newErrors.title = "Title is required";
        }
        if (!userDetails.description) {
            newErrors.description = "Description is required";
        }
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    }

    const handleInputChange = (e) => {
        const { name, value } = e.target;

        setUserDetails({ ...userDetails, [name]: value });
    }


    return (
        <div className="container" style={styles.container}>
            <form onSubmit={handleSubmit}>
                <h1>Profile and settings</h1>
                <hr />
                <div className="row">
                    <div className="col-md-6">
                        <label class="input-label">Name</label>
                        <input type="text"
                            maxlength="100"
                            name="fullName"
                            placeholder="Enter your full name here"
                            value={userDetails.fullName}
                            className={`${errors.fullName ? "is-invalid" : ""}`}
                            onChange={handleInputChange}
                        />
                        {errors.fullName && <div className='invalid-feedback'>{errors.fullName}</div>}
                    </div>
                    <div className="col-md-6">
                        <label class="input-label">Email</label>
                        <input type="text"
                            maxlength="100"
                            name="email"
                            placeholder="Enter your email here"
                            value={userDetails.email}
                            className={`${errors.email ? "is-invalid" : ""}`}
                            onChange={handleInputChange}
                        />
                        {errors.email && <div className='invalid-feedback'>{errors.email}</div>}
                    </div>
                    <div className="col-md-6">
                        <label class="input-label">Title</label>
                        <input type="text"
                            maxlength="50"
                            name="title"
                            placeholder="Instructor On Udemy"
                            value={userDetails.title}
                            className={`${errors.title ? "is-invalid" : ""}`}
                            onChange={handleInputChange}
                        />
                        {errors.title && <div className='invalid-feedback'>{errors.title}</div>}
                    </div>
                    <div className="col-md-6">
                        <label class="input-label">Website</label>
                        <div class="input-with-prefix input-with-suffix ">
                            <span>www.</span>
                            <input type="text"
                                maxlength="50"
                                name="website"
                                placeholder="domain name"
                                value={userDetails.website}
                                className={`${errors.website ? "is-invalid" : ""}`}
                                onChange={handleInputChange}
                            />
                            <span>.com</span>
                        </div>
                    </div>
                    <div className="col-md-12">
                        <label class="input-label">Description</label>
                        <textarea maxlength="200"
                            name="description"
                            placeholder="Enter your description here"
                            value={userDetails.description}
                            className={`${errors.description ? "is-invalid" : ""}`}
                            onChange={handleInputChange}
                        ></textarea>
                        {errors.description && <div className='invalid-feedback'>{errors.description}</div>}
                    </div>
                    <div className="col-md-12 mt-3">
                        <button type="submit" class="btn btn-dark px-4 py-2">Save</button>
                    </div>
                </div>
                {message && <div className={`p-3 mt-3 ${isSuccess ? "bg-success" : "bg-danger"} text-white`}>{message}</div>}
            </form>
        </div>
    );
}

const styles = {
    container: {
        marginTop: '100px',
    }
}

//todo user save

export default Profile;