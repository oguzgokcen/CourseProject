import {React , useState} from "react";
import { useAuth } from "../context/AuthContext";
import alertify from 'alertifyjs';
import { Button, Card, CardBody, CardTitle, Col, Container, Form, FormGroup, Input, Label, Row} from 'reactstrap';
import { useNavigate } from 'react-router-dom';
import apiClient from '../api/apiClient';
import { endpoints } from '../api/endpoints';


export default function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { login } = useAuth();

    const navigate = useNavigate();

    const handleLogin = () => {
        apiClient.post(endpoints.login, { email, password })
            .then((response) => {
                if (response.data.data) {
                    login(response.data.data);
                    alertify.success("Login successful!");
                    navigate("/");
                }
            })
            .catch((error) => {
                alertify.error("Login failed! " + error);
            });
    }

    return(
        <Container className="d-flex justify-content-center align-items-center"
        style={{minHeight: '100vh'}}>

            <Row>
                <Col md="12">
                    <Card style={{maxWidth: '400px', margin: '0 auto'}}>
                        <CardBody>
                            <CardTitle tag="h3" className="text-center mb-4">Login</CardTitle>
                            <Form>
                                <FormGroup>
                                    <Label for="email">Email</Label>
                                    <Input
                                    type='email'
                                    name='email'
                                    id='email'
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    placeholder='Email'
                                    />
                                </FormGroup>
                                <FormGroup>
                                    <Label for="password">Password</Label>
                                    <Input
                                    type='password'
                                    name='password'
                                    id='password'
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    placeholder='Password'
                                    />
                                </FormGroup>
                                <Button color='primary' block onClick={handleLogin}>Login</Button>
                            </Form>
                        </CardBody>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}