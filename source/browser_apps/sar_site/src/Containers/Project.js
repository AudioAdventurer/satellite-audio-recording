import "./Project.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Form, Button, Tabs, Tab} from "react-bootstrap";
import LoaderButton from "../Components/LoaderButton";

export default class Project extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;

    this.state = {
      id: projectId,
      projectTitle: "",
      description: "",
      selectedFile:"",
      importingScript:false
    };

    this.loadData(projectId);
  }

  loadData(projectId) {
    SarService.getProject(projectId)
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

  validateFileSelected() {
    return this.state.selectedFile !== "";
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value
    });
  };

  handleSelectedFile = event => {
    this.setState({
      selectedFile: event.target.files[0]
    });
  }

  loadFile = event => {
    event.preventDefault();

    this.setState({
      importingScript:true
    });

    let data = new FormData();
    data.append('file', this.state.selectedFile);

    SarService.importFountain(this.state.id, data)
      .then(r=>{
        this.setState({
          importingScript:false
        });
        alert("Script Loaded");
      })
      .catch(e=>{
        alert(e.message);
        this.setState({
          importingScript:false
        });
      });

  };

  render() {
    return (
      <div className="Project">
        <Row>
          <Col>
            <h3>{`Project: ${this.state.projectTitle}`}</h3>
          </Col>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col>
            <Tabs>
              <Tab eventKey="main" title="General">
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
                  <Button variant="primary" type="submit">
                    Save
                  </Button>
                </Form>
              </Tab>
              <Tab eventKey="import" title="Import Script">
                <p>Warning - this will remove all scenes and characters and reset this project.</p>
                <p>A script in the Fountain format can be loaded automatically.</p>
                <Form onSubmit={this.loadFile}>
                  <Form.Group controlId="selectedFile">
                    <Form.Label>Upload Fountain File</Form.Label>
                    <Form.Control
                      type="file"
                      onChange={this.handleSelectedFile}/>
                  </Form.Group>
                  <LoaderButton
                    isLoading={this.state.importingScript}
                    loadingText = "Importing ..."
                    disabled={!this.validateFileSelected}
                    text="Import Script"
                    type="submit" />
                </Form>
              </Tab>
            </Tabs>
          </Col>
        </Row>
      </div>
    );
  }
}