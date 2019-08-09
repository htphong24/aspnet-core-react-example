import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Form, Input, Button, Col } from 'antd';
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
            First: values.txtFirstName,
            Last: values.txtLastName,
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
        <Col span={2}></Col>
        <Col span={2}>
          <Form.Item >
            <Button type="primary" htmlType="submit" className="btn btn-primary">Add</Button>
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item>
            {getFieldDecorator('txtFirstName', {
              rules: [{ required: true, message: 'First name required' }],
            })(
              <Input placeholder="First Name" name="txtFirstName"/>
            )}
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item>
            {getFieldDecorator('txtLastName', {
              rules: [{ required: true, message: 'Last name required' }],
            })(
              <Input placeholder="Last Name" name="txtLastName"/>
            )}
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item>
            {getFieldDecorator('txtEmail', {
              rules: [
                { required: true, message: 'Email required' },
                { type: 'email', message: 'The input is not valid E-mail!' }
              ],
            })(
              <Input placeholder="Email" name="txtEmail"/>
            )}
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item>
            {getFieldDecorator('txtPhone1', {
              rules: [{ required: true, message: 'Phone required' }],
            })(
              <Input placeholder="Phone 1" name="txtPhone1"/>
            )}
          </Form.Item>
        </Col>
      </Form>
    );
  }
}

export default ContactAddForm;
