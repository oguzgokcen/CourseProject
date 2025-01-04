import React, { useState } from "react";
import { useAuth } from "../context/AuthContext";
import alertify from 'alertifyjs';
import { Button, Card, CardBody, CardTitle, Col, Container, Form, FormGroup, Input, Label, Row} from 'reactstrap';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';
import { useNavigate } from 'react-router-dom';

export default function SignUp() {
    const [userDetails, setUserDetails] = useState({
        fullName: "",
        email: "",
        password: "",
    });

    const [errors, setErrors] = useState({});
    const [message, setMessage] = useState("");
    const [isSuccess, setIsSuccess] = useState(false);

    const {login} = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (validateForm()) {
            register(userDetails);
        }
    }

    const register = async (userDetails) => {
        try {
            const response = await apiClient.post(endpoints.signup, userDetails);
            setMessage(response.data.data.message);
            setIsSuccess(true);
            login(response.data.data.token);
            alertify.success(response.data.data.message);

            setTimeout(() => {
                navigate("/");
            }, 2000);
        } catch (error) {
            setMessage(error);
            setIsSuccess(false);
            alertify.error(error);
        }
    }

    const handleChange = (e) => {
        setUserDetails({ ...userDetails, [e.target.name]: e.target.value });
    }

    const validateForm = () => {
        const errors = {};
        if (!userDetails.fullName) errors.fullName = "Full name is required";
        if (!userDetails.email) errors.email = "Email is required";
        if (!userDetails.password) errors.password = "Password is required";
        if (passwordValidation(userDetails.password)) errors.password = passwordValidation(userDetails.password);
        setErrors(errors);
        return Object.keys(errors).length === 0;
    }

    const passwordValidation = (password) => {
        if (password.length < 8) return "Password must be at least 8 characters long";
        if (!/[A-Z]/.test(password)) return "Password must contain at least one uppercase letter";
        if (!/[a-z]/.test(password)) return "Password must contain at least one lowercase letter";
        if (!/[0-9]/.test(password)) return "Password must contain at least one number";
        if (!/[!@#$%^&*]/.test(password)) return "Password must contain at least one special character";
        return null;
    }

    return (
        <Container className="d-flex justify-content-center align-items-center"
            style={{ minHeight: '100vh'}}>
            <Row className="w-50">
                <Col md="12">
                    <Card>
                        <CardBody>
                            <CardTitle tag="h3" className="text-center mb-4">Sign Up</CardTitle>
                            <Form onSubmit={handleSubmit}>
                                <FormGroup>
                                    <Label for="fullName">Full Name</Label>
                                    <Input
                                        type='text'
                                        name='fullName'
                                        id='fullName'
                                        maxLength="100"
                                        value={userDetails.fullName}
                                        onChange={handleChange}
                                        placeholder='Full Name'
                                        invalid={!!errors.fullName}
                                    />
                                    {errors.fullName && 
                                        <div className="invalid-feedback d-block">{errors.fullName}</div>
                                    }
                                </FormGroup>
                                <FormGroup>
                                    <Label for="email">Email</Label>
                                    <Input
                                        type='email'
                                        name='email'
                                        id='email'
                                        maxLength="100"
                                        value={userDetails.email}
                                        onChange={handleChange}
                                        placeholder='Email'
                                        invalid={!!errors.email}
                                    />
                                    {errors.email && 
                                        <div className="invalid-feedback d-block">{errors.email}</div>
                                    }
                                </FormGroup>
                                <FormGroup>
                                    <Label for="password">Password</Label>
                                    <Input
                                        type='password'
                                        name='password'
                                        id='password'
                                        value={userDetails.password}
                                        onChange={handleChange}
                                        placeholder='Password'
                                        invalid={!!errors.password}
                                    />
                                    {errors.password && 
                                        <div className="invalid-feedback d-block">{errors.password}</div>
                                    }
                                </FormGroup>
                                <Button color='primary' block type='submit'>Register</Button>
                            </Form>
                            {message && <div className={`p-3 mt-3 ${isSuccess ? "bg-success" : "bg-danger"} text-white`}>{message}</div>}
                        </CardBody>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}