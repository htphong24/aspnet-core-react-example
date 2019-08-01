import React, { Component } from 'react';
import './App.css';

import { PAGE_LIMIT, API_BASE_URL } from './constants';
import Pagination from './components/Pagination';
import ContactRow from './components/ContactRow';

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

    // When a user submits a comment, we will need to refresh the list of comments to 
    // include the new one.
    this.handleSearchChange = this.handleSearchChange.bind(this);
  }

  handleSearchChange(e) {
    // Prevent the browser's default action of submitting the form.
    e.preventDefault();
    this.setState({
        filter: e.target.value
      },
      () => {
        this.loadContacts();
      }
    );
  }

  loadContacts() {
    let contactUrl = this.state.filter === ""
      ? API_BASE_URL + "contacts?PageNumber=" + this.state.currentPage + "&RowsPerPage=" + PAGE_LIMIT // on load
      : API_BASE_URL + "contacts/search?q=" + this.state.filter + "&PageNumber=" + this.state.currentPage + "&RowsPerPage=" + PAGE_LIMIT // on search
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

  componentDidMount() {
    this.loadContacts();
  }

  // This will be called each time we navigate to a new page from the pagination control. This method will be 
  // passed to the onPageChanged prop of the Pagination component.
  onPageChanged = data => {
    console.log()
    this.setState({
      currentPage: data.currentPage
    }, () => this.loadContacts());
  }

  /*
  Let's say we have a fictitious API endpoint /api/contacts?page={current_page}&limit={page_limit}. 
  The following snippet shows how we can fetch contacts on demand from the API using the axios HTTP package:

  onPageChanged = data => {
    const { currentPage, totalPages, pageLimit } = data;

    axios.get(`/api/contacts?page=${currentPage}&limit=${pageLimit}`)
      .then(response => {
        const currentContacts = response.data.contacts;
        this.setState({ currentPage, currentContacts, totalPages });
      });
  }
  */

  render() {
    
    // We render the total number of contacts, the current page, the total number of pages,
    // <Pagination> control and then <ContactRow> for each contact in the current page
    const { recordCount, currentContacts, currentPage, pageCount } = this.state;
    
    //if (recordCount === 0) return null;
    const headerClass = ['text-dark py-2 pr-4 m-0', currentPage ? 'border-gray border-right' : ''].join(' ').trim();
    
    // Notice that we passed the onPageChanged() method we defined earlier to the onPageChanged prop of 
    // <Pagination> control. This is very important for capturing page changes from the Pagination component
    // Also notice that we are displaying 5 contacts per page
    return (
      <div className="container mb-5">

        <h1 className="text-center">My Contact Management</h1>
        <br/>

        <form className="form-inline md-form form-sm mt-0">
          <i className="fas fa-search" aria-hidden="true"></i>
          <input className="form-control form-control-sm ml-3 w-75" type="text" placeholder="Search" aria-label="Search" onChange={this.handleSearchChange}/>
        </form>

        <div className="row d-flex flex-row py-5">

          <h4>Contact List</h4>

          <div className="table-wrapper-scroll-y my-custom-scrollbar">
            <table className="table table-bordered table-striped mb-0">
              <thead>
                <tr>
                  <th scope="col">Id</th>
                  <th scope="col">First</th>
                  <th scope="col">Last</th>
                  <th scope="col">Email</th>
                  <th scope="col">Phone1</th>
                </tr>
              </thead>
              <tbody>
                {currentContacts.map(contact => <ContactRow key={contact.Id} contact={contact} />)}
              </tbody>
            </table>
          </div>

          <div className="w-100 px-4 py-5 d-flex flex-row flex-wrap align-items-center justify-content-between">
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

            <div className="d-flex flex-row py-4 align-items-center">
              <Pagination totalRecords={recordCount} pageLimit={PAGE_LIMIT} pageNeighbours={1} onPageChanged={this.onPageChanged} />
            </div>
          </div>

        </div>
      </div>
    );

  }

}

export default App;