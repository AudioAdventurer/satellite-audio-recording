import "./Participants.css";
import React, {Component} from "react";
import SarService from "../Services/SarService";
import {Row, Col, Table} from "react-bootstrap";
import { FaPlusCircle } from "react-icons/fa";
import { Link } from 'react-router-dom'

export default class Users extends Component {
  constructor(props) {
    super(props);

    this.state = {
      users: []
    };

    this.loadUsers = this.loadUsers.bind(this);
    this.renderTableBody = this.renderTableBody.bind(this);
  }

  componentDidMount() {
    this.loadUsers();
  }

  loadUsers() {
    SarService.getUsers()
      .then(r => {
        this.setState({
          users: r
        });
      })
      .catch(e => {
        alert(e.message);
      });
  }

  renderTableBody(list) {
    if (list != null
      && list.length > 0) {
      let rows =  list.map((item, i) => {
        let url = `/users/${item.UserId}`;
        let name = `${item.GivenName} ${item.FamilyName}`.trim();

        return (
          <tr key={item.UserId}>
            <td>
              <Link to={url}>
                {item.Email}
              </Link>
            </td>
            <td>{name}</td>
            <td>{item.UserType}</td>
          </tr>
        );
      });

      return (
        <tbody>
        {rows}
        </tbody>);
    }

    return(
      <tbody></tbody>);
  }

  render() {
    return (
      <div className="Users">
        <Row>
          <Col>
            <h3>Users</h3>
          </Col>
          <Col>
            <div className="float-md-right">
              <Link to={`/users/new`}>
                <FaPlusCircle/>
              </Link>
            </div>
          </Col>
        </Row>
        <Row>
          <Col>
            <Table striped bordered hover>
              <thead>
                <tr>
                  <th>Email</th>
                  <th>Name</th>
                  <th>Access</th>
                </tr>
              </thead>
              { this.renderTableBody(this.state.users) }
            </Table>
          </Col>
        </Row>
      </div>);
  }
}