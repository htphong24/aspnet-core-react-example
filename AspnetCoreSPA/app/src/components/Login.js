import React, { Component } from "react";
import "antd/dist/antd.css";
import { Form, Input, Button, Icon, notification } from "antd";
import { getAuth, login } from "../services/authenticationApi";
import { Captcha, captchaSettings } from "reactjs-captcha";
import { BASE_URL } from "../constants";
class Login extends Component {
  constructor(props) {
    super(props);

    // set the captchaEndpoint property to point to the captcha endpoint path on your app's backend
    captchaSettings.set({
      captchaEndpoint: BASE_URL + "/simple-captcha-endpoint.ashx",
      //captchaEndpoint: "simple-captcha-endpoint.ashx",
    });
  }

  render() {
    //console.log("Login > render > this.props:\n", this.props);
    const AntWrappedLoginForm = Form.create()(LoginForm);
    return (
      <div className="login-container">
        <h1 className="page-title">Login</h1>
        <div className="login-content">
          <AntWrappedLoginForm onLogin={this.props.onLogin} />
        </div>
      </div>
    );
  }
}

class LoginForm extends Component {
  constructor(props) {
    super(props);

    this.state = {
      captchaNeeded: false,
    };

    this.handleSubmit = this.handleSubmit.bind(this);
  }

  _isMounted = false;

  initData = async () => {
    //console.log("LoginForm > initData");
    if (this._isMounted) {
      //console.log("LoginForm > initData > _isMounted == true");
      try {
        let response = await getAuth();
        //console.log("LoginForm > initData > response (getAuth):\n", response);
        this.setState({
          captchaNeeded: response.data.CaptchaNeeded,
        });
      } catch (error) {
        this.setState({
          captchaNeeded: true,
        });
      }
    }
  };

  // LIFECYCLE METHODS

  componentDidMount = () => {
    //console.log("LoginForm > componentDidMount");
    this._isMounted = true;
    this.initData();
  };

  componentWillUnmount = () => {
    //console.log("LoginForm > componentWillUnmount");
    this._isMounted = false;
  };

  handleSubmit = (event) => {
    //console.log("LoginForm > handleSubmit");

    event.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        //const submit houstRequest = Object.assign({}, values); // clone target values
        // build submit request
        // console.log("LoginForm > handleSubmit > this.captcha:\n", this.captcha);
        // console.log(
        //   "LoginForm > handleSubmit > this.state.captchaNeeded:\n",
        //   this.state.captchaNeeded
        // );
        const submitRequest = {
          Email: values.email,
          Password: values.password,
          CaptchaNeeded: this.state.captchaNeeded,
          UserEnteredCaptchaCode: this.state.captchaNeeded
            ? this.captcha.getUserEnteredCaptchaCode()
            : null,
          CaptchaId: this.state.captchaNeeded
            ? this.captcha.getCaptchaId()
            : null,
        };

        // console.log("LoginForm > handleSubmit > form.validateFields");
        try {
          let response = await login(submitRequest);
          if (response.data.AuthLogin.Success) {
            this.props.onLogin(response.data.AuthLogin.Email);
          } else {
            notification.error({
              message: "Login failed",
              description: response.data.AuthLogin.Message,
            });

            // if captcha already exists, reload its image
            if (this.state.captchaNeeded) {
              this.captcha.reloadImage();
            }

            // captcha not existed yet? then show it if needed
            if (response.data.CaptchaNeeded)
              this.setState(
                {
                  captchaNeeded: true,
                },
                () => {
                  this.props.form.resetFields("userCaptchaInput");
                }
              );
          }
        } catch (error) {
          notification.error({
            message: "Login error",
            description: error || "Sorry! Something went wrong.",
          });
        }
      }
    });
  };

  render() {
    // console.log("LoginForm > render > this.props:\n", this.props);
    const { getFieldDecorator } = this.props.form;
    const { captchaNeeded } = this.state;
    return (
      <Form onSubmit={this.handleSubmit} className="login-form">
        <Form.Item>
          {getFieldDecorator("email", {
            rules: [
              {
                required: true,
                message: "Please input your username or email!",
              },
            ],
          })(
            <Input
              prefix={<Icon type="user" />}
              size="large"
              name="email"
              placeholder="Username or Email"
            />
          )}
        </Form.Item>
        <Form.Item>
          {getFieldDecorator("password", {
            rules: [{ required: true, message: "Please input your Password!" }],
          })(
            <Input
              prefix={<Icon type="lock" />}
              size="large"
              name="password"
              type="password"
              placeholder="Password"
            />
          )}
        </Form.Item>
        {captchaNeeded && (
          <Form.Item>
            <Captcha
              captchaStyleName="reactFormCaptcha"
              ref={(captcha) => {
                this.captcha = captcha;
              }}
            />
          </Form.Item>
        )}
        {captchaNeeded && (
          <Form.Item>
            {getFieldDecorator("userCaptchaInput", {
              rules: [
                {
                  required: true,
                  message: "Please input characters from the picture!",
                },
              ],
            })(
              <Input
                prefix={<Icon type="robot" />}
                size="large"
                name="userCaptchaInput"
                id="userCaptchaInput"
                placeholder="Retype the chars long from the picture"
              />
            )}
          </Form.Item>
        )}
        <Form.Item>
          <Button
            type="primary"
            htmlType="submit"
            size="large"
            className="login-form-button"
          >
            Login
          </Button>
        </Form.Item>
      </Form>
    );
  }
}

export default Login;
