import "./Audio.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import { Row, Col, Form, ToggleButtonGroup, ToggleButton } from "react-bootstrap";
import {toast} from "react-toastify";

export default class Audio extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;

    this.state = {
      projectId: projectId,
      projectTitle: "",
      scenes:[],
      characters:[],
      viewMode: "scene",
      whatToShow: "all"
    };

    this.loadProject = this.loadProject.bind(this);
    this.loadScenes = this.loadScenes.bind(this);
    this.loadCharacters = this.loadCharacters.bind(this);
  }

  componentDidMount() {
    this.loadProject(this.state.projectId);
    this.loadScenes(this.state.projectId);
    this.loadCharacters(this.state.projectId);
  }

  loadProject(projectId) {
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

  loadScenes(projectId) {
    SarService.getAudioSummaryForScenes(projectId)
      .then(r => {
        this.setState({
          scenes: r
        })
      })
      .catch(e => {
        toast.error(e.message);
      })
  }

  loadCharacters(projectId) {
    SarService.getAudioSummaryForCharacters(projectId)
      .then(r => {
        this.setState({
          characters: r
        })
      })
      .catch(e => {
        toast.error(e.message);
      })
  }

  handleViewModeChange = event => {
    this.setState({
      viewMode: event.target.value
    })
  };

  handleWhatToShowChange = value => {
    this.setState({
      whatToShow: value
    });
  };

  renderByCharacters(characters) {
    let filtered = null;

    if (this.state.whatToShow === "all") {
      filtered = characters;
    } else  if (this.state.whatToShow === "incomplete") {
      filtered = characters.filter(character => character.Lines !== character.LinesWithAudio);
    } else if (this.state.whatToShow === "complete") {
      filtered = characters.filter(character => character.Lines === character.LinesWithAudio);
    } else {
      toast.error("Unknown value for What to Show");
      return;
    }

    let rows = filtered.map((character, i) => {
      return (
        <div className="audioSceneSection" key={i}>
          <Row className="audioSceneSectionDescription">
            <Col>
              Description: {character.Name}
            </Col>
          </Row>
          <Row className="audioSceneSectionLines">
            <Col>
              Lines: {character.Lines}
            </Col>
            <Col>
              Lines with Audio: {character.LinesWithAudio}
            </Col>
          </Row>
        </div>
      );
    });

    return (
      <div className="audioSummary">
        <Row className="audioSummaryHeader">
          <Col>
            Audio Summary by Scene
          </Col>
        </Row>
        { rows }
      </div>
    );
  }

  renderByScenes(scenes) {
    let filtered = null;

    if (this.state.whatToShow === "all") {
      filtered = scenes;
    } else  if (this.state.whatToShow === "incomplete") {
      filtered = scenes.filter(scene => scene.Lines !== scene.LinesWithAudio);
    } else if (this.state.whatToShow === "complete") {
        filtered = scenes.filter(scene => scene.Lines === scene.LinesWithAudio);
    } else {
      toast.error("Unknown value for What to Show");
      return;
    }

    let rows = filtered.map((scene, i) => {
      return (
        <div className="audioSceneSection" key={i}>
          <Row className="audioSceneSectionNumber">
            <Col>
              Number: {scene.Number}
            </Col>
          </Row>
          <Row className="audioSceneSectionDescription">
            <Col>
              Description: {scene.Description}
            </Col>
          </Row>
          <Row className="audioSceneSectionLines">
            <Col>
              Lines: {scene.Lines}
            </Col>
            <Col>
              Lines with Audio: {scene.LinesWithAudio}
            </Col>
          </Row>
        </div>
      );
    });

    return (
      <div className="audioSummary">
        <Row className="audioSummaryHeader">
          <Col>
            Audio Summary by Scene
          </Col>
        </Row>
        { rows }
      </div>
    );
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
          <Col md={2} >
            View Mode
          </Col>
        </Row>
        <Row>
          <Col md={2}>
            <Form.Group controlId="viewMode">
              <Form.Control
                as="select"
                value={this.state.viewMode}
                onChange={this.handleViewModeChange}
              >
                <option value="scene">Scene View</option>
                <option value="character">Character View</option>
              </Form.Control>
            </Form.Group>
          </Col>
          <Col md={2}>
            <ToggleButtonGroup
              type="radio"
              name="whatToShow"
              value={this.state.whatToShow}
              onChange={this.handleWhatToShowChange}>
              <ToggleButton value="complete">Complete</ToggleButton>
              <ToggleButton value="all">All</ToggleButton>
              <ToggleButton value="incomplete">InComplete</ToggleButton>
            </ToggleButtonGroup>
          </Col>
        </Row>
        {
          (this.state.viewMode === 'scene')
          ? this.renderByScenes(this.state.scenes)
          : this.renderByCharacters(this.state.characters)
        }
      </div>
    );
  }
}