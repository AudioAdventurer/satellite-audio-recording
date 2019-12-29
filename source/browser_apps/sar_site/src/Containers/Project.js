import "./Project.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Form, Button} from "react-bootstrap";

export default class Project extends Component {
  constructor(props) {
    super(props);

    let id = this.props.match.params.id;

    this.state = {
      id: id,
      projectTitle: "",
      description: "",
      language: "",
      locale:""
    };

    this.loadData(id);
  }

  loadData(id) {
    SarService.getProject(id)
      .then(r => {
        this.setState({
          projectTitle: r.Title || "",
          description: r.Description || "",
          language: r.Language || "",
          locale: r.Locale || ""
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  handleSubmit = event => {
    event.preventDefault();

    try {
      let proj = {
        "Id": this.state.id,
        "Title": this.state.projectTitle,
        "Description": this.state.description,
        "Language": this.state.language,
        "Locale": this.state.locale
      };

      SarService.saveProject(proj)
        .then(r => {
          alert("Saved");
        })
        .catch(e =>{
          alert(e.message);
        })
    } catch (e) {
      alert(e.message);
    }
  };

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  render() {
    return (
      <div className="Project">
        <Row>
          <Col>
            <h2>Project</h2>
          </Col>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
            <Form onSubmit={this.handleSubmit}>
              <Form.Group controlId="projectTitle">
                <Form.Label>Title</Form.Label>
                <Form.Control
                  type="text"
                  value={this.state.projectTitle}
                  onChange={this.handleChange}
                  placeholder="Enter title"/>
              </Form.Group>
              <Form.Group controlId="description">
                <Form.Label>Description</Form.Label>
                <Form.Control
                  value={this.state.description}
                  onChange={this.handleChange}
                  placeholder="Enter description"/>
              </Form.Group>
              <Form.Group controlId="language">
                <Form.Label>Language</Form.Label>
                <Form.Control
                  type="text"
                  value={this.state.language}
                  onChange={this.handleChange}
                  placeholder="Enter language"/>
              </Form.Group>
              <Form.Group controlId="locale">
                <Form.Label>Locale</Form.Label>
                <Form.Control
                  type="text"
                  value={this.state.locale}
                  onChange={this.handleChange}
                  placeholder="Enter locale"/>
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