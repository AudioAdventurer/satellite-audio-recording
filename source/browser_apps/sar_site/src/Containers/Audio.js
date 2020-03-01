import "./Audio.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col} from "react-bootstrap";
import {toast} from "react-toastify";

export default class Audio extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;

    this.state = {
      projectId: projectId,
      projectTitle: "",
    };
  }

  componentDidMount() {
    this.loadData(this.state.projectId);
  }

  loadData(projectId) {
    SarService.getProject(projectId)
      .then(r => {
        this.setState({
          projectTitle: r.Title || ""
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }



  render() {
    return (
      <div className="Audio">
        <Row>
          <Col>
            <h3>{`Project: ${this.state.projectTitle}`}</h3>
          </Col>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>

          </Col>
        </Row>
      </div>
    );
  }
}