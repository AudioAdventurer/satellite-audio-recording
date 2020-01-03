import "./Participants.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { FaPlusCircle } from "react-icons/fa";
import { Link } from 'react-router-dom'

export default class Participants extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;

    this.state = {
      projectId: projectId,
      projectTitle:"",
      project:{},
      participants:[]
    };

    this.loadParticipants = this.loadParticipants.bind(this);
    this.loadProject = this.loadProject.bind(this);

    this.loadProject(projectId);
    this.loadParticipants(projectId);
  }

  loadProject(projectId) {
    SarService.getProject(projectId)
      .then(r => {
        this.setState({
          project: r,
          projectTitle: r.Title
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  loadParticipants(projectId) {
    SarService.getParticipantsWithAccess(projectId)
      .then(r => {
        this.setState({
          participants: r
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  handleNewPerson() {

  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.Id != null) {
          let url = `/projects/${this.state.projectId}/participants/${item.Id}`;
          let name = `${item.GivenName} ${item.FamilyName}`.trim();

          return (
            <tr key={item.Id}>
              <td>
                <Link to={url}>
                  {name}
                </Link>
                </td>
              <td>{item.Email}</td>
              <td>{item.PhoneNumber}</td>
              <td>{item.AccessTypes.join(', ')}</td>
            </tr>
          );
        } else {
          return "";
        }
      });

      return(
        <tbody>
        {rows}
        </tbody>);
    }

    return(
      <tbody></tbody>);
  }

  render() {
    return (
      <div className="Participants">
        <Row>
          <Col>
            <h3>Participants in {this.state.projectTitle}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/projects/${this.state.projectId}/participants/new`}>
                <FaPlusCircle/>
              </Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              <thead>
              <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Phone Number</th>
                <th>Access</th>
              </tr>
              </thead>
              { this.renderTableBody(this.state.participants) }
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}