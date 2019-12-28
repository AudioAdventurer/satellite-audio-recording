import "./App.css";
import React, { Component } from "react";
import { Link } from "react-router-dom";
import Routes from "./Routes";
import { LinkContainer } from "react-router-bootstrap";
import SarService from "./Services/SarService";
import {Nav, Navbar} from "react-bootstrap";

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
    SarService.close();
    this.userHasAuthenticated(false);
  };

  renderLoggedInNavBar() {
    return(
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          <Nav.Item>
            <LinkContainer to="/">
              <Nav.Link>Home</Nav.Link>
            </LinkContainer>
          </Nav.Item>
          <Nav.Item>
            <LinkContainer to="/projects">
              <Nav.Link>Projects</Nav.Link>
            </LinkContainer>
          </Nav.Item>
        </Nav>
        <Nav >
          <Nav.Item onClick={this.handleLogout}>Logout</Nav.Item>
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
        { this.renderNavbar() }
        <Routes childProps={childProps} />
      </div>
    );
  }
}