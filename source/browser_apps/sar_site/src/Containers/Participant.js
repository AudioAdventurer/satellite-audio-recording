import "./Participant.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Form, Button} from "react-bootstrap";
import { Link, Redirect } from 'react-router-dom'
import uuid from 'uuid';

export default class Character extends Component {
  constructor(props) {
    super(props);

    let personId = this.props.match.params.participantId;
    let projectId = this.props.match.params.projectId;

    this.state = {
      redirect:false,
      projectId:projectId,
      personId:personId,
      givenName:"",
      familyName:"",
      email:"",
      phoneNumber:"",
      accessTypes:[]
    };

    if (personId !== 'new') {
      this.loadPerson(projectId, personId);
    }
  }


  loadPerson(projectId, personId) {
    SarService.getParticipant(projectId, personId)
      .then(r => {
        this.setState({
          givenName: r.GivenName ?? '',
          familyName: r.FamilyName ?? '',
          email: r.Email ?? '',
          phoneNumber: r.PhoneNumber ?? '',
          accessTypes: r.AccessTypes ?? []
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  renderRedirect = () => {
    if (this.state.redirect) {
      return <Redirect to={`/projects/${this.state.projectId}/participants`} />
    }
  };

  handleSubmit = event => {
    event.preventDefault();

    try {
      let personId = this.state.characterId;
      if (personId==='new'){
        personId = uuid.v4();
      }

      let projectId = this.state.projectId;

      let givenName = this.state.givenName;

      let familyName = this.state.familyName;
      if (familyName === "") {
        familyName = null;
      }

      let phoneNumber = this.state.phoneNumber;
      if (phoneNumber === "") {
        phoneNumber = null;
      }

      let email = this.state.email;
      if (email === "") {
        email = null;
      }

      let accessTypes = this.state.accessTypes;

      let person = {
        Id: personId,
        ProjectId: projectId,
        GivenName: givenName,
        FamilyName: familyName,
        PhoneNumber: phoneNumber,
        Email: email,
        AccessTypes: accessTypes
      };

      SarService.saveParticipant(person)
        .then(r => {
          this.setState({
            redirect: true
          });
        })
        .catch(e =>{
          alert(e.message);
        })
    } catch (e) {
      alert(e.message);
    }
  };

  renderSelect(){
    return(
      <Form.Control as="select"
                    multiple
                    value={this.state.accessTypes}
                    onChange={this.handleChange}>
        <option value="actor">Actor</option>
        <option value="audio">Audio Engineer</option>
        <option value="director">Director</option>
        <option value="producer">Producer</option>
        <option value="writer">Writer</option>
        <option value="owner">Owner</option>
      </Form.Control>);
  }

  render() {
    return (
      <div className="Character">
        {this.renderRedirect()}
        <Row>
          <Col>
            <h3>Participant: {`${this.state.givenName} ${this.state.familyName}`.trim()}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/projects/${this.state.projectId}/participants`}>Return to Participants</Link>
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
                  placeholder="Enter participant given name"
                  value={this.state.givenName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="familyName">
                <Form.Label>Last Name</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter participant family name"
                  value={this.state.familyName}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="email">
                <Form.Label>Email</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter participant's email address"
                  value={this.state.email}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="phoneNumber">
                <Form.Label>Phone Number</Form.Label>
                <Form.Control
                  type="text"
                  placeholder="Enter participant's phone number"
                  value={this.state.phoneNumber}
                  onChange={this.handleChange}
                />
              </Form.Group>
              <Form.Group controlId="accessTypes">
                <Form.Label>Actor</Form.Label>
                {this.renderSelect(this.state.accessTypes)}
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