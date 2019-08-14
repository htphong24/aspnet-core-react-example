import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Row, Col, Tooltip, Input, Form } from 'antd';
import { DATA_SOURCE } from '../constants';
import { updateContact } from '../utils/APIUtils';

class ContactUpdateForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
      contact: props.contact,
    };

    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  handleSubmit = (evt, id) => {
    evt.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        //const submitRequest = Object.assign({}, values); // clone target values
        let submitRequest = {
          Contact: {
            Id: id,
            FirstName: values.txtUpdateFirstName,
            LastName: values.txtUpdateLastName,
            Email: values.txtUpdateEmail,
            Phone1: values.txtUpdatePhone1
          }
        };
        updateContact(submitRequest)
          .then(response => {
            this.props.onUpdated(response.Contact);
          })
          .catch(error => {
            this.props.form.setFields({
              txtEmail: {
                value: values.txtEmail,
                errors: [new Error(error.ErrorMessage)],
              },
            });
          });
      }
    });
  }

  handleCancel = (evt, id) => {
    this.props.onCancel();
  }

  render() {
    const { contact } = this.state;
    const { getFieldDecorator } = this.props.form;
    return (
      <Form onSubmit={(evt) => this.handleSubmit(evt, contact.Id)}>
        <Row>
          <Col span={2}>
            {DATA_SOURCE === "sqlserver" || DATA_SOURCE === "mongodb" ? (
              <span>
                <Tooltip placement="top" title="Save contact">
                  <button type="submit" name="btnSave">
                    <i className="fas fa-save" />
                  </button>
                </Tooltip>
                <Tooltip placement="top" title="Cancel editing">
                  <i className="fas fa-times" onClick={this.handleCancel} />
                </Tooltip>
              </span>
            ) : (
              <span>N/A</span>
            )}
          </Col>
          <Col span={2}>{contact.Id}</Col>
          <Col span={4}>
            {getFieldDecorator('txtUpdateFirstName', {
              rules: [{ required: true, message: 'First name required' }],
              initialValue: `${contact.FirstName}`
            })(
              <Input placeholder="First Name" name="txtUpdateFirstName" />
            )}
          </Col>
          <Col span={4}>
            {getFieldDecorator('txtUpdateLastName', {
              rules: [{ required: true, message: 'Last name required' }],
              initialValue: `${contact.LastName}`
            })(
              <Input placeholder="Last Name" name="txtUpdateLastName" />
            )}
          </Col>
          <Col span={8}>
            {getFieldDecorator('txtUpdateEmail', {
              rules: [
                { required: true, message: 'Email required' },
                { type: 'email', message: 'The input is not valid E-mail!' }
              ],
              initialValue: `${contact.Email}`
            })(
              <Input placeholder="Email" name="txtUpdateEmail" />
            )}
          </Col>
          <Col span={4}>
            {getFieldDecorator('txtUpdatePhone1', {
              rules: [{ required: true, message: 'Phone 1 required' }],
              initialValue: `${contact.Phone1}`
            })(
              <Input placeholder="Phone 1" name="txtUpdatePhone1" />
            )}
          </Col>
        </Row>
      </Form>
    );
  }
}

export default ContactUpdateForm;
