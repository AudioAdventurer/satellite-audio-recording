import React, { Component } from "react";
import {Row, Col, Button, Modal} from "react-bootstrap";
import PropTypes from 'prop-types';
import SarService from "../Services/SarService";
import moment from "moment";
import {Howl} from 'howler';
import { v4 as uuidv4 } from 'uuid';

export default class RecordingPlayer extends Component {

  sound = null;

  constructor(props) {
    super(props);

    this.state = {
      blob: null,
      id: uuidv4(),
      showDeleteModal: false
    };

    this.playBlob = this.playBlob.bind(this);
    this.playStopped = this.playStopped.bind(this);
    this.handleStop = this.handleStop.bind(this);

    this.handleShowDeleteModal = this.handleShowDeleteModal.bind(this);
    this.handleCloseDeleteModal = this.handleCloseDeleteModal.bind(this);
    this.handleDelete = this.handleDelete.bind(this);
  }

  componentDidMount() {

  }

  componentWillUnmount() {

  }

  componentDidUpdate(prevProps ) {
    if (prevProps === undefined
        || prevProps.recording === undefined
        || this.props.recording === undefined){
      return;
    }

    if (this.props.recording !== null
        && prevProps.recording === null) {
      //Going from Null to Not Null

    } else if (this.props.recording === null
               && prevProps.recording !== null) {
      //Going from Not Null to Null
      //Clear the blob
      this.setState({
        blob: null
      });
    } else if (this.props.recording.Id !== prevProps.recording.Id) {
      //The recording changed
      //Clear the blob
      this.setState({
        blob: null
      });
    }
  }

  handlePlay = event => {
    if (this.state.blob === null) {
      let recording = this.props.recording;

      SarService.getRecording(recording.ProjectId, recording.Id)
        .then(r => {
          this.setState({
            blob: r
          }, () => {
            this.playBlob();
          })
        })
        .catch(error => {
          if (error.response.status !== 404) {
            alert(error.message);
          }
        });
    } else {
      this.playBlob()
    }
  };

  playBlob() {
    this.setState({
      playing: true
    }, ()=> {
      let url = URL.createObjectURL(this.state.blob);

      this.sound = new Howl({
        src: [url],
        format: ['wav'],
        onend: this.playStopped
      });

      this.sound.play();
    });
  }

  playStopped() {
    this.setState({
      playing: false
    });
  }

  handleStop() {
    if (this.sound !== null) {
      this.sound.stop();
      this.playStopped();
    }
  }

  handleDelete() {
    this.handleCloseDeleteModal();

    if (this.props.onDelete !== undefined) {
      this.props.onDelete(this.props.recording);
    }
  }

  handleShowDeleteModal() {
    this.setState({
      showDeleteModal: true
    });
  }

  handleCloseDeleteModal() {
    this.setState({
      showDeleteModal:false
    });
  }


  render() {
    return (
      <Row style={{paddingTop:10}}>
        <Modal
          id={this.state.id}
          show={this.state.showDeleteModal}
          onHide={this.handleCloseDeleteModal}>
          <Modal.Header closeButton>
            <Modal.Title>Delete Recording</Modal.Title>
          </Modal.Header>

          <Modal.Body>
            <p>Are you sure that you want to delete this recording?</p>
          </Modal.Body>
          <Modal.Footer>
            <Button
              variant="secondary"
              onClick={this.handleCloseDeleteModal}>
              Close
            </Button>
            <Button
              variant="primary"
              onClick={this.handleDelete}
              >
              Delete
            </Button>
          </Modal.Footer>
        </Modal>
        <Col>
          <Row>
            <Col md={2}>
              {this.props.recording.SequenceNumber}
            </Col>
            <Col md={8}>
              {moment(this.props.recording.RecordedOn).format("L LTS")}
            </Col>
          </Row>
          <Row>
            <Col md={2}>
              &nbsp;
            </Col>
            <Col md={3}>
              <Button
                variant={"secondary"}
                size={"sm"}
                onClick={this.handlePlay}
                disabled={this.state.playing}>
                Play
              </Button>
            </Col>
            <Col md={3}>
              <Button
                variant={"secondary"}
                size={"sm"}
                onClick={this.handleStop}
                disabled={!this.state.playing}>
                Stop
              </Button>
            </Col>
            <Col md={3}>
              <Button
                variant={"secondary"}
                size={"sm"}
                onClick={this.handleShowDeleteModal}>
                Delete
              </Button>
            </Col>
          </Row>
        </Col>
      </Row>
    );
  }
}

RecordingPlayer.propTypes = {
  className: PropTypes.string,
  waveColor: PropTypes.string,
  play: PropTypes.bool,
  onFinish: PropTypes.func
};
