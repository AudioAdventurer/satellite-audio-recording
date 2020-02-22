import "./App.css";
import React, { Component } from "react";
import { Link } from "react-router-dom";
import Routes from "./Routes";
import { LinkContainer } from "react-router-bootstrap";
import SarService from "./Services/SarService";
import {Nav, Navbar} from "react-bootstrap";
import { ToastContainer } from 'react-toastify'; 
import 'react-toastify/dist/ReactToastify.css';

export default class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      isAuthenticating: true,
      messageList:[]
    };
  }

  userHasAuthenticated = authenticated => {
    this.setState({ isAuthenticated: authenticated });
  };

  handleLogout = event => {
    SarService.logout();
    this.userHasAuthenticated(false);
  };

  renderUsers(){
    if (SarService.isSystemAdmin()
        || SarService.isSystemOwner())
    {
      return (
        <Nav.Item>
          <LinkContainer to="/users">
            <Nav.Link>Users</Nav.Link>
          </LinkContainer>
        </Nav.Item>
      );
    }
  }

  renderLoggedInNavBar() {
    return(
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          <Nav.Item>
            <LinkContainer to="/projects">
              <Nav.Link>
                Projects
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
          {this.renderUsers()}
        </Nav>
        <Nav >
          <Nav.Item>
            <LinkContainer to="/profile">
              <Nav.Link>
                Profile
              </Nav.Link>
            </LinkContainer>
          </Nav.Item>
          <Nav.Item onClick={this.handleLogout}>
            <Nav.Link>
              Logout
            </Nav.Link>
          </Nav.Item>
        </Nav>
      </Navbar.Collapse>
    );
  }

  renderNotLoggedInNavBar() {
    return (
      <Navbar.Collapse>
        <Nav className="mr-auto">
        </Nav>
        <Nav >
          <Nav.Item>
            <LinkContainer to="/login">
              <Nav.Link>Login</Nav.Link>
            </LinkContainer>
          </Nav.Item>
        </Nav>
      </Navbar.Collapse>
    );
  }

  renderNavbar() {
    return (
      <Navbar bg="light" expand="lg">
        <Navbar.Brand>
          <Link to="/">Satellite Audio Recorder</Link>
        </Navbar.Brand>
        {this.state.isAuthenticated ? this.renderLoggedInNavBar() : this.renderNotLoggedInNavBar() }
      </Navbar>
    );
  }

  render() {
    const childProps = {
      isAuthenticated: this.state.isAuthenticated,
      userHasAuthenticated: this.userHasAuthenticated
    };

    return (
      <div className="App container">
        <ToastContainer 
          position="top-center" 
          autoClose={false} 
          closeOnClick 
        />
        { this.renderNavbar() }
        <Routes childProps={childProps} />
      </div>
    );
  }
}