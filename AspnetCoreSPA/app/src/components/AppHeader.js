import React, { Component } from "react";
import { Link, withRouter } from "react-router-dom";
import "./AppHeader.css";
import { Layout, Menu, Dropdown, Icon } from "antd";
const Header = Layout.Header;

class AppHeader extends Component {
  constructor(props) {
    super(props);
    this.handleMenuClick = this.handleMenuClick.bind(this);
  }

  handleMenuClick({ key }) {
    if (key === "logout") {
      this.props.onLogout();
    }
  }

  render() {
    let menuItems;
    //console.log("AppHeader > render > props:\n", this.props);
    if (this.props.me) {
      menuItems = [
        <Menu.Item key="/profile" className="profile-menu">
          <ProfileDropdownMenu
            me={this.props.me}
            handleMenuClick={this.handleMenuClick}
          />
        </Menu.Item>,
      ];
    }

    return (
      <Header>
        <div className="container">
          <div className="app-title">
            <Link to="/">My Contact Management</Link>
          </div>
          <Menu
            className="app-menu"
            mode="horizontal"
            selectedKeys={[this.props.location.pathname]}
            style={{ lineHeight: "64px" }}
          >
            {menuItems}
          </Menu>
        </div>
      </Header>
    );
  }
}

function ProfileDropdownMenu(props) {
  //console.log("ProfileDropdownMenu > props:\n", props);
  const dropdownMenu = (
    <Menu onClick={props.handleMenuClick} className="profile-dropdown-menu">
      <Menu.Item key="user-info" className="dropdown-item" disabled>
        <div className="user-full-name-info">
          {props.me.FirstName} {props.me.LastName}
        </div>
        <div className="username-info">{props.me.Email}</div>
      </Menu.Item>
      <Menu.Divider />
      <Menu.Item key="profile" className="dropdown-item">
        <Link to={`/users/${props.me.username}`}>Profile</Link>
      </Menu.Item>
      <Menu.Item key="logout" className="dropdown-item">
        Logout
      </Menu.Item>
    </Menu>
  );

  return (
    <Dropdown
      overlay={dropdownMenu}
      trigger={["click"]}
      getPopupContainer={() =>
        document.getElementsByClassName("profile-menu")[0]
      }
    >
      <a className="ant-dropdown-link">
        <Icon type="user" className="nav-icon" style={{ marginRight: 0 }} />{" "}
        <Icon type="down" />
      </a>
    </Dropdown>
  );
}

export default withRouter(AppHeader);
