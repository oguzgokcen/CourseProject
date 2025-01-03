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
                    alertify.success("Giriş başarılı!");
                    navigate("/");
                }
            })
            .catch((error) => {
                alertify.error("Giriş başarısız! " + error);
            });
    }

    return(
        <Container className="d-flex justify-content-center align-items-center"
        style={{minHeight: '100vh'}}>

            <Row>
                <Col md="12">
                    <Card style={{maxWidth: '400px', margin: '0 auto'}}>
                        <CardBody>
                            <CardTitle tag="h3" className="text-center mb-4">Giriş Yap</CardTitle>
                            <Form>
                                <FormGroup>
                                    <Label for="email">Kullanıcı Adı</Label>
                                    <Input
                                    type='text'
                                    name='email'
                                    id='email'
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    placeholder='Email'
                                    />
                                </FormGroup>
                                <FormGroup>
                                    <Label for="password">Şifre</Label>
                                    <Input
                                    type='text'
                                    name='password'
                                    id='password'
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    placeholder='Şifre'
                                    />
                                </FormGroup>
                                <Button color='primary' block onClick={handleLogin}>Giriş Yap</Button>
                            </Form>
                        </CardBody>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}