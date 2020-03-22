import "./ScriptViewer.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Button} from "react-bootstrap";
import DialogViewer from "../Components/DialogViewer";
import Recordings from "../Components/Recordings";
import {toast} from "react-toastify";
import {Link} from "react-router-dom";
import Scenes from "../Components/Scenes";

export default class ScriptViewer extends Component {
  headerHeight = 160;

  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;

    let columnHeight = window.innerHeight - this.headerHeight;

    this.state = {
      projectId: projectId,
      currentScenePosition: 0,
      maxScenePosition: 0,
      dialog:[],
      scenes:[],
      columnHeight:columnHeight,
      selectedDialogId:null
    };

    this.getNextScene = this.getNextScene.bind(this);
    this.getPreviousScene = this.getPreviousScene.bind(this);
    this.loadScene = this.loadScene.bind(this);
    this.loadScenes = this.loadScenes.bind(this);
    this.handleSceneSelected = this.handleSceneSelected.bind(this);
    this.handleResize = this.handleResize.bind(this);
    this.handleDialogSelected = this.handleDialogSelected.bind(this);

    window.addEventListener('resize', this.handleResize);

  }

  handleResize() {
    let columnHeight = window.innerHeight - this.headerHeight;

    this.setState({
      columnHeight: columnHeight
    });
  }

  handleDialogSelected(dialogId) {
    this.setState({
      selectedDialogId: dialogId
    });
  }

  componentDidMount() {
    this.loadScenes(this.state.projectId);
  }

  loadScenes(projectId) {
    SarService.getScenes(projectId)
      .then(r=> {
        this.setState({
          scenes: r,
          currentScenePosition: 0,
          maxScenePosition: r.length - 1
        }, ()=>{
          this.loadScene();
        });
      })
      .catch(error => {
        alert(error.message);
      });
  }

  handleSceneSelected(position) {
    let pos = parseInt(position);

    this.setState({
      currentScenePosition: pos
    }, ()=>{
      this.loadScene();
    })
  }

  getNextScene() {
    let scenePosition = this.state.currentScenePosition;
    scenePosition += 1;

    if (scenePosition > this.state.maxScenePosition){
      scenePosition = this.state.maxScenePosition;
    }

    this.setState({
      currentScenePosition: scenePosition
    }, ()=> {
      this.loadScene()
    });
  }

  getPreviousScene() {
    let scenePosition = this.state.currentScenePosition;

    scenePosition -= 1;

    if (scenePosition < 0) {
      scenePosition = 0;
    }

    this.setState({
      currentScenePosition: scenePosition
    }, ()=> {
      this.loadScene()
    });
  }

  loadScene() {
    let scenes = this.state.scenes;

    if (scenes.length > 0) {
      let scene = this.state.scenes[this.state.currentScenePosition];
      let start = scene.ScriptPosition;
      let end = scene.ScriptEndPosition;

      SarService.getLines(this.state.projectId, start, end)
        .then(r => {
          this.setState({
            dialog: r
          });
        })
        .catch(error => {
          toast.error(error.message);
        })
    }
  }

  render() {
    return (
      <div className="RecordDialog">
        <Row>
          <Col>
            <h3>Script Viewer</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/projects`}>Return to Projects</Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col md={3}>
            <Scenes
              scenes={this.state.scenes}
              height={this.state.columnHeight}
              onClick = {this.handleSceneSelected}
            />
          </Col>
          <Col md={6}>
            <Row style={{paddingTop:10, paddingBottom:10}}>
              <Col>
                <div className="float-md-left">
                  <Button
                    className="ScriptViewerButton"
                    variant="secondary"
                    size="sm"
                    disabled={this.state.currentScenePosition === 0}
                    onClick={this.getPreviousScene}>
                    Previous Scene
                  </Button>
                </div>
              </Col>
              <Col>
                <div className="float-md-right">
                  <Button
                    className="ScriptViewerButton"
                    variant="secondary"
                    size="sm"
                    disabled={this.state.currentScenePosition === this.state.maxScenePosition}
                    onClick={this.getNextScene}>Next Scene</Button>
                </div>
              </Col>
            </Row>
            <DialogViewer
              dialog={this.state.dialog}
              height={this.state.columnHeight}
              onDialogSelected={this.handleDialogSelected}
            />
          </Col>
          <Col md={3}>
            <Recordings
              projectId={this.state.projectId}
              dialogId={this.state.selectedDialogId}
              recordingsTimestamp = {this.state.recordingsTimestamp}
            />
          </Col>
        </Row>
      </div>
    );
  }
}