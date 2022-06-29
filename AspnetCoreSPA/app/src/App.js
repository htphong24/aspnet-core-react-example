import React, { Component } from "react";
import { Route, withRouter, Switch } from "react-router-dom";
import Home from "./components/Home";
import Login from "./components/Login";
import "antd/dist/antd.css";
import "./App.css";
import { notification } from "antd";
import { getMe } from "./services/meApi";
import { logout } from "./services/authenticationApi";

class App extends Component {
  constructor(props) {
    super(props);

    this.state = {
      me: null,
    };

    this.handleLogin = this.handleLogin.bind(this);
    this.loadMe = this.loadMe.bind(this);
    this.handleLogout = this.handleLogout.bind(this);
  }

  _isMounted = false;

  handleLogin = async () => {
    //console.log("App > handleLogin");
    await this.loadMe();
    this.props.history.push("/");
  };

  loadMe = async () => {
    //console.log("App > loadMe");
    if (this._isMounted) {
      if (this.props.me === undefined || this.props.me == null) {
        try {
          let response = await getMe();
          //console.log("App > loadMe > getMe response:\n", response);
          this.setState(
            {
              me: response.data.User,
            },
            () => {
              if (["/auth/login"].includes(window.location.pathname))
                this.props.history.push("/");
            }
          );
        } catch (error) {
          //console.log("App > loadMe > error:\n", error);
          notification.error({
            message: "Load current user",
            description: error || "Sorry! Something went wrong.",
          });
        }
      }
    }
  };

  handleLogout = async () => {
    //console.log("App > handleLogout > this.state:\n", this.state);
    try {
      const logoutRequest = {
        Email: this.state.me.Email,
      };
      await logout(logoutRequest);
      this.setState(
        {
          me: null,
        },
        () => {
          this.props.history.push("/auth/login");
        }
      );
    } catch (error) {
      //console.log("App > handleLogout > error (catch):\n", error);
      notification.error({
        message: "Log Out",
        description: error || "Sorry! Something went wrong.",
      });
    }
  };

  componentDidMount = () => {
    this._isMounted = true;
    if (this.state.me == null) this.loadMe();
  };

  componentWillUnmount = () => {
    // console.log("App > componentWillUnmount");
    this._isMounted = false;
  };

  render() {
    console.log("App.js > render");
    return (
      <Switch>
        <Route
          exact
          path="/"
          render={(props) => <Home {...props} onLogout={this.handleLogout} />}
        ></Route>
        <Route
          path="/auth/login"
          render={(props) => <Login {...props} onLogin={this.handleLogin} />}
        ></Route>
      </Switch>
    );
  }
}

export default withRouter(App);
