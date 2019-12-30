import "./LoaderButton.css";
import React, { Component } from "react";
import {Button} from "react-bootstrap";

export default class LoaderButton extends Component {

    render() {
        return (
            <Button
                className={ `LoaderButton ${ this.props.classname }` }
                disabled={ this.props.disabled || this.props.isLoading }
                type={ this.props.type }
                variant={ this.props.variant }
                size={ this.props.size }
                onClick={this.props.onClick}
            >
                { !this.props.isLoading ? this.props.text : this.props.loadingText }
            </Button>
        )
    }
}
