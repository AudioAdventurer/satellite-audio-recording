import "./Projects.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'
import { FaPlusCircle } from "react-icons/fa";

export default class Projects extends Component {
  constructor(props) {
    super(props);

    this.state = {
      projects:[]
    };

    this.loadData()
  }

  loadData() {
    SarService.getProjects()
      .then(r => {
        this.setState({
          projects: r
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
          let projectUrl = `/projects/${item.Id}`;

          return (
            <tr key={item.Id}>
              <td>
                <Row>
                  <Col>
                    <Link to={projectUrl}>{item.Title}</Link>
                  </Col>
                </Row>
                <Row>
                  <Col sm={3}><Link to={`${projectUrl}/characters`}>Characters</Link></Col>
                  <Col sm={3}><Link to={`${projectUrl}/scenes`}>Scenes</Link></Col>
                  <Col sm={3}><Link to={`${projectUrl}/actors`}>Actors</Link></Col>
                </Row>
              </td>
              <td>{item.Description}</td>
              <td>{item.Language}</td>
              <td>{item.Locale}</td>
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
      <div className="Projects">
        <Row>
          <Col>
            <h2>Projects</h2>
          </Col>
          <Col>
            <div className="float-md-right">
              <span id="newProject" onClick={() => this.handleNewProject()}>
                <FaPlusCircle/>
              </span>
            </div>
          </Col>
        </Row>
        <Table striped bordered hover>
          <thead>
          <tr>
            <th>Title</th>
            <th>Description</th>
            <th>Language</th>
            <th>Locale</th>
          </tr>
          </thead>
          { this.renderTableBody(this.state.projects) }
        </Table>
      </div>
    );
  }
}