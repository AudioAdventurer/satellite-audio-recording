import React from "react";
import {Row, Col, Table} from "react-bootstrap";

export default class RecordingInstructions extends React.Component {

  render() {
    return (
      <Row>
        <Col>
          <Row>
            <Col>
              <h5>Recording Instructions</h5>
            </Col>
          </Row>
          <Row>
            <Col>
              <Table striped bordered hover>
                <thead>
                  <tr>
                    <th>Button</th>
                    <th>Action</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td>Space</td>
                    <td>Start recording - Recording will automatically stop when you stop talking</td>
                  </tr>
                  <tr>
                    <td>Escape</td>
                    <td>Cancel a recording</td>
                  </tr>
                </tbody>
              </Table>
            </Col>
          </Row>
        </Col>
      </Row>
    )
  }

}