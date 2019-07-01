import React from 'react';
import PropTypes from 'prop-types';

const ContactRow = props => {
  const {
    //id = 0,
    first = '',
    last = '',
    email = '',
    phone1 = ''
  } = props.contact || {};

  return (
    <tr>
      <td>{first}</td>
      <td>{last}</td>
      <td>{email}</td>
      <td>{phone1}</td>
    </tr>
  )
}

ContactRow.propTypes = {
  contact: PropTypes.shape({
    //id: PropTypes.number.isRequired,
    first: PropTypes.string.isRequired,
    last: PropTypes.string.isRequired, 
    email: PropTypes.string, 
    phone1: PropTypes.string
  }).isRequired
};

export default ContactRow;
