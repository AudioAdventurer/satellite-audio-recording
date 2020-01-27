import "./RecordDialog.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Button} from "react-bootstrap";

export default class RecordDialog extends Component {
  constructor(props) {
    super(props);

    let projectId = this.props.match.params.projectId;
    let dialogId = this.props.match.params.dialogId;

    this.state = {
      projectId: projectId,
      dialogId: dialogId,
      currentDialog: null,
      nextDialogId: null,
      previousDialogId: null,
      dialog:[]
    };

    this.loadDialogContext = this.loadDialogContext.bind(this);
    this.getPreviousLine = this.getPreviousLine.bind(this);
    this.getNextLine = this.getNextLine.bind(this);
  }

  componentDidMount() {
    this.loadDialogContext(this.state.projectId, this.state.dialogId);
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    let dialogId = this.props.match.params.dialogId;

    if (dialogId !== this.state.dialogId) {
      this.setState({
        dialogId: dialogId
      }, () => {
        this.loadDialogContext(this.state.projectId, this.state.dialogId);
      });
    }
  }

  loadDialogContext(projectId, dialogId) {
    SarService.getDialogContext(projectId, dialogId)
      .then(r => {
        let dialog = r.Context;
        let previous = r.PreviousLine;
        let next = r.NextLine;

        let currentDialog = null;

        dialog.forEach( line => {
            if (line.CharacterDialogId !== null
                && line.CharacterDialogId === dialogId) {
              currentDialog = line;
            }
        });

        this.setState({
          dialog: dialog,
          currentDialog: currentDialog,
          nextDialogId: next,
          previousDialogId: previous
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  getNextLine() {
    let url = `/projects/${this.state.projectId}/dialog/${this.state.nextDialogId}/record`;
    this.props.history.push(url);
  }

  getPreviousLine() {
    let url = `/projects/${this.state.projectId}/dialog/${this.state.previousDialogId}/record`;
    this.props.history.push(url);
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {

        if (item.LineType === 'Scene') {
          return(
            <Row key={i}>
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
            <Row key={i}>
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
              <Row key={i}>
                <Col style={{
                  backgroundColor: 'Gainsboro',
                  paddingTop: '0px',
                  paddingBottom: '5px',
                  paddingLeft:'100px',
                  paddingRight:'100px'
                }}>
                  {item.Line}
                </Col>
              </Row>);
          } else {
            return (
              <Row key={i}>
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
            <Row key={i}>
              <Col style={{
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'Gainsboro'}}>
                {item.Line}
              </Col>
            </Row>);
        } else if (item.lineType === 'Parenthetical') {
          return (
            <Row key={i}>
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
        <div>
          {rows}
        </div>);
    }

    return(
      <div/>);
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
            <Row style={{paddingTop:10, paddingBottom:10}}>
              <Col>
                <div className="float-md-left">
                  <Button
                    variant="secondary"
                    size="sm"
                    disabled={this.state.previousDialogId === null}
                    onClick={this.getPreviousLine}>Previous Line</Button>
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
                  <Button
                    variant="secondary"
                    size="sm"
                    disabled={this.state.nextDialogId === null}
                    onClick={this.getNextLine}>Next Line</Button>
                </div>
              </Col>
            </Row>
            <Row>
              <Col>
                { this.renderTableBody(this.state.dialog) }
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