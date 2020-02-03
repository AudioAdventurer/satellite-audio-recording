import React from "react";
import {Row, Col} from "react-bootstrap";
import Wave from "./Wave";

export default class Recordings extends React.Component {

  componentDidMount() {

  }

  componentWillUnmount() {

  }

  componentDidUpdate(prevProps, prevState, snapshot) {

  }

  renderRecordings(recordings) {
    if (recordings != null
        && recordings.length > 0) {
      let rows =  recordings.map((item, i) => {
        return (
          <Row key={i}>
            <Col>
              <Row>
                <Col>
                  {item.SequenceNumber}
                </Col>
              </Row>
              <Row>
                <Col>
                  <Wave
                    projectId= {item.ProjectId}
                    recordingId= { item.Id }
                  />
                </Col>
              </Row>
            </Col>
          </Row>
        );
      });

      return (
        <Row>
          <Col>
            {rows}
          </Col>
        </Row>);
    }

    return (<Row></Row>);
  }

  render() {
    return (
      <Row style={{paddingTop: 30}}>
        <Col>
          <Row>
            <Col>
              <h4>Existing Recordings</h4>
            </Col>
          </Row>
          { this.renderRecordings(this.props.recordings) }
        </Col>
      </Row>
    );
  }
}