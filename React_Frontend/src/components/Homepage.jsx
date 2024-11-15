import React, { useState, useEffect } from 'react';
import './Homepage.css';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import axios from 'axios';

function Homepage() {
  const [data, setData] = useState([]);
  const [show, setShow] = useState(false);
  const [task, setTask] = useState('');
  const [description, setDescription] = useState('');
  const [taskStatus, setTaskStatus] = useState('Pending');
  const [editId, setEditId] = useState(null);
  const [editTask, setEditTask] = useState('');
  const [editDescription, setEditDescription] = useState('');
  const [editTaskStatus, setEditTaskStatus] = useState('Pending');
  const [role, setRole] = useState('admin'); // role for user and admin

  useEffect(() => {
    axios
  .get('https://localhost:7224/api/Registration/getRole?email=${email}')
  .then((response) => {
    const userRole = response.data.role;
    if (userRole) {
      console.log(userRole)
      setRole(userRole);
    } else {
      console.log('Role not found in response.');
    }
  })
  .catch((error) => {
    console.log('Error fetching role:', error);
  });
    getData();
  }, []);
    
  const getData = () => {
    axios
      .get('https://localhost:7224/api/EmployeeTasks')
      .then((result) => {
        const tasks = result.data;
        setData(tasks);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleSubmit = () => {
    const newTask = {
      tasks: task,
      tasksDescription: description,
      taskstatus: taskStatus,
    };
    axios
      .post('https://localhost:7224/api/EmployeeTasks', newTask)
      .then(() => {
        getData();
        setTask('');
        setDescription('');
        setTaskStatus('Pending');
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleEdit = (item) => {
    setEditId(item.id);
    setEditTask(item.tasks);
    setEditDescription(item.tasksDescription);
    setEditTaskStatus(item.taskstatus);
    setShow(true);
  };

  const handleUpdate = () => {
    const updatedTask = {
      id: editId,
      tasks: editTask,
      tasksDescription: editDescription,
      taskstatus: editTaskStatus,
    };
    axios
      .put(`https://localhost:7224/api/EmployeeTasks/${editId}`, updatedTask)
      .then(() => {
        getData();
        setShow(false);
        setEditId(null);
        setEditTask('');
        setEditDescription('');
        setEditTaskStatus('Pending');
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleDelete = (id) => {
    if (window.confirm('Are you sure to delete this task?')) {
      axios
        .delete(`https://localhost:7224/api/EmployeeTasks/${id}`)
        .then(() => {
          getData();
        })
        .catch((error) => {
          console.log(error);
        });
    }
  };

  return (
    <>
      <div className="navbarcontainer">
        <div className="websitename">
          <p onClick={() => window.location.reload(false)}>Task Management System</p>
        </div>
      </div>
      <div className="userinfo">
        <div className="welcomenote">
          <p>Welcome {role}!</p>
        </div>
        <div className="description">Hello... it's really good to see you again!</div>
      </div>
      <br />
      <br />
      <Container>
        <Row>
          <Col className='completedtasks'><h5>Completed Tasks: {data.filter(task => task.taskstatus === 'Completed').length}</h5></Col>
          <Col className='inprogresstasks'><h5>In Progress Tasks: {data.filter(task => task.taskstatus === 'InProgress').length}</h5></Col>
          <Col className='pendingtasks'><h5>Pending Tasks: {data.filter(task => task.taskstatus === 'Pending').length}</h5></Col>
        </Row>
      </Container>
      <br />
      <br />
      {role === 'admin' && (
        <Container>
          <Row>
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Task Name"
                value={task}
                onChange={(e) => setTask(e.target.value)}
              />
            </Col>
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Description"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
              />
            </Col>
            <Col className="radiobuttons">
              <label>
                <input
                  type="radio"
                  name="taskstatus"
                  value="Completed"
                  checked={taskStatus === 'Completed'}
                  onChange={(e) => setTaskStatus(e.target.value)}
                />
                &nbsp;Completed
              </label>
              &nbsp;
              <label>
                <input
                  type="radio"
                  name="taskstatus"
                  value="InProgress"
                  checked={taskStatus === 'InProgress'}
                  onChange={(e) => setTaskStatus(e.target.value)}
                />
                &nbsp;InProgress
              </label>
              &nbsp;
              <label>
                <input
                  type="radio"
                  name="taskstatus"
                  value="Pending"
                  checked={taskStatus === 'Pending'}
                  onChange={(e) => setTaskStatus(e.target.value)}
                />
                &nbsp;Pending
              </label>
            </Col>
            <Col>
              <button className="btn btn-primary" onClick={handleSubmit}>
                Submit
              </button>
            </Col>
          </Row>
        </Container>
      )}
      <br />
      <br />
      <Table striped bordered hover className='datatable'>
        <thead>
          <tr>
            <th>#</th>
            <th>Tasks</th>
            <th>Description</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {data && data.length > 0 ? (
            data.map((item, index) => (
              <tr key={item.id}>
                <td>{index + 1}</td>
                <td>{item.tasks}</td>
                <td>{item.tasksDescription}</td>
                <td>{item.taskstatus}</td>
                <td>
                  {role === 'admin' ? (
                    <>
                      <button className="btn btn-primary" onClick={() => handleEdit(item)}>
                        Edit
                      </button>
                      &nbsp;
                      <button className="btn btn-danger" onClick={() => handleDelete(item.id)}>
                        Delete
                      </button>
                    </>
                  ) : (
                    <button className="btn btn-secondary" onClick={() => handleEdit(item)}>
                      Update Status
                    </button>
                  )}
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="5">Loading...</td>
            </tr>
          )}
        </tbody>
      </Table>

      <Modal show={show} onHide={() => setShow(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Edit / Update</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Row>
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Task Name"
                value={editTask}
                onChange={(e) => setEditTask(e.target.value)}
                disabled={role !== 'admin'} // Only admin can edit task name
              />
            </Col>
            <Col>
              <input
                type="text"
                className="form-control"
                placeholder="Enter Description"
                value={editDescription}
                onChange={(e) => setEditDescription(e.target.value)}
                disabled={role !== 'admin'} // Only admin can edit description
              />
            </Col>
          </Row>
          <Col className="radiobuttons">
            <label>
              <input
                type="radio"
                name="editTaskstatus"
                value="Completed"
                checked={editTaskStatus === 'Completed'}
                onChange={(e) => setEditTaskStatus(e.target.value)}
              />
              &nbsp;Completed
            </label>
            &nbsp;
            <label>
              <input
                type="radio"
                name="editTaskstatus"
                value="InProgress"
                checked={editTaskStatus === 'InProgress'}
                onChange={(e) => setEditTaskStatus(e.target.value)}
              />
              &nbsp;InProgress
            </label>
            &nbsp;
            <label>
              <input
                type="radio"
                name="editTaskstatus"
                value="Pending"
                checked={editTaskStatus === 'Pending'}
                onChange={(e) => setEditTaskStatus(e.target.value)}
              />
              &nbsp;Pending
            </label>
          </Col>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShow(false)}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdate}>
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default Homepage;
