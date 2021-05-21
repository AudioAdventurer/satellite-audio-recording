import React from "react";
import {Row, Col, Button} from "react-bootstrap";
import SarService from "../Services/SarService";

export default class Scenes extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      scenes:[],
      selectedScene: null
    };
  }


  componentDidMount() {

  }

  componentWillUnmount() {

  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.scenes=== null && this.props.scenes !== null) {
      this.setState({
        scenes: this.props.scenes
      });
    } else if (prevProps.scenes !== null && this.props.scenes === null){
      this.setState({
        scenes:[]
      });
    } else if (prevProps.scenes.length !== this.props.scenes.length){
      this.setState({
        scenes: this.props.scenes
      });
    }
  }

  renderScenes(scenes) {
    if (scenes != null
      && scenes.length > 0) {
      let rows =  scenes.map((item, i) => {
        const line = SarService.getSceneLine(item);

        let style = {
          height: "50px",
          width: "100%",
        };

        if (this.state.selectedScene !== null
            && this.state.selectedScene.Id === item.Id)
        {
          return (
            <Row
              key={i}
              style={{paddingBottom:"5px"}}>
              <Col>
                <Button
                  size="sm"
                  variant="primary"
                  style={style}>
                  {line}
                </Button>
              </Col>
            </Row>
          );
        } else {
          return (
            <Row
              key={i}
              style={{paddingBottom:"5px"}}>
              <Col>
                <Button
                  size="sm"
                  variant="secondary"
                  style={style}>
                  {line}
                </Button>
              </Col>
            </Row>
          );
        }
      });

      return (
        <Row style={{overflow:"scroll",height:"600px"}}>
          <Col>
            {rows}
          </Col>
        </Row>);
    }

    return (<Row></Row>);
  }

  render() {
    return (
      <Row>
        <Col>
          <Row>
            <Col>
              <h4>Scenes</h4>
            </Col>
          </Row>
          { this.renderScenes(this.state.scenes) }
        </Col>
      </Row>
    );
  }
}