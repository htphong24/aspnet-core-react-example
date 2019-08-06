import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Form, Icon, Input, Row, Col, Pagination } from 'antd';

import './App.css';

import { PAGE_SIZE, API_BASE_URL } from './constants';
//import Pagination from './components/Pagination';
import ContactRow from './components/ContactRow';
import ContactAddForm from './components/ContactAdd';

const request = (options) => {
  const headers = new Headers({
    'Content-Type': 'application/json'
  })

  const defaults = { headers: headers };
  options = Object.assign({}, defaults, options);
  return fetch(options.url, options)
    .then(response =>
      response.json().then(json => {
        if (!response.ok) {
          return Promise.reject(json);
        }
        return json;
      })
    );
};

class App extends Component {

  constructor(props) {
    super(props);

    this.state = {
      recordCount: null,   // total number of records
      currentContacts: [], // an array of all the contacts to be shown on the currently active page. Initialized to an empty array([])
      currentPage: 1,   // the page number of the currently active page. Initialized to 1
      pageCount: null,    // the total number of pages for all the contact records. Initialized to null.
      filter: ""           // search keyword
    };

    this.handleSearchChange = this.handleSearchChange.bind(this);
    //this.handleFormAdd = this.handleFormAdd.bind(this);
  }

  handleSearchChange = evt => {
    // Prevent the browser's default action of submitting the form.
    evt.preventDefault();
    this.setState({
      filter: evt.target.value,
      currentPage: 1
      },
      () => {
        this.loadContacts();
      }
    );
  }

  loadContacts = () => {
    let contactUrl = this.state.filter === ""
      ? API_BASE_URL + "contacts?PageNumber=" + this.state.currentPage + "&RowsPerPage=" + PAGE_SIZE // on load
      : API_BASE_URL + "contacts/search?q=" + this.state.filter + "&PageNumber=" + this.state.currentPage + "&RowsPerPage=" + PAGE_SIZE // on search
    request({
      url: contactUrl,
      method: 'GET'
    }).then(response => {
      this.setState({
        recordCount: response.RecordCount,
        currentContacts: response.Results,
        currentPage: response.PageNumber,
        pageCount: response.PageCount
      });
    }).catch(error => {
      if (error.status === 404) {
        this.setState({
          recordCount: null
        });
      } else {
        this.setState({
          recordCount: null
        });
      }
    });
  }

  componentDidMount = () => {
    this.loadContacts();
  }

  // This will be called each time we navigate to a new page from the pagination control. This method will be 
  // passed to the handlePageChanged prop of the Pagination component.
  handlePageChanged = data => {
    this.setState({
      currentPage: data
    }, () => this.loadContacts());
  }

  //handleFormAdd = evt => {
  //  evt.preventDefault();
    //this.props.form.validateFields((err, values) => {
    //  if (!err) {
    //    console.log('Received values of form: ', values);
    //  }
    //});
    // TODO: retrieve first, last, email, phone and call ajax
  //}

  render() {
    // We render the total number of contacts, the current page, the total number of pages,
    // <Pagination> control and then <ContactRow> for each contact in the current page
    const { recordCount, currentContacts, currentPage, pageCount } = this.state;
    const headerClass = ['text-dark py-2 pr-4 m-0', currentPage ? 'border-gray border-right' : ''].join(' ').trim();
    const MyContactAddForm = Form.create()(ContactAddForm);

    return (
      <div className="container">
        <h1 className="text-center">My Contact Management</h1>
        <Input.Search placeholder="Search" onChange={this.handleSearchChange} />
        <Row>
          <MyContactAddForm />
        </Row>

        <div className="my-custom-scrollbar">
          <Row className="my-row-header">
            <Col span={2}>Actions</Col>
            <Col span={2}>Id</Col>
            <Col span={4}>First</Col>
            <Col span={4}>Last</Col>
            <Col span={8}>Email</Col>
            <Col span={4}>Phone1</Col>
          </Row>
          {currentContacts.map(contact => <ContactRow key={contact.Id} contact={contact} />)}
        </div>

        <div className="w-100 px-4 d-flex flex-row flex-wrap align-items-center justify-content-between">
          <div className="d-flex flex-row align-items-center">

            <h5 className={headerClass}>
              <strong className="text-secondary">{recordCount}</strong> Contacts found
            </h5>

            {currentPage && (
              <span className="current-page d-inline-block h-100 pl-4 text-secondary">
                Page <span className="font-weight-bold">{currentPage}</span> / <span className="font-weight-bold">{pageCount}</span>
              </span>
            )}

          </div>

          <div className="d-flex flex-row align-items-center">
            <Pagination total={recordCount} pageSize={PAGE_SIZE} current={currentPage} onChange={this.handlePageChanged} />
          </div>
        </div>

      </div>
    );

  }

}

export default App;