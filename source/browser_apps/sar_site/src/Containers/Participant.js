import "./Participant.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Form, Button} from "react-bootstrap";
import { Link, Redirect } from 'react-router-dom'
import {toast} from "react-toastify";

export default class Participant extends Component {
  constructor(props) {
    super(props);

    let personId = this.props.match.params.participantId;
    let projectId = this.props.match.params.projectId;

    let isNew = false;
    if (personId === 'new') {
      isNew = true;
    }

    this.state = {
      isNew: isNew,
      availablePeople: [],
      personName: '',
      redirect:false,
      projectId:projectId,
      personId:personId,
      accessTypes:[]
    };

    this.loadPerson = this.loadPerson.bind(this);
    this.loadAvailablePeople = this.loadAvailablePeople.bind(this);
  }

  componentDidMount() {
    if (this.state.isNew) {
      this.loadAvailablePeople();
    } else {
      this.loadPerson(this.state.projectId, this.state.personId);
    }
  }

  loadAvailablePeople() {
    let projectId = this.state.projectId;

    SarService.getAvailablePeople(projectId)
      .then(r => {
        this.setState({
          availablePeople: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  loadPerson(projectId, personId) {
    SarService.getParticipant(projectId, personId)
      .then(r => {
        this.setState({
          personName: `${r.GivenName} ${r.FamilyName}`.trim() ,
          accessTypes: r.AccessTypes ?? []
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

  handleAccessTypesChange = event => {
    let options = event.target.options;
    let value = [];

    for (let i = 0, l = options.length; i < l; i++) {
      if (options[i].selected) {
        value.push(options[i].value);
      }
    }
    this.setState({
      accessTypes: value
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
      let participantAccess = {
        PersonId: this.state.personId,
        AccessTypes: this.state.accessTypes
      };

      SarService.saveParticipantAccess(this.state.projectId, participantAccess)
        .then(r => {
          this.setState({
            redirect: true
          });
        })
        .catch(e =>{
          toast.error(e.message);
        })
    } catch (e) {
      toast.error(e.message);
    }
  };

  renderAccessTypesSelect() {
    return(
      <Form.Control as="select"
                    multiple
                    value={this.state.accessTypes}
                    onChange={this.handleAccessTypesChange}>
        <option value="performer">Performer</option>
        <option value="audio">Audio Engineer</option>
        <option value="director">Director</option>
        <option value="producer">Producer</option>
        <option value="writer">Writer</option>
        <option value="owner">Owner</option>
      </Form.Control>);
  }

  renderUsersFormGroup(users) {
    let rows = users.map((item, i) => {
      return (
        <option key={i} value={item.Id}>{`${item.GivenName} ${item.FamilyName}`.trim()}</option>
      )
    });

    return (
      <Form.Group controlId="personId">
        <Form.Label>User</Form.Label>
        <Form.Control
          as="select"
          value={this.state.personId}
          onChange={this.handleChange}>
          {rows}
        </Form.Control>
      </Form.Group>);
  }

  renderUserName(userName) {
    return (
      <div>
        {userName}
      </div>);
  }

  render() {
    return (
      <div className="Character">
        {this.renderRedirect()}
        <Row>
          <Col>
            <h3>Participant: {this.state.personName}</h3>
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
              { this.state.isNew
                ? this.renderUsersFormGroup(this.state.availablePeople)
                : this.renderUserName(this.state.personName)}
              <Form.Group controlId="accessTypes">
                <Form.Label>Project Access</Form.Label>
                {this.renderAccessTypesSelect(this.state.renderAccessTypesSelect)}
              </Form.Group>
              <Button variant="primary"
                      type="submit">
                Save
              </Button>
            </Form>
          </Col>
        </Row>
      </div>
    );
  }
}