import "./Scenes.css";
import React from "react";
import {Row, Col, Table, Button} from "react-bootstrap";

export default class Scenes extends React.Component {

  handleClick = event => {
    this.props.onClick(event.target.id);
  };


  renderScenes(recordings) {
    if (recordings != null
      && recordings.length > 0) {
      let rows =  recordings.map((item, i) => {
        return (
          <tr key={i}>
            <td>
              <Button
                className="SelectSceneButton"
                id={i}
                size="sm"
                onClick={this.handleClick}
              >
                {item.SequenceNumber}
              </Button>
            </td>
            <td>
              {item.InteriorExterior} {item.Location} {item.TimeOfDay}
            </td>
          </tr>
        );
      });

      return (
        <tbody>
          {rows}
        </tbody>);
    }

    return (<tbody></tbody>);
  }

  render() {
    return (
      <Row style={{paddingTop: 30}}>
        <Col>
          <Row>
            <Col>
              <h4>Scenes</h4>
            </Col>
          </Row>
          <Row style={{height:this.props.height - 30, overflow:"auto"}}>
            <Col>
              <Table>
                <thead>
                <tr>
                  <th>Number</th>
                  <th>Scene</th>
                </tr>
                </thead>
                { this.renderScenes(this.props.scenes) }
              </Table>
            </Col>
          </Row>
        </Col>
      </Row>
    );
  }
}