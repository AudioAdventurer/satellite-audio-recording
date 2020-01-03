import "./Characters.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { FaPlusCircle } from "react-icons/fa";
import { Link } from 'react-router-dom'

export default class Characters extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;

    this.state = {
      projectId: projectId,
      projectTitle:"",
      project:{},
      characters:[]
    };

    this.loadProject(projectId);
    this.loadCharacters(projectId);
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

  loadCharacters(projectId) {
    SarService.getCharactersWithActors(projectId)
      .then(r => {
        this.setState({
          characters: r
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
          let url = `/projects/${this.state.projectId}/characters/${item.Id}`
          let actorGivenName = "";
          let actorFamilyName = "";

          if (item.Actor !== null) {
            actorGivenName = item.Actor.GivenName;
            actorFamilyName = item.Actor.FamilyName;
          }

          return (
            <tr key={item.Id}>
              <td><Link to={url}>{item.Name}</Link></td>
              <td>{actorGivenName}</td>
              <td>{actorFamilyName}</td>
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
      <div className="Characters">
        <Row>
          <Col>
            <h3>Characters in {this.state.projectTitle}</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/projects/${this.state.projectId}/characters/new`}>
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
                <th>Actor First Name</th>
                <th>Actor Last Name</th>
              </tr>
              </thead>
              { this.renderTableBody(this.state.characters) }
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}