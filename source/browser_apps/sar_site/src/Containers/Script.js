import "./Script.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'
import {toast} from "react-toastify";
import Scenes from "../Components/Scenes";
import DialogViewer from "../Components/DialogViewer";

export default class Script extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;

    this.state = {
      currentScene: null,
      scenes: [],
      dialog:[],
      projectId: projectId,
      sceneId: null
    };

    this.handleSceneSelected = this.handleSceneSelected.bind(this);
    this.loadScenes = this.loadScenes.bind(this);
    this.loadDialog = this.loadDialog.bind(this);
  }

  handleSceneSelected(sceneId) {
    this.setState({
      sceneId: sceneId
    }, ()=>{
      this.loadDialog();
    });
  }

  componentDidMount() {
    this.loadScenes();
  }

  loadScenes() {
    SarService.getScenes(this.state.projectId)
      .then(r => {
        this.setState({
          scenes: r
        });
      })
      .catch(e => {
        toast.error(e.message);
      });
  }

  loadDialog() {
    if (this.state.sceneId !== null) {
      SarService.getLinesByScene(this.state.projectId, this.state.sceneId)
        .then(r => {
          this.setState({
            dialog: r
          });
        })
        .catch(e => {
          toast.error(e.message);
        });
    }
  }


  render() {
    return (
      <div className="Script">
        <Row>
          <Col>
            <h3>Script</h3>
          </Col>
          <Col>
          </Col>
        </Row>
        <Row>
          <Col md={3}>
            <Scenes
              onSceneSelected={this.handleSceneSelected}
              scenes={this.state.scenes}
            />
          </Col>
          <Col md={9}>
            <DialogViewer
              lines={this.state.dialog}
            />
          </Col>
        </Row>
      </div>
    );
  }
}