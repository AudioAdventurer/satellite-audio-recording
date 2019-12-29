import "./Home.css";
import React, { Component } from "react";
import {Row, Col} from "react-bootstrap";
import SarService from "../Services/SarService";
import { Redirect } from 'react-router-dom'

export default class Home extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isLoading: true,
            redirectToSetup:false
        };

        SarService.isSetup()
          .then(r => {
              if (!r.IsSetup) {
                  this.setState({
                      redirectToSetup:true
                  });
              }
          }).catch(e => {

        });
    }

    renderLander() {
      return (
        <div className="lander">
          <h1>Satellite Audio Recorder</h1>
          <p>This is a tool designed to assist with creating Audio Drama when actors are primarily in remote locations.
            It manages collection of one or more takes of each line in the script.  Feed back can be supplied,
            and additional takes can be requested on specific lines.  It allows the audio engineer to specify a file
            naming format and extract all audio files into a standardized folder structure.
          </p>
          <p>While this tool can be used for editing scripts, it is not intended to replace other tools that are focused
            on script writing.  Tracking the audio recordings back to specific lines causes more structure to be forced
            onto the script, which would make it less fluid for writing.  Once your script is ready for actors to record
            the audio the script can be loaded in a Fountain.io format.
          </p>
        </div>
      );
    }

    renderProjects() {
        return (
            <div className="HomePanel">
                <Row>
                    <Col>
                    </Col>
                </Row>
                <Row>
                    <Col>
                    </Col>
                </Row>
                <Row>
                    <Col>
                    </Col>
                </Row>
            </div>
        );
    }

    render() {
        if (this.state.redirectToSetup)         {
            return(<Redirect to='/setup'/>)
        } else {
            return (
              <div className="Home">
                  { this.props.isAuthenticated ? this.renderProjects() : this.renderLander()}
              </div>);
        }
    }
}