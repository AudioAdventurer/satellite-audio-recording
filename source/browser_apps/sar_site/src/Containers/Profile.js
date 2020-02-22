import "./User.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Form, Button} from "react-bootstrap";
import { Link, Redirect } from 'react-router-dom'
import {toast} from "react-toastify";

export default class Profile extends Component {
  constructor(props) {
    super(props);

    let personId = SarService.UserProperties.PersonId;

    this.state = {
      redirect: false,
      personId: personId,
      givenName: "",
      familyName: "",
      phoneNumber: ""
    };

    this.loadPerson = this.loadPerson.bind(this);
  }

  componentDidMount() {
    this.loadPerson();
  }

  loadPerson() {
    SarService.getSelf()
      .then(r => {
        this.setState({
          personId: r.Id,
          givenName: r.GivenName ?? '',
          familyName: r.FamilyName ?? '',
          phoneNumber: r.PhoneNumber ?? ''
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Redirect to={`/`} />
    }
  };

  handleSubmit = event => {
    event.preventDefault();

    try {
      let personId = this.state.personId;

      let givenName = this.state.givenName;

      let familyName = this.state.familyName;
      if (familyName === "") {
        familyName = null;
      }

      let phoneNumber = this.state.phoneNumber;
      if (phoneNumber === "") {
        phoneNumber = null;
      }

      let profile = {
        PersonId: personId,
        GivenName: givenName,
        FamilyName: familyName,
        PhoneNumber: phoneNumber,
      };

      SarService.saveProfile(profile)
        .then(r => {
          this.setState({
            redirect: true
          });
        })
        .catch(e => {
          toast.error(e.message);
        })
    } catch (e) {
      toast.error(e.message);
    }
  };

  render() {
    return (
      <div className="User">
        {this.renderRedirect()}
        <Row>
          <Col>
            <h3>User: {`${this.state.givenName} ${this.state.familyName}`.trim()}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/`}>Return to Home</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form onSubmit={this.handleSubmit}>
              <Form.Group controlId="givenName">
                <Form.Label>First Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user given name"
                  value={this.state.givenName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="familyName">
                <Form.Label>Last Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter user family name"
                  value={this.state.familyName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="phoneNumber">
                <Form.Label>Phone Number</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter users's phone number"
                  value={this.state.phoneNumber}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Button variant="primary" type="submit">
                Save
              </Button>
            </Form>
          </Col>
        </Row>
      </div>
    );
  }
}