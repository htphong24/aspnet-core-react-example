import React, { Component } from "react";
import "antd/dist/antd.css";
import { Button, Col, Form, Input, Popconfirm, Row, Tooltip } from "antd";
import { addContact, reloadContacts } from "../services/contactsApi";

class ContactAddForm extends Component {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleReload = this.handleReload.bind(this);
  }

  handleSubmit = (evt) => {
    evt.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          //const submitRequest = Object.assign({}, values); // clone target values
          let submitRequest = {
            Contact: {
              FirstName: values.txtFirstName,
              LastName: values.txtLastName,
              Email: values.txtEmail,
              Phone1: values.txtPhone1,
            },
          };
          const response = await addContact(submitRequest);
          //console.log("ContactAddForm > handleSubmit > response:\n", response);
          this.props.onAdded();
        } catch (error) {
          // console.log(
          //   "ContactAddForm > handleSubmit > error (catch):\n",
          //   error
          // );
          this.props.form.setFields({
            txtEmail: {
              value: values.txtEmail,
              errors: [new Error(error)],
            },
          });
        }
      }
    });
  };

  handleReload = async (evt) => {
    try {
      await reloadContacts();
      this.props.onReloaded();
    } catch (error) {}
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    return (
      <Form onSubmit={this.handleSubmit}>
        <Row>
          <Col span={2}>
            <Popconfirm
              title="Are you sure you want to reload all contacts data?"
              onConfirm={this.handleReload}
              okText="Yes"
              cancelText="No"
            >
              <Tooltip placement="top" title="Reload all contacts">
                <Button type="danger" className="btn btn-danger">
                  Reload
                </Button>
              </Tooltip>
            </Popconfirm>
          </Col>
          <Col span={2}>
            <Tooltip placement="top" title="Add new contact">
              <Button
                type="primary"
                htmlType="submit"
                className="btn btn-primary"
              >
                Add
              </Button>
            </Tooltip>
          </Col>
          <Col span={4}>
            <Form.Item>
              {getFieldDecorator("txtFirstName", {
                rules: [{ required: true, message: "First name required" }],
              })(<Input placeholder="First Name" name="txtFirstName" />)}
            </Form.Item>
          </Col>
          <Col span={4}>
            <Form.Item>
              {getFieldDecorator("txtLastName", {
                rules: [{ required: true, message: "Last name required" }],
              })(<Input placeholder="Last Name" name="txtLastName" />)}
            </Form.Item>
          </Col>
          <Col span={8}>
            <Form.Item>
              {getFieldDecorator("txtEmail", {
                rules: [
                  { required: true, message: "Email required" },
                  { type: "email", message: "The input is not valid E-mail!" },
                ],
              })(<Input placeholder="Email" name="txtEmail" />)}
            </Form.Item>
          </Col>
          <Col span={4}>
            <Form.Item>
              {getFieldDecorator("txtPhone1", {
                rules: [{ required: true, message: "Phone required" }],
              })(<Input placeholder="Phone 1" name="txtPhone1" />)}
            </Form.Item>
          </Col>
        </Row>
      </Form>
    );
  }
}

export default ContactAddForm;
