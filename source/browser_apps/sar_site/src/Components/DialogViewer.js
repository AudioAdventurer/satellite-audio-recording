import React from "react";
import {Row, Col} from "react-bootstrap";

export default class DialogViewer extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      dialog:[]
    };
  }

  componentDidMount() {

  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.dialog.length !== this.props.dialog.length) {
      this.setState({
        dialog: this.props.dialog
      })

      return;
    }

    if (this.props.dialog.length > 0
        && prevProps.dialog.length > 0
        && this.props.dialog[0].Line !== prevProps.dialog[0].Line) {
      this.setState({
        dialog: this.props.dialog
      })
    }
  }

  renderDialog(list) {
    if (list !== null
      && list.length > 0) {
      let rows =  list.map((item, i) => {
        if (item.LineType === 'Scene') {
          return(
            <Row key={i}>
              <Col style={{
                fontWeight:'bold',
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'white'}} >
                {item.Line}
              </Col>
            </Row>);
        } else if (item.LineType === 'Character') {
          return(
            <Row key={i}>
              <Col style={{
                textAlign:'center',
                paddingTop: '5px',
                paddingBottom: '0px',
                backgroundColor:'white'}}>
                {item.Line}
              </Col>
            </Row>);
        } else if (item.LineType === 'Dialogue') {
            return (
              <Row key={i}>
                <Col style={{
                  backgroundColor:'white',
                  border:'2px solid black',
                  paddingTop:'20px',
                  paddingBottom:'20px',
                  paddingLeft:'100px',
                  paddingRight:'100px'
                }}>
                  {item.Line}
                </Col>
              </Row>);
        } else if (item.LineType === 'Action') {
          return (
            <Row key={i}>
              <Col style={{
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'white'}}>
                {item.Line}
              </Col>
            </Row>);
        } else if (item.LineType === 'Parenthetical') {
          return (
            <Row key={i}>
              <Col style={{
                paddingLeft: '200px',
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'white'}}>
                ({item.Line})
              </Col>
            </Row>);
        } else {
          console.info(item);

          return (<Row key={i}/>)
        }
      });

      return(
        <Col>
          {rows}
        </Col>);
    }

    return(
      <Col/>);
  }

  render() {
    return (
      <Row style={{marginLeft:0, marginRight:0, height:this.props.height}}>
        <Col>
          <Row>
            <Col>
              <h4>Dialog</h4>
            </Col>
          </Row>
          <Row>
            { this.renderDialog(this.state.dialog) }
          </Row>
        </Col>
      </Row>
    );
  }
}