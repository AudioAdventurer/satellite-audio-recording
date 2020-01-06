import "./CharacterLines.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'

export default class CharacterLines extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;
    let characterId = this.props.match.params.characterId;
    let start = 0;
    let limit = 10;

    this.state = {
      projectId: projectId,
      characterId: characterId,
      characterName:"",
      character:{},
      minSequenceNumber:0,
      maxSequenceNumber:0,
      limit:limit
    };

    this.loadCharacter = this.loadCharacter.bind(this);
    this.loadCharacterLines = this.loadCharacterLines.bind(this);
    this.getPreviousScriptLines = this.getPreviousScriptLines.bind(this);
    this.getNextScriptLines = this.getNextScriptLines.bind(this);
    this.setMinMax = this.setMinMax.bind(this);

    this.loadCharacter(projectId, characterId);
    this.loadCharacterLines(projectId, characterId, start, limit);
  }

  loadCharacter(projectId, characterId) {
    SarService.getCharacter(projectId, characterId)
      .then(r => {
        this.setState({
          character: r,
          characterName: r.Name
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  loadCharacterLines(projectId, characterId, start, limit) {
    SarService.getNextLinesByCharacter(projectId, characterId, start, limit)
      .then(r => {
        this.setMinMax(r);

        this.setState({
          characterLines: r
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  setMinMax(list) {
    let minSequenceNumber = Number.MAX_SAFE_INTEGER;
    let maxSequenceNumber = 0;

    for (let i = 0; i< list.length; i++){
      let item = list[i];
      let sequenceNumber = item.SequenceNumber;

      if (sequenceNumber < minSequenceNumber)
      {
        minSequenceNumber = sequenceNumber;
      }
      if (sequenceNumber > maxSequenceNumber)
      {
        maxSequenceNumber = sequenceNumber;
      }
    }

    this.setState({
      minSequenceNumber: minSequenceNumber,
      maxSequenceNumber: maxSequenceNumber
    });
  }

  getNextScriptLines() {
    let projectId = this.state.projectId;
    let characterId = this.state.characterId;
    let limit = this.state.limit;
    let start = this.state.maxSequenceNumber;

    SarService.getNextLinesByCharacter(projectId, characterId, start, limit)
      .then(r => {
          this.setMinMax(r);

          this.setState({
            characterLines: r
          });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  getPreviousScriptLines() {
    let projectId = this.state.projectId;
    let characterId = this.state.characterId;
    let limit = this.state.limit;
    let start = this.state.minSequenceNumber;

    SarService.getPreviousLinesByCharacter(projectId, characterId, start, limit)
      .then(r => {
        this.setMinMax(r);

        this.setState({
          characterLines: r
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
        if (item.CharacterDialogId != null) {
          let url =`/projects/${this.state.projectId}/dialog/${item.characterDialogId}`;

          return (
            <tr key={item.CharacterDialogId}>
              <td>{item.SceneNumber}</td>
              <td><Link to={url}>Recording</Link></td>
              <td>{item.RecordingCount}</td>
              <td>{item.Line}</td>
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
      <div className="CharacterLines">
        <Row>
          <Col>
            <h3>Lines for {this.state.characterName}</h3>
          </Col>
        </Row>
        <Row>
          <Col>
            <div className="float-md-left">
              <button variant="secondary" size="sm" onClick={this.getPreviousScriptLines}>Previous Lines</button>
            </div>
          </Col>
          <Col>
            <div className="float-md-right">
              <button variant="secondary" size="sm" onClick={this.getNextScriptLines}>Next Lines</button>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              <thead>
              <tr>
                <th width={150}>Scene Number</th>
                <th width={100}>Action</th>
                <th width={100}>Recordings</th>
                <th>Line</th>
              </tr>
              </thead>
              { this.renderTableBody(this.state.characterLines) }
            </Table>
          </Col>
        </Row>
      </div>
    );
  }
}