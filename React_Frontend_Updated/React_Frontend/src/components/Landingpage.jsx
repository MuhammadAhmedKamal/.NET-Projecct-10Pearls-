import React from 'react';
import { useNavigate } from 'react-router-dom';
import './Landingpage.css';

function Landingpage() {
  const navigate = useNavigate();

  // Handler functions for redirection
  const handleAdminLogin = () => {
    navigate('/admin'); // Update this path as per your routing setup
  };

  const handleUserLogin = () => {
    navigate('/loginpage');
  };

  return (
    <>
      <div className='heading'>Task Management System</div>
      <div className='webinfo'>
      Welcome to Task Managment System! <br /> We are dedicated to providing  an engaging platform where users can seamlessly register, log in, and manage their tasks efficiently. Whether you're an admin overseeing operations or a user tracking progress, our website ensures a user-friendly experience with secure access and personalized features. Dive in to explore the powerful tools and intuitive design that make managing your tasks simpler and more productive. Join us today to streamline your workflow!
      <br /><br />
      </div>
      <br />
      <br />
      <div className="landingpagebuttons">
        <button className='userbutton' onClick={handleUserLogin}>Login</button>
      </div>
    </>
  );
}

export default Landingpage;
