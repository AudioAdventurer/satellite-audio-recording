import React from "react";
import {Row, Col} from "react-bootstrap";
import RecordingPlayer from "./RecordingPlayer";
import SarService from "../Services/SarService";

export default class Recordings extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      recordings:[]
    };

    this.loadRecordings = this.loadRecordings.bind(this);
    this.handleDelete = this.handleDelete.bind(this);
  }


  componentDidMount() {
    this.loadRecordings(this.props.projectId, this.props.dialogId);
  }

  componentWillUnmount() {

  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.projectId !== this.props.projectId
        || prevProps.dialogId !== this.props.dialogId
        || prevProps.recordingsTimestamp !== this.props.recordingsTimestamp) {
      this.loadRecordings(this.props.projectId, this.props.dialogId);
    }
  }

  loadRecordings(projectId, dialogId) {
    SarService.getRecordings(projectId, dialogId)
      .then(r=> {
        this.setState({
          recordings: r
        });
      })
      .catch(error => {
        alert(error.message);
      });
  }

  handleDelete(item) {
    SarService.deleteRecording(item.ProjectId, item.Id)
      .then(r => {
        this.loadRecordings(this.props.projectId, this.props.dialogId);
      })
      .catch(error => {
        alert(error.message);
      });
  }

  renderRecordings(recordings) {
    if (recordings != null
        && recordings.length > 0) {
      let rows =  recordings.map((item, i) => {
        return (
          <Row key={i}>
            <Col>
              <RecordingPlayer
                recording={item}
                onDelete={this.handleDelete}
              />
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
          { this.renderRecordings(this.state.recordings) }
        </Col>
      </Row>
    );
  }
}