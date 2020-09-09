import React, { Component } from 'react';
import 'antd/dist/antd.css';
import { Row, Col, Form, Pagination, Input, Layout } from 'antd';
import { PAGE_SIZE } from '../constants';
import ContactRow from './ContactRow';
import ContactAddForm from './ContactAddForm';
import AppHeader from './AppHeader';
import { getContacts } from '../utils/APIUtils';
const { Content } = Layout;

class Home extends Component {
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

        this.handlePageChanged = this.handlePageChanged.bind(this);
        this.handleSearchChanged = this.handleSearchChanged.bind(this);
        this.handleAdded = this.handleAdded.bind(this);
        this.handleEdit = this.handleEdit.bind(this);
        this.handleUpdated = this.handleUpdated.bind(this);
        this.handleCanceled = this.handleCanceled.bind(this);
        this.handleDeleted = this.handleDeleted.bind(this);
        this.handleReloaded = this.handleReloaded.bind(this);
    }

    _isMounted = false;

    // EVENTS

    // This will be called each time we navigate to a new page from the pagination control. This method will be
    // passed to the handlePageChanged prop of the Pagination component.
    handlePageChanged = data => {
        //console.log("Home.js handlePageChanged");
        this.setState({
            currentPage: data
        }, () => this.loadContacts());
    }

    handleSearchChanged = evt => {
        // Prevent the browser's default action of submitting the form.
        //console.log("Home.js handleSearchChange");
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

    handleAdded = () => {
        // clear search query
        //console.log("Home.js handleAdded");
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
                            //console.log("Home.js handleAdded then getContacts");
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

    handleEdit = (evt) => {
        //console.log("Home.js handleEdit");
        this.setState({
            editingContact: evt
        });
    };

    handleUpdated = (evt) => {
        //console.log("Home.js handleUpdated");
        this.setState({
            editingContact: null
        });
    };

    handleCanceled = (evt) => {
        //console.log("Home.js handleCanceled");
        this.setState({
            editingContact: null
        });
    };

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
                        //console.log("Home.js handleDeleted then getContacts");
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

    handleReloaded = (evt) => {
        //console.log("Home.js handleReloaded");
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

    // LIFECYCLE METHODS

    componentDidMount = () => {
        //console.log("Home.js componentDidMount");
        this._isMounted = true;
        var token = localStorage.getItem("accessToken");
        if (token == null && !this.props.isAuthenticated) {
            this.props.history.push("/auth/login");
        }
        else {
            this.loadContacts();
        }
    }

    componentWillMount = () => {
        //console.log("Home.js componentWillMount");
    }

    componentWillReceiveProps = () => {
        //console.log("Home.js componentWillReceiveProps");
    }

    componentWillUnmount = () => {
        //console.log("Home.js componentWillUnmount");
        this._isMounted = false;
    }

    // PRIVATE METHODS

    loadContacts = () => {
        //console.log("Home.js loadContacts");
        if (this._isMounted) {
            //console.log("Home.js loadContacts _isMounted==true");
            getContacts(
                this.state.filter,
                this.state.currentPage
            )
                .then(response => {
                //console.log("Home.js loadContacts then getContacts");
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
    }

    render() {
        var token = localStorage.getItem("accessToken");
        if (token == null) {
            return (<div/>);
        }
        else {
            // We render the total number of contacts, the current page, the total number of pages,
            // <Pagination> control and then <ContactRow> for each contact in the current page
            const { recordCount, currentContacts, currentPage, pageCount, editingContact } = this.state;
            const headerClass = ['text-dark py-2 pr-4 m-0', currentPage ? 'border-gray border-right' : ''].join(' ').trim();
            const MyContactAddForm = Form.create()(ContactAddForm);

            return (
                <Layout className="app-container">
                    <AppHeader onLogout={this.props.onLogout} currentUser={this.props.currentUser} />
                    <Content>
                        <div className="container">
                            <div className="home-container">
                                <Input.Search id="txtSearch" placeholder="Search" onChange={this.handleSearchChanged} value={this.state.filter} />
                                <Row>
                                    <MyContactAddForm onAdded={this.handleAdded} onReloaded={this.handleReloaded} />
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
                                        onDeleted={this.handleDeleted} />)}
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
                        </div>
                    </Content>
                </Layout>
            );
        }
    }
}

export default Home;