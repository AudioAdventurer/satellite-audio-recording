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
                <p>A tool for creating Audio Drama when actors are remote.</p>
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