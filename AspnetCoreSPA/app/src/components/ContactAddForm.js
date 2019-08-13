import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Form, Input, Button, Row, Col, Tooltip } from 'antd';
import { addContact } from '../utils/APIUtils';

class ContactAddForm extends Component {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit = evt => {
    evt.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        //const submitRequest = Object.assign({}, values); // clone target values
        let submitRequest = {
          Contact: {
            FirstName: values.txtFirstName,
            LastName: values.txtLastName,
            Email: values.txtEmail,
            Phone1: values.txtPhone1
          }
        };
        addContact(submitRequest)
          .then(response => {
            this.props.onAdd();
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

  render() {
    const { getFieldDecorator } = this.props.form;
    return (
      <Form onSubmit={this.handleSubmit}>
        <Row>
          <Col span={2}></Col>
          <Col span={2}>
            <Tooltip placement="top" title="Add new contact">
              <Button type="primary" htmlType="submit" className="btn btn-primary">Add</Button>
            </Tooltip>
          </Col>
          <Col span={4}>
            {getFieldDecorator('txtFirstName', {
              rules: [{ required: true, message: 'First name required' }],
            })(
              <Input placeholder="First Name" name="txtFirstName"/>
            )}
          </Col>
          <Col span={4}>
            {getFieldDecorator('txtLastName', {
              rules: [{ required: true, message: 'Last name required' }],
            })(
              <Input placeholder="Last Name" name="txtLastName"/>
            )}
          </Col>
          <Col span={8}>
            {getFieldDecorator('txtEmail', {
              rules: [
                { required: true, message: 'Email required' },
                { type: 'email', message: 'The input is not valid E-mail!' }
              ],
            })(
              <Input placeholder="Email" name="txtEmail"/>
            )}
          </Col>
          <Col span={4}>
            {getFieldDecorator('txtPhone1', {
              rules: [{ required: true, message: 'Phone required' }],
            })(
              <Input placeholder="Phone 1" name="txtPhone1"/>
            )}
          </Col>
        </Row>
      </Form>
    );
  }
}

export default ContactAddForm;
