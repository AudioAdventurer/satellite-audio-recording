import "./Participants.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { FaPlusCircle } from "react-icons/fa";

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
    SarService.getParticipants(projectId)
      .then(r => {
        this.setState({
          participants: r
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.Id != null) {
          return (
            <tr key={item.Id}>
              <td>{item.GivenName}</td>
              <td>{item.FamilyName}</td>
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
              <span id="newProject" onClick={() => this.handleNewPerson()}>
                <FaPlusCircle/>
              </span>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              <thead>
              <tr>
                <th>First Name</th>
                <th>Last Name</th>
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