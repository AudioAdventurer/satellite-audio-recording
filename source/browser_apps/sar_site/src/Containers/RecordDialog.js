import "./RecordDialog.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { Link } from 'react-router-dom'

export default class RecordDialog extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;
    let dialogId = this.props.match.params.dialogId;

    this.state = {
      projectId: projectId,
      dialogId: dialogId,
      currentDialog: null,
      dialog:[]
    };

    this.loadDialogContext = this.loadDialogContext.bind(this);
    this.getPreviousLine = this.getPreviousLine.bind(this);
    this.getNextLine = this.getNextLine.bind(this);
  }

  componentDidMount() {
    this.loadDialogContext(this.state.projectId, this.state.dialogId);
  }

  loadDialogContext(projectId, dialogId) {
    SarService.getDialogContext(projectId, dialogId)
      .then(r => {
        let currentDialog = null;

        r.forEach( line => {
            if (line.CharacterDialogId !== null
                && line.CharacterDialogId === dialogId) {
              currentDialog = line;
            }
        });

        this.setState({
          dialog: r,
          currentDialog: currentDialog
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  getNextLine() {

  }

  getPreviousLine() {

  }


  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.LineType === 'Scene') {
          return(
            <Row>
              <Col style={{
                fontWeight:'bold',
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'Gainsboro'}} >
                {item.Line}
              </Col>
            </Row>);
        } else if (item.LineType === 'Character') {
          return(
            <Row>
              <Col style={{
                textAlign:'center',
                paddingTop: '5px',
                paddingBottom: '0px',
                backgroundColor:'Gainsboro'}}>
                {item.Line}
              </Col>
            </Row>);
        } else if (item.LineType === 'Dialogue') {
          if (item.CharacterDialogId === null) {
            return (
              <Row>
                <Col style={{
                  backgroundColor: 'Gainsboro',
                  paddingTop: '5px',
                  paddingBottom: '5px',
                  paddingLeft:'100px',
                  paddingRight:'100px'
                }}>
                  {item.Line}
                </Col>
              </Row>);
          } else {
            return (
              <Row>
                <Col style={{
                  backgroundColor:'white',
                  border:'2px solid black',
                  paddingTop:'20px',
                  paddingBottom:'20px',
                  paddingLeft:'100px',
                  paddingRight:'100px'
                }}>
                  {item.Line}
                </Col>
              </Row>);
          }
        } else if (item.LineType === 'Action') {
          return (
            <Row>
              <Col style={{
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'Gainsboro'}}>
                {item.Line}
              </Col>
            </Row>);
        } else if (item.lineType === 'Parenthetical') {
          return (
            <Row>
              <Col style={{
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'Gainsboro'}}>
                ({item.Line})
              </Col>
            </Row>);
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
      <div className="RecordDialog">
        <Row>
          <Col>
            <h3>Record Dialog</h3>
          </Col>
        </Row>

        <Row>
          <Col md={9}>
            <Row>
              <Col>
                <div className="float-md-left">
                  <button variant="secondary" size="sm" onClick={this.getPreviousLine}>Previous Line</button>
                </div>
              </Col>
              <Col>
                <Row>
                  <Col>
                    Recording Controls
                  </Col>
                </Row>
                <Row>
                  <Col>

                  </Col>
                </Row>
              </Col>
              <Col>
                <div className="float-md-right">
                  <button variant="secondary" size="sm" onClick={this.getNextLine}>Next Line</button>
                </div>
              </Col>
            </Row>
            <Row>
              <Col>
                <Table>
                  <thead>
                  <tr>
                    <th width={150}>Text</th>
                  </tr>
                  </thead>
                  { this.renderTableBody(this.state.dialog) }
                </Table>
              </Col>
            </Row>
          </Col>
          <Col>
            <Row style={{paddingTop:30}}>
              <Col>
                <h4>Existing Recordings</h4>
                <p>X of Y recordings complete</p>
              </Col>
            </Row>
            <Row>
              <Col>
                ...
              </Col>
            </Row>
          </Col>
        </Row>
      </div>
    );
  }
}