import "./Login.css";
import React, { Component } from "react";
import SarService from "../Services/SarService";
import { Link } from "react-router-dom";
import {Form} from "react-bootstrap";
import LoaderButton from "../Components/LoaderButton";

export default class Login extends Component {
    constructor(props) {
        super(props);

        let username = SarService.getCookie("username");

        this.state = {
            isLoading:false,
            username : username,
            showNewPassword: false,
            newPassword: "",
            password: ""
        };
    }

    validateForm() {
        return this.state.username.length > 0
            && this.state.password.length > 0;
    }

    handleChange = event => {
        this.setState({
            [event.target.id]: event.target.value
        });
    };

    handleSubmit = event => {
        event.preventDefault();

        try {
            SarService.login(this.state.username, this.state.password)
              .then(r =>{
                  let jwt = r.JWT;
                  SarService.setSession(r.Session);
                  SarService.setCookie("username", this.state.username);
                  SarService.setJwt(jwt);

                  this.props.userHasAuthenticated(true);
              })
              .catch(e =>{
                  alert(e.message);
              })
        } catch (e) {
            alert(e.message);
        }
    };

    render() {
        return (
            <div className="Login">
                <Form onSubmit={this.handleSubmit}>
                    <Form.Group controlId="username" size="large">
                        <Form.Label>Username</Form.Label>
                        <Form.Control
                            autoFocus
                            type="text"
                            value={this.state.username}
                            onChange={this.handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId="password" size="large">
                        <Form.Label>Password</Form.Label>
                        <Form.Control
                            value={this.state.password}
                            onChange={this.handleChange}
                            type="password"
                        />
                    </Form.Group>
                    <LoaderButton
                        block
                        variant="secondary"
                        size="large"
                        disabled={!this.validateForm()}
                        type="submit"
                        isLoading={this.state.isLoading}
                        text="Login"
                        loadingText="Logging in…"
                    />
                    <div className="BottomBar">
                        <Link to="/forgot">Forgot Password</Link>
                    </div>
                </Form>
            </div>
        );
    }
}