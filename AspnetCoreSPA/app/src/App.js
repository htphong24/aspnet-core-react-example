import React, { Component } from 'react';
import { Route, withRouter, Switch } from 'react-router-dom';
import MainApp from './components/MainApp';
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
  }

  render() {
    return (
      <Layout className="app-container">
        <Content>
          <div className="container">
            <Switch>
              <Route exact path="/" render={(props) =>
                <MainApp isAuthenticated={this.state.isAuthenticated} currentUser={this.state.currentUser} {...props} />}>
              </Route>
              <Route path="/auth/login" render={(props) => <Login onLogin={this.handleLogin} {...props} />}></Route>
            </Switch>
          </div>
        </Content>
      </Layout>
    );
  }
}

export default withRouter(App);
