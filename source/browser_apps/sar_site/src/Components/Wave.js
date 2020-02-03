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
      container: "#waveform",
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
    if (this.props.play) {
      console.log("play");
      this.wavesurfer.play();
    } else {
      console.log("pausing");
      this.wavesurfer.pause();
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
        alert(error.message);
      });
  }

  render() {
    const cssClass = this.props.className ? this.props.className : "";
    return (
      <Row>
        <Col>
          <Row>
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
                id="waveform"
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
