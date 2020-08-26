import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Form, Input, Button, Icon, notification } from 'antd';
import { login } from '../utils/APIUtils';
import { Captcha, captchaSettings } from 'reactjs-captcha';

class Login extends Component {
    constructor(props) {
        super(props);

        // set the captchaEndpoint property to point to the captcha endpoint path on your app's backend
        captchaSettings.set({
            captchaEndpoint: 'simple-captcha-endpoint.ashx'
        });
    }

    render() {
        const AntWrappedLoginForm = Form.create()(LoginForm)
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

    handleSubmit(event) {
        event.preventDefault();
        this.props.form.validateFields((err, values) => {
            if (!err) {
                //const submit houstRequest = Object.assign({}, values); // clone target values

                // build submit request
                let submitRequest = {
                    Email: values.email,
                    Password: values.password,
                    CaptchaId: values.captchaInput,
                    CaptchaNeeded: this.state.captchaNeeded,
                    UserEnteredCaptchaCode: this.state.captchaNeeded ? this.captcha.getUserEnteredCaptchaCode() : null,
                    CaptchaId: this.state.captchaNeeded ? this.captcha.getCaptchaId() : null
                };
                login(submitRequest)
                    .then(response => {
                        localStorage.setItem("accessToken", response.AccessToken);
                        this.props.onLogin();
                    }).catch(error => {
                        //console.log("Login Form - handleSubmit - error");
                        //console.log(error);
                        if (error.ErrorCode === 401) {
                            notification.error({
                                message: 'Contacts Management',
                                description: 'Your Username or Password is incorrect. Please try again!'
                            });
                        } else {
                            notification.error({
                                message: 'Contacts Management',
                                description: error.ErrorMessage || 'Sorry! Something went wrong. Please try again!'
                            });
                            if (this.state.captchaNeeded)
                                this.captcha.reloadImage();
                        }
                        this.setState({
                            captchaNeeded: true,
                        });
                    });
            }
        });
    }

    render() {
        const { getFieldDecorator } = this.props.form;
        const { captchaNeeded } = this.state;
        return (
            <Form onSubmit={this.handleSubmit} className="login-form">
                <Form.Item>
                    {getFieldDecorator('email', {
                        rules: [{ required: true, message: 'Please input your username or email!' }],
                    })(
                        <Input prefix={<Icon type="user" />} size="large" name="email" placeholder="Username or Email" />
                    )}
                </Form.Item>
                <Form.Item>
                    {getFieldDecorator('password', {
                        rules: [{ required: true, message: 'Please input your Password!' }],
                    })(
                        <Input prefix={<Icon type="lock" />} size="large" name="password" type="password" placeholder="Password" />
                    )}
                </Form.Item>
                {captchaNeeded &&
                    <Form.Item>
                        <Captcha captchaStyleName="reactFormCaptcha" ref={(captcha) => { this.captcha = captcha; }} />
                    </Form.Item>
                }
                {captchaNeeded &&
                    <Form.Item>
                        {getFieldDecorator('userCaptchaInput', {
                            rules: [{ required: true, message: 'Please input characters from the picture!' }],
                        })(
                            <Input prefix={<Icon type="robot" />} size="large" name="userCaptchaInput" id="userCaptchaInput" placeholder="Retype the chars long from the picture" />
                        )}
                    </Form.Item>
                }
                <Form.Item>
                    <Button type="primary" htmlType="submit" size="large" className="login-form-button">Login</Button>
                    </Form.Item>
                    
            </Form>
        );
    }
}

export default Login;