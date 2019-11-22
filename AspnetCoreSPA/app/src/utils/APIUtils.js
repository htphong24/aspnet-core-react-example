import { PAGE_SIZE, API_BASE_URL } from '../constants';

//
// All the Rest API calls are written in this script. 
// It uses the fetch API to make requests to the backend server
//

const request = (options) => {
    const headers = new Headers({
        'Content-Type': 'application/json',
    })

    //if (localStorage.getItem(ACCESS_TOKEN)) {
    //  headers.append('Authorization', 'Bearer ' + localStorage.getItem(ACCESS_TOKEN))
    //}

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

export function getContacts(filter, page) {
    let contactUrl = filter === ""
        ? API_BASE_URL + "contacts?PageNumber=" + page + "&RowsPerPage=" + PAGE_SIZE // on load
        : API_BASE_URL + "contacts/search?q=" + filter + "&PageNumber=" + page + "&RowsPerPage=" + PAGE_SIZE // on search
    return request({
        url: contactUrl,
        method: 'GET'
    });
}

export function addContact(data) {
    return request({
        url: API_BASE_URL + "contacts",
        method: 'POST',
        body: JSON.stringify(data)
    });
}

export function updateContact(data) {
    return request({
        url: API_BASE_URL + "contacts/" + data.Contact.Id,
        method: 'PUT',
        body: JSON.stringify(data)
    });
}

export function deleteContact(data) {
    return request({
        url: API_BASE_URL + "contacts/" + data.Contact.Id,
        method: 'DELETE',
        body: JSON.stringify(data)
    });
}

export function reloadContacts() {
    return request({
        url: API_BASE_URL + "contacts/reload",
        method: 'POST',
        body: JSON.stringify({})
    });
}

export function login(data) {
    return request({
        url: API_BASE_URL + "/auth/login",
        method: 'POST',
        body: JSON.stringify(data)
    });
}