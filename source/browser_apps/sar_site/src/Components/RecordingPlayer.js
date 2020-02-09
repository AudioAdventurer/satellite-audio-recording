import React, { Component } from "react";
import {Row, Col, Button} from "react-bootstrap";
import PropTypes from 'prop-types';
import SarService from "../Services/SarService";
import moment from "moment";
import {Howl} from 'howler';

export default class RecordingPlayer extends Component {

  constructor(props) {
    super(props);

    this.state = {
      blob: null,
    };

    this.playBlob = this.playBlob.bind(this);
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
    let url = URL.createObjectURL(this.state.blob);

    let sound = new Howl({
      src: [url],
      format: ['wav']
    });

    sound.play();
  }

  handleDelete() {
    if (this.props.onDelete !== undefined) {
      this.props.onDelete(this.props.recording);
    }
  }

  render() {
    return (
      <Row style={{paddingTop:10}}>
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
                onClick={this.handlePlay}>
                Play
              </Button>
            </Col>
            <Col md={4}>
              &nbsp;
            </Col>
            <Col md={3}>
              <Button
                variant={"secondary"}
                size={"sm"}
                onClick={this.handleDelete}>
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
