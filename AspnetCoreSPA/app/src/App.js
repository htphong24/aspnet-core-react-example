import React, { Component } from 'react';
import { Route, withRouter, Switch } from 'react-router-dom';
import Home from './components/Home';
import Login from './components/Login';

import 'antd/dist/antd.css';
import { Form, Input, Row, Col, Pagination, Layout } from 'antd';
import './App.css';

const { Content } = Layout;

class App extends Component {

    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            currentUser: null
        };

        this.handleLogin = this.handleLogin.bind(this);
        this.loadCurrentUser = this.loadCurrentUser.bind(this);
        this.handleLogout = this.handleLogout.bind(this);
    }

    handleLogin() {
        this.loadCurrentUser();
        this.props.history.push("/");
    }

    loadCurrentUser() {
        getCurrentUser()
            .then(response => {
                this.setState({
                    currentUser: response,
                    isAuthenticated: true
                });
            }).catch(error => {
                // blah
            });
    }

    handleLogout() {
        localStorage.removeItem("accessToken");

        this.setState({
            currentUser: null,
            isAuthenticated: false
        });

        this.props.history.push("/");
    }

    render() {
        return (
            <Switch>
                <Route exact path="/" render={(props) =>
                    <Home isAuthenticated={this.state.isAuthenticated} currentUser={this.state.currentUser} {...props} />}>
                </Route>
                <Route path="/auth/login" render={(props) => <Login onLogin={this.handleLogin} {...props} />}></Route>
            </Switch>
        );
    }
}

export default withRouter(App);
