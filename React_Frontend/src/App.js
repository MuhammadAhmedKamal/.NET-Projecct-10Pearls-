import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Signuppage from './components/Signuppage';
import Loginpage from './components/Loginpage';
import Homepage from './components/Homepage';
import Landingpage from './components/Landingpage';

function App() {
  return (
    <>

<Router>
      <Routes>

        {/* Route path for LandingPage */}
        <Route path='/' element={<Landingpage />}/>


        {/* Route path for SignupPage */}
        <Route path="/signuppage" element={<Signuppage />} />


        {/* Route for LoginPage */}
        <Route path="/loginpage" element={<Loginpage />} />


        {/* Route path for HomePage */}
        <Route path='/homepage' element={<Homepage />}/>
      </Routes>
    </Router>
    </>
  );
}

export default App;
