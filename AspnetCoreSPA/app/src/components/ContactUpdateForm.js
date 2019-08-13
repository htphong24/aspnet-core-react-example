import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Row, Col, Tooltip, Input, Form } from 'antd';
import { DATA_SOURCE } from '../constants';

class ContactUpdateForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
      contact: props.contact,
    };

    this.handleUpdate = this.handleUpdate.bind(this);
  }

  handleUpdate = (evt) => {
    console.log("ContactUpdateForm - handleUpdate - evt");
    console.log(evt);
    evt.preventDefault();
    //this.props.form.validateFields((err, values) => {
    //  if (!err) {
    //    //const submitRequest = Object.assign({}, values); // clone target values
    //    let submitRequest = {
    //      Contact: {
    //        FirstName: values.txtFirstName,
    //        LastName: values.txtLastName,
    //        Email: values.txtEmail,
    //        Phone1: values.txtPhone1
    //      }
    //    };
    //    addContact(submitRequest)
    //      .then(response => {
    //        this.props.onAdd();
    //      })
    //      .catch(error => {
    //        this.props.form.setFields({
    //          txtEmail: {
    //            value: values.txtEmail,
    //            errors: [new Error(error.ErrorMessage)],
    //          },
    //        });
    //      });
    //  }
    //});
  }

  render() {
    const { contact } = this.state;
    const { getFieldDecorator } = this.props.form;
    return (
      <Form onSubmit={this.handleUpdate}>
        <Row>
          <Col span={2}>
            {DATA_SOURCE === "sqlserver" || DATA_SOURCE === "mongodb" ? (
              <span>
                <Tooltip placement="top" title="Edit contact">
                  <i className="fas fa-edit" />
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
