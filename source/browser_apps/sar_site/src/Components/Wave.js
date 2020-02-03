import React, { Component } from "react";
import {Row, Col} from "react-bootstrap";
import WaveSurfer from "wavesurfer.js";
import PropTypes from 'prop-types';
import SarService from "../Services/SarService";

export default class Wave extends Component {

  constructor(props) {
    super(props);

    this.state = {
      blob: null,
    };
  }

  componentDidMount() {
    const { waveColor } = this.props;

    this.wavesurfer = WaveSurfer.create({
      container: `#waveform_${this.props.sequenceNumber}`,
      waveColor: waveColor ? waveColor : "#FD9E66"
    });

    this.wavesurfer.on("finish", () => {
      this.wavesurfer.pause();
      this.props.onFinish();
    });

    this.loadData();
  }

  componentWillUnmount() {

  }

  componentDidUpdate(prevProps ) {
    if (this.props.projectId !== prevProps.projectId
        || this.props.recordingId !== prevProps.recordingId) {
      this.wavesurfer.empty();

      this.setState({
        blob: null
      }, () => {
        this.loadData()
      });
    }
  }

  loadData() {
    let projectId = this.props.projectId;
    let recordingId = this.props.recordingId;

    SarService.getRecording(projectId, recordingId)
      .then(r => {
          this.wavesurfer.loadBlob(r);
        })
      .catch(error => {
        if (error.response.status !== 404) {
          alert(error.message);
        }
      });
  }

  render() {
    const cssClass = this.props.className ? this.props.className : "";
    return (
      <Row>
        <Col>
          <Row>
            <Col>
              {this.props.sequenceNumber}
            </Col>
            <Col>
              Play
            </Col>
            <Col>
              Delete
            </Col>
          </Row>
          <Row>
            <Col>
              <div
                id={`waveform_${this.props.sequenceNumber}`}
                className={cssClass}
              />
            </Col>
          </Row>
        </Col>
      </Row>
    );
  }
}

Wave.propTypes = {
  className: PropTypes.string,
  waveColor: PropTypes.string,
  play: PropTypes.bool,
  onFinish: PropTypes.func
};
