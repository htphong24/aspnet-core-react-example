import React, { Component } from 'react';
import PropTypes from 'prop-types';
import 'antd/dist/antd.css';
import { Form, Icon, Input, Button, Row, Col } from 'antd';

class ContactAddForm extends Component {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit = evt => {
    evt.preventDefault();
    this.props.form.validateFields((err, values) => {
      if (!err) {
        const submitRequest = Object.assign({}, values); // clone target values
        //TODO: submitRequest.fname, submitRequest.lname, submitRequest.email, submitRequest.phone1
        //contactAdd(submitRequest)
        //  .then(response => {
        //    this.props.history.push("/");
        //  }).catch(error => {
        //    if (error.status === 401) {
        //      this.props.handleLogout('/login', 'error', 'You have been logged out. Please login create poll.');
        //    } else {
        //      notification.error({
        //        message: 'Polling App',
        //        description: error.message || 'Ooops! Something went wrong. Please try again!'
        //      });
        //    }
        //  });
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
            {getFieldDecorator('fname', {
              rules: [{ required: true, message: 'First name required' }],
            })(
              <Input placeholder="First Name" name="fname"/>
            )}
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item>
            {getFieldDecorator('lname', {
              rules: [{ required: true, message: 'Last name required' }],
            })(
              <Input placeholder="Last Name" name="lname"/>
            )}
          </Form.Item>
        </Col>
        <Col span={8}>
          <Form.Item>
            {getFieldDecorator('email', {
              rules: [
                { required: true, message: 'Email required' },
                { type: 'email', message: 'The input is not valid E-mail!' }
              ],
            })(
              <Input placeholder="Email" name="email"/>
            )}
          </Form.Item>
        </Col>
        <Col span={4}>
          <Form.Item>
            {getFieldDecorator('phone1', {
              rules: [{ required: true, message: 'Phone required' }],
            })(
              <Input placeholder="Phone 1" name="phone1"/>
            )}
          </Form.Item>
        </Col>
      </Form>
    );
  }
}

export default ContactAddForm;
