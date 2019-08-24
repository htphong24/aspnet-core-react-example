import React, { Component } from 'react';
import { Route, withRouter, Switch } from 'react-router-dom';
import 'antd/dist/antd.css';
import { Form, Input, Row, Col, Pagination } from 'antd';
import './App.css';
import { PAGE_SIZE } from './constants';
import ContactRow from './components/ContactRow';
import ContactAddForm from './components/ContactAddForm';
import { getContacts } from './utils/APIUtils';

class App extends Component {

  constructor(props) {
    super(props);

    this.state = {
      recordCount: null,    // total number of records
      currentContacts: [],  // an array of all the contacts to be shown on the currently active page. Initialized to an empty array([])
      currentPage: 1,       // the page number of the currently active page. Initialized to 1
      pageCount: null,      // the total number of pages for all the contact records. Initialized to null.
      filter: "",           // search keyword
      editingContact: null, // id of the contact in Edit mode
    };

    this.handleSearchChange = this.handleSearchChange.bind(this);
    this.handleAdded = this.handleAdded.bind(this);
    this.handleEdit = this.handleEdit.bind(this);
    this.handleUpdated = this.handleUpdated.bind(this);
    this.handleCanceled = this.handleCanceled.bind(this);
    this.handleDeleted = this.handleDeleted.bind(this);
    this.handleReloaded = this.handleReloaded.bind(this);
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
    getContacts(
      this.state.filter,
      this.state.currentPage
    ).then(response => {
      this.setState({
        recordCount: response.RecordCount,
        currentContacts: response.Results,
        currentPage: response.PageNumber,
        pageCount: response.PageCount
      });
    })
    .catch(error => {
      if (error.status === 404) {
        this.setState({
          recordCount: null
        });
      }
      else {
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

  handleAdded = () => {
    // clear search query
    this.setState({
      filter: ""
    }, () => {
      getContacts(
        this.state.filter,
        this.state.currentPage
      )
      .then(response => {
        // then move to the last page to show the contact has been added
        let lastPage = Math.ceil(response.RecordCount / PAGE_SIZE);
        getContacts(
          this.state.filter,
          lastPage
        )
        .then(response => {
          this.setState({
            recordCount: response.RecordCount,
            currentContacts: response.Results,
            currentPage: response.PageNumber,
            pageCount: response.PageCount
          });
        })
        .catch(error => {
          if (error.status === 404) {
            this.setState({
              recordCount: null
            });
          }
          else {
            this.setState({
              recordCount: null
            });
          }
        });
      })
      .catch(error => {
        if (error.status === 404) {
          this.setState({
            recordCount: null
          });
        }
        else {
          this.setState({
            recordCount: null
            });
          }
        });
    });
  }

  handleDeleted = () => {
    getContacts(
      this.state.filter,
      this.state.currentPage
    )
      .then(response => {
        // then move to the currentPage to show the page where we were before onDelete
        let lastPage = Math.ceil(response.RecordCount / PAGE_SIZE);
        let newCurrentPage = this.state.currentPage >= lastPage ? lastPage : this.state.currentPage
    
        getContacts(
          this.state.filter,
          newCurrentPage
        )
          .then(response => {
            this.setState({
              recordCount: response.RecordCount,
              currentContacts: response.Results,
              currentPage: response.PageNumber,
              pageCount: response.PageCount
            });
          })
          .catch(error => {
            if (error.status === 404) {
              this.setState({
                recordCount: null
              });
            }
            else {
              this.setState({
                recordCount: null
              });
            }
          });
      })
      .catch(error => {
        if (error.status === 404) {
          this.setState({
            recordCount: null
          });
        }
        else {
          this.setState({
            recordCount: null
          });
        }
      });
  }

  handleEdit = (evt) => {
    this.setState({
      editingContact: evt
    });
  };

  handleUpdated = (evt) => {
    this.setState({
      editingContact: null
    });
  };

  handleCanceled = (evt) => {
    this.setState({
      editingContact: null
    });
  };

  handleReloaded = (evt) => {
    console.log("App - handleReloaded - evt");
    console.log(evt);
    this.setState({
      recordCount: null,
      currentContacts: [],
      currentPage: 1,
      pageCount: null,
      filter: "",
      editingContact: null,
    },
    () => {
      this.loadContacts();
    });
  };

  render() {
    // We render the total number of contacts, the current page, the total number of pages,
    // <Pagination> control and then <ContactRow> for each contact in the current page
    const { recordCount, currentContacts, currentPage, pageCount, editingContact } = this.state;
    const headerClass = ['text-dark py-2 pr-4 m-0', currentPage ? 'border-gray border-right' : ''].join(' ').trim();
    const MyContactAddForm = Form.create()(ContactAddForm);
    
    return (
      <div className="container">
        <h1 className="text-center">My Contact Management</h1>
        <Input.Search id="txtSearch" placeholder="Search" onChange={this.handleSearchChange} value={this.state.filter} />
        <Row>
          <MyContactAddForm onAdded={this.handleAdded} onReloaded={this.handleReloaded}/>
        </Row>

        <div className="my-custom-scrollbar">
          <Row className="my-row-header">
            <Col span={2}>Actions</Col>
            <Col span={2}>Id</Col>
            <Col span={4}>First Name</Col>
            <Col span={4}>Last Name</Col>
            <Col span={8}>Email</Col>
            <Col span={4}>Phone1</Col>
          </Row>
          {currentContacts.map(contact => <ContactRow
                                            key={contact.Id} 
                                            contact={contact} 
                                            editingContact={editingContact} 
                                            onEdit={this.handleEdit} 
                                            onUpdated={this.handleUpdated} 
                                            onCanceled={this.handleCanceled} 
                                            onDeleted={this.handleDeleted}
                                          />)}
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
