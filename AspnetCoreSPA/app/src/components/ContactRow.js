import React, { Component } from 'react';
import PropTypes from 'prop-types';
import 'antd/dist/antd.css';
import { Row, Col, Tooltip, Input, Form } from 'antd';
import { DATA_SOURCE } from '../constants';
import ContactUpdateForm from './ContactUpdateForm';

class ContactRow extends Component {
  constructor(props) {
    super(props);

    this.state = {
      contact: props.contact,
      mode: "Read" // Reading/Editing
    };

    this.handleEdit = this.handleEdit.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  handleEdit = (evt, id) => {
    this.setState({
      mode: "Edit"
    });
  }

  handleCancel = (evt) => {
    this.setState({
      mode: "Read"
    });
  }

  render() {
    const { contact, mode } = this.state;
    const MyContactUpdateForm = Form.create()(ContactUpdateForm);

    return (
      mode === "Read"
      ? <Row>
          <Col span={2}>
            {DATA_SOURCE === "sqlserver" || DATA_SOURCE === "mongodb" ? (
              <span>
                <Tooltip placement="top" title="Edit contact">
                  <i className="fas fa-edit" onClick={(evt) => this.handleEdit(evt, contact.Id)} />
                </Tooltip>
                <Tooltip placement="top" title="Delete contact">
                  <i className="fas fa-trash" />
                </Tooltip>
              </span>
            ) : (
                <span>N/A</span>
              )}
          </Col>
          <Col span={2}>{contact.Id}</Col>
          <Col span={4}>{contact.FirstName}</Col>
          <Col span={4}>{contact.LastName}</Col>
          <Col span={8}>{contact.Email}</Col>
          <Col span={4}>{contact.Phone1}</Col>
        </Row>

      : <MyContactUpdateForm contact={contact} onCancel={this.handleCancel}/>
        
    );
  }
}

/*
const ContactRow = props => {
  console.log("ContactRow - props");
  console.log(props);
  const {
    Id = 0,
    FirstName = '',
    LastName = '',
    Email = '',
    Phone1 = ''
  } = props.contact || {};

  return (
    <Row>
      <Col span={2}>
      {DATA_SOURCE === "sqlserver" || DATA_SOURCE === "mongodb" ? (
        <span>
          <Tooltip placement="top" title="Edit contact">
            <i className="fas fa-edit" value={Id} onClick={props.onUpdate}/>
          </Tooltip>
          <Tooltip placement="top" title="Delete contact">
            <i className="fas fa-trash" />
          </Tooltip>
        </span>
        ) : (
        <span>N/A</span>
      )}
      </Col>
      <Col span={2}>{Id}</Col>
      <Col span={4}>{FirstName}</Col>
      <Col span={4}>{LastName}</Col>
      <Col span={8}>{Email}</Col>
      <Col span={4}>{Phone1}</Col>
    </Row>
  );
}
*/
ContactRow.propTypes = {
  contact: PropTypes.shape({
    Id: PropTypes.number,
    FirstName: PropTypes.string,
    LastName: PropTypes.string,
    Email: PropTypes.string, 
    Phone1: PropTypes.string
  }).isRequired
};

export default ContactRow;
