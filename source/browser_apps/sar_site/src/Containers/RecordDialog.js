import "./RecordDialog.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Button} from "react-bootstrap";
import { ReactMic as Visualizer } from "react-mic";
import hark from "hark";
import FocusDialogViewer from "../Components/FocusDialogViewer";
import Recorder from "../Components/Recorder";
import RecordingInstructions from "../Components/RecordingInstructions";
import Recordings from "../Components/Recordings";

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
      cancelled: false,
      shouldRecord: false,
      blob: null,
      savingBlob: false,
      dialog:[],
      recordings:[]
    };

    this.loadDialogContext = this.loadDialogContext.bind(this);

    this.getPreviousLine = this.getPreviousLine.bind(this);
    this.getNextLine = this.getNextLine.bind(this);
    this.processBlob = this.processBlob.bind(this);
  }

  componentDidMount() {
    document.addEventListener("keydown", this.handleKeyDown, false);
    this.loadDialogContext(this.state.projectId, this.state.dialogId);
                              }

  componentWillUnmount() {
    document.removeEventListener("keydown", this.handleKeyDown, false);
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
          previousDialogId: previous,
          shouldRecord: false,
          blob: null,
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  getNextLine() {
    if (this.state.nextDialogId !== null
        && !this.state.savingBlob) {
      let url = `/projects/${this.state.projectId}/dialog/${this.state.nextDialogId}/record`;
      this.props.history.push(url);
    }
  }

  getPreviousLine() {
    if (this.state.previousDialogId !== null
        && !this.state.savingBlob) {
      let url = `/projects/${this.state.projectId}/dialog/${this.state.previousDialogId}/record`;
      this.props.history.push(url);
    }
  }

  recordHandler = () => {
    setTimeout(() => {
      this.setState((state, props) => {
        return {
          cancelled: false,
          shouldRecord: true,
          play: false
        };
      });
    }, 500);
  };

  processBlob = blob => {
    if (!this.state.cancelled) {
      this.setState({
        blob: blob,
        savingBlob: true,
      }, () => {
        let data = new FormData();
        data.append('file', blob);

        SarService.saveRecording(this.state.projectId, this.state.dialogId, data)
          .then(r => {
            this.setState({
              savingBlob:false
            });
          })
          .catch(e=>{
            alert(e.message);
            this.setState({
              savingBlob:false
            });
          });
      });
    }
  };

  handleKeyDown = event => {
    // space bar code
    if (event.keyCode === 32) {
      if (!this.state.shouldRecord) {
        event.preventDefault();
        this.recordHandler();
      }
    }

    // esc key code
    if (event.keyCode === 27) {
      event.preventDefault();

      this.setState({
        cancelled: true,
        shouldRecord: false,
        blob: null,
      });
    }

    // previous line of dialog
    if (event.keyCode === 37) {
      if (!this.state.play) {
        this.getPreviousLine();
      }
    }

    // next line of dialog
    if (event.keyCode === 39) {
      if (!this.state.play) {
        this.getNextLine();
      }
    }
  };

  silenceDetection = stream => {
    const options = {
      interval: "150",
      threshold: -80
    };
    const speechEvents = hark(stream, options);

    speechEvents.on("stopped_speaking", () => {
      this.setState({
        shouldRecord: false
      });
    });
  };

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
                    disabled={this.state.previousDialogId === null
                              || this.state.savingBlob}
                    onClick={this.getPreviousLine}>Previous Line</Button>
                </div>
              </Col>
              <Col>
                <Row>
                  <Col>
                    <Visualizer
                      className="wavedisplay"
                      record={this.state.shouldRecord}
                      backgroundColor={"#222222"}
                      strokeColor={"#FD9E66"}
                    />
                    <Recorder
                      command={this.state.shouldRecord ? "start" : "stop"}
                      onStop={this.processBlob}
                      gotStream={this.silenceDetection}
                    />
                  </Col>
                </Row>
              </Col>
              <Col>
                <div className="float-md-right">
                  <Button
                    variant="secondary"
                    size="sm"
                    disabled={this.state.nextDialogId === null
                              || this.state.savingBlob }
                    onClick={this.getNextLine}>Next Line</Button>
                </div>
              </Col>
            </Row>
            <FocusDialogViewer
              dialog={this.state.dialog}
            />
          </Col>
          <Col md={3}>
            <RecordingInstructions />
            <Recordings
              projectId={this.state.projectId}
              dialogId={this.state.dialogId}
            />
          </Col>
        </Row>
      </div>
    );
  }
}