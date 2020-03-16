import "./DialogViewer.css";
import React from "react";
import {Row, Col, Button} from "react-bootstrap";

export default class DialogViewer extends React.Component {

  constructor(props) {
    super(props);

    this.state = {
      selectedDialogId:null
    };
  }

  componentDidMount() {
    let dialog = this.props.dialog;

    if (dialog.length> 0) {
      let cd = dialog.find(d => d.CharacterDialogId !== null);

      if (cd !== null) {
        this.setState({
          selectedDialogId: cd.CharacterDialogId
        }, () => {
          this.props.onDialogSelected(cd.CharacterDialogId);
        });
      }
    }
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    if (prevProps.dialog.length === 0
        || prevProps.dialog[0].SequenceNumber !== this.props.dialog[0].SequenceNumber) {
      let dialog = this.props.dialog;

      if (dialog.length> 0) {
        let cd = dialog.find(d => d.CharacterDialogId !== null);

        if (cd !== null) {
          this.setState({
            selectedDialogId: cd.CharacterDialogId
          }, () => {
            this.props.onDialogSelected(cd.CharacterDialogId);
          });
        }
      }
    }
  }

  handleSelect = event => {
    this.setState({
      selectedDialogId: event.target.id
    }, ()=> {
      this.props.onDialogSelected(this.state.selectedDialogId);
    });
  };

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
          if (this.state.selectedDialogId === item.CharacterDialogId) {
            return (
              <Row key={i}>
                <Col md={10} style={{
                  backgroundColor:'white',
                  border:'2px solid black',
                  paddingTop:'20px',
                  paddingBottom:'20px',
                  paddingLeft:'100px',
                  paddingRight:'100px'
                }}>
                  {item.Line}
                </Col>
                <Col md={2}
                     className="DialogViewerSelectColumn"
                >
                  <Button
                    className="DialogViewerSelectButton"
                    id={item.CharacterDialogId}
                    onClick={this.handleSelect}
                  >
                    Select
                  </Button>
                </Col>
              </Row>);
          } else {
            return (
              <Row key={i}>
                <Col md={10} style={{
                  backgroundColor:'Gainsboro',
                  border:'2px solid black',
                  paddingTop:'20px',
                  paddingBottom:'20px',
                  paddingLeft:'100px',
                  paddingRight:'100px'
                }}>
                  {item.Line}
                </Col>
                <Col md={2}
                  className="DialogViewerSelectColumn"
                >
                  <Button
                    className="DialogViewerSelectButton"
                    id={item.CharacterDialogId}
                    onClick={this.handleSelect}
                  >
                    Select
                  </Button>
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
      <Row style={{marginLeft:0, marginRight:0, height:this.props.height, overflow:"auto"}}>
        { this.renderDialog(this.props.dialog) }
      </Row>
    );
  }
}