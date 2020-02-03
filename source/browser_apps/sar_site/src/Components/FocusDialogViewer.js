import React from "react";
import {Row, Col} from "react-bootstrap";

export default class FocusDialogViewer extends React.Component {

  renderDialog(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {
        if (item.LineType === 'Scene') {
          return(
            <Row key={i}>
              <Col style={{
                fontWeight:'bold',
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'Gainsboro'}} >
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
                backgroundColor:'Gainsboro'}}>
                {item.Line}
              </Col>
            </Row>);
        } else if (item.LineType === 'Dialogue') {
          if (item.CharacterDialogId === null) {
            return (
              <Row key={i}>
                <Col style={{
                  backgroundColor: 'Gainsboro',
                  paddingTop: '0px',
                  paddingBottom: '5px',
                  paddingLeft:'100px',
                  paddingRight:'100px'
                }}>
                  {item.Line}
                </Col>
              </Row>);
          } else {
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
          }
        } else if (item.LineType === 'Action') {
          return (
            <Row key={i}>
              <Col style={{
                paddingTop: '5px',
                paddingBottom: '5px',
                backgroundColor:'Gainsboro'}}>
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
                backgroundColor:'Gainsboro'}}>
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
      <Row style={{marginLeft:0, marginRight:0}}>
        { this.renderDialog(this.props.dialog) }
      </Row>
    );
  }
}