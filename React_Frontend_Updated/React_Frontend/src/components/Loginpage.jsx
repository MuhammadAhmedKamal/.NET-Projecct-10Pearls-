import React, {useState} from 'react'
import './Loginpage.css'
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import Alert from 'react-bootstrap/Alert';



export default function Loginpage() {

    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');

    const handleLogin = async (e) => {
        e.preventDefault();
    
        try {
            const response = await axios.get('https://localhost:7224/api/Registration/login', {
                params: {
                email: email,
                password: password
            }
            });

            if (response.status === 200) 
                {
                    setMessage("Login Successful!");
                    // Redirect or perform other actions on successful login
                    navigate("/homepage");
                }
            } 
         catch (error) 
            {
            if (error.response && error.response.status === 401) 
                {
                    console.log("Response Error:", error.response); 
                    setMessage("Invalid email or password");
                } 
            else 
                {
                    setMessage("An error occurred. Please try again later.");
                    navigate("/loginpage");
                }
            }
        };





  return (
    <>
        <div className="namecontainer">
            <h1 className='pagename'>Login Page</h1>
        </div>

        <div className="textdata">Welcome back! Please log in to access your personalized dashboard and manage your tasks effortlessly. Secure and easy access to all your tools and features awaits you.</div>


        <div className="formcontainer">
            <Form onSubmit={handleLogin}>

                <Form.Group className="mb-3" controlId="loginEmail">
                    <Form.Label>Email Address</Form.Label>
                    <Form.Control type="email" placeholder="Enter Email Address" 
                    value={email}
                    onChange={(e) => setEmail(e.target.value)} />
                    <Form.Text className="text-mutedd">
                        We'll never share your email with anyone else.
                    </Form.Text>
                </Form.Group>

                <Form.Group className="mb-3" controlId="loginPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" 
                    value={password}
                    onChange={(e) => setPassword(e.target.value)} />
                </Form.Group>

                <Form.Group className="mb-3" controlId="loginCheckbox">
                    <Form.Check type="checkbox" label="Remember me" />
                </Form.Group>


                <Button className='button' variant="primary" type="submit">
                    Login
                </Button>

                <div className="newaccount">
                    <a href="./signuppage">Create new account</a>
                </div>

            </Form>
            <div className="errormessage">
                <p>{message}</p> 
            </div>
    </div>
    </>
  )
}
