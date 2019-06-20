import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import './App.css';

import { PAGE_LIMIT, API_BASE_URL } from './constants';
import Pagination from './components/Pagination';
import ContactRow from './components/ContactRow';

const testContactsData = [
  { id: 1,  first: 'F1',  last: 'L1',  email: 'E1@abc.com',  phone1: '1000001'  },
  { id: 2,  first: 'F2',  last: 'L2',  email: 'E2@abc.com',  phone1: '1000002'  },
  { id: 3,  first: 'F3',  last: 'L3',  email: 'E3@abc.com',  phone1: '1000003'  },
  { id: 4,  first: 'F4',  last: 'L4',  email: 'E4@abc.com',  phone1: '1000004'  },
  { id: 5,  first: 'F5',  last: 'L5',  email: 'E5@abc.com',  phone1: '1000005'  },
  { id: 6,  first: 'F6',  last: 'L6',  email: 'E6@abc.com',  phone1: '1000006'  },
  { id: 7,  first: 'F7',  last: 'L7',  email: 'E7@abc.com',  phone1: '1000007'  },
  { id: 8,  first: 'F8',  last: 'L8',  email: 'E8@abc.com',  phone1: '1000008'  },
  { id: 9,  first: 'F9',  last: 'L9',  email: 'E9@abc.com',  phone1: '1000009'  },
  { id: 10, first: 'F10', last: 'L10', email: 'E10@abc.com', phone1: '10000010' },
  { id: 11, first: 'F11', last: 'L11', email: 'E11@abc.com', phone1: '10000011' },
  { id: 12, first: 'F12', last: 'L12', email: 'E12@abc.com', phone1: '10000012' },
  { id: 13, first: 'F13', last: 'L13', email: 'E13@abc.com', phone1: '10000013' },
  { id: 14, first: 'F14', last: 'L14', email: 'E14@abc.com', phone1: '10000014' },
  { id: 15, first: 'F15', last: 'L15', email: 'E15@abc.com', phone1: '10000015' },
  { id: 16, first: 'F16', last: 'L16', email: 'E16@abc.com', phone1: '10000016' },
];

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
      allContacts: [],     // an array of all the contacts in our app. Initialized to an empty array([]) // allContacts: this.props.initialData
      currentContacts: [], // an array of all the contacts to be shown on the currently active page. Initialized to an empty array([])
      currentPage: null,   // the page number of the currently active page. Initialized to null
      totalPages: null,    // the total number of pages for all the contact records. Initialized to null.
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
    }, () => { this.loadContacts(); });
  }

  loadContacts() {
    let contactUrl = this.state.filter === ""
      ? API_BASE_URL + "/contacts"
      : API_BASE_URL + "/contacts/search/" + this.state.filter;
    request({
      url: contactUrl,
      method: 'GET'
    }).then(response => {
      this.setState({
        allContacts: response
      }, () => {
          const newCurrentPage = 1;
          const offset = (newCurrentPage - 1) * PAGE_LIMIT;
          const newCurrentContacts = this.state.allContacts.slice(offset, offset + PAGE_LIMIT);
          const newTotalPages = Math.ceil(this.state.allContacts.length / PAGE_LIMIT);
          this.setState({
            currentContacts: newCurrentContacts,
            currentPage: newCurrentPage,
            totalPages: newTotalPages
          });
      });
    }).catch(error => {
      if (error.status === 404) {
        this.setState({
          allContacts: []
        });
      } else {
        this.setState({
          allContacts: []
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
    const { allContacts } = this.state;
    const { currentPage, totalPages, pageLimit } = data;

    // "offset" indicates the starting index for fetching the records for the current page.
    // Using (currentPage - 1) ensures that the offset is zero-based.
    // Example: we are displaying 25 records per page and we are currently viewing page 5
    // Then "offset" will be ((5 - 1) * 25 = 100), i.e. we will fetch from 100 to 124
    const offset = (currentPage - 1) * pageLimit;

    // Since, we are not fetching records on demand from a database or any external source, 
    // we need a way to extract the required chunk of records to be shown for the current page.
    // offset:              starting index for the slice
    // offset + pageLimit:  the index before which to end the slice
    const currentContacts = allContacts.slice(offset, offset + pageLimit);
    this.setState({
      currentContacts: currentContacts,
      currentPage: currentPage,
      totalPages: totalPages
    });
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
    const { allContacts, currentContacts, currentPage, totalPages } = this.state;
    const totalContacts = allContacts.length;
    if (totalContacts === 0) return null;
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
                  <th scope="col">First</th>
                  <th scope="col">Last</th>
                  <th scope="col">Email</th>
                  <th scope="col">Phone1</th>
                </tr>
              </thead>
              <tbody>
                {currentContacts.map(contact => <ContactRow key={contact.id} contact={contact} />)}
              </tbody>
            </table>
          </div>

          <div className="w-100 px-4 py-5 d-flex flex-row flex-wrap align-items-center justify-content-between">
            <div className="d-flex flex-row align-items-center">

              <h5 className={headerClass}>
                <strong className="text-secondary">{totalContacts}</strong> Contacts found
              </h5>

              {currentPage && (
                <span className="current-page d-inline-block h-100 pl-4 text-secondary">
                  Page <span className="font-weight-bold">{currentPage}</span> / <span className="font-weight-bold">{totalPages}</span>
                </span>
              )}

            </div>

            <div className="d-flex flex-row py-4 align-items-center">
              {totalContacts && (<Pagination totalRecords={totalContacts} pageLimit={PAGE_LIMIT} pageNeighbours={1} onPageChanged={this.onPageChanged} />)}
            </div>
          </div>

        </div>
      </div>
    );

  }

}

export default App;