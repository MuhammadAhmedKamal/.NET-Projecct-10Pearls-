import React, {useState} from 'react'
import './Signuppage.css';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

export default function Signuppage() {

    const navigate = useNavigate();
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [message, setMessage] = useState('');

  const handleSignup = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('https://localhost:7224/api/Registration/registration', {
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: password,
        isActive: true // Defaulting to true for active users; adjust as needed
      });

      if (response.status === 200) {
        setMessage("Signup Successful! You can now log in.");
        navigate("/homepage");
      }
    } catch (error) {
        console.log("Error Response:", error.response);
        // console.log("No response was received:", error.request);
      setMessage("An error occurred. Please try again.");
    }
  };


  return (
    <>
        <div className="namecontainer">
            <h1 className='pagename'>Signup Page</h1>
        </div>

        <div className="textdata">Create your account and join Task Managment System today! Signing up is quick and easy—get started to unlock access to seamless task management and personalized features tailored just for you.</div>


        <div className="formcontainer">
            <Form onSubmit={handleSignup}>
                <Form.Group className="mb-3" controlId="formBasicFirstName">
                    <Form.Label>First Name</Form.Label>
                    <Form.Control type="text" placeholder="Enter first name" 
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}/>
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicLastName">
                    <Form.Label>Last Name</Form.Label>
                    <Form.Control type="text" placeholder="Enter last name" 
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}/>
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Email Address</Form.Label>
                    <Form.Control type="email" placeholder="Enter Email Address" 
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}/>
                    <Form.Text className="text-mutedd">
                        We'll never share your email with anyone else.
                    </Form.Text>
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" 
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}/>
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicCheckbox">
                    <Form.Check type="checkbox" label="Remember me" />
                </Form.Group>


                <Button className='button' variant="primary" type="submit">
                    SignUp
                </Button>
                
                <div className="alreadyhaveaccount" >
                    <a href="./loginpage">Already have an account</a>
                </div>

            </Form>
            <div className="errormessage">
                <p>{message}</p> 
            </div>
    </div>
    </>
  )
}
