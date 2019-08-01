import React from 'react';
import PropTypes from 'prop-types';

const ContactRow = props => {
  const {
    Id = 0,
    First = '',
    Last = '',
    Email = '',
    Phone1 = ''
  } = props.contact || {};

  return (
    <tr>
      <td>{Id}</td>
      <td>{First}</td>
      <td>{Last}</td>
      <td>{Email}</td>
      <td>{Phone1}</td>
    </tr>
  )
}

ContactRow.propTypes = {
  contact: PropTypes.shape({
    Id: PropTypes.number,
    First: PropTypes.string,
    Last: PropTypes.string,
    Email: PropTypes.string, 
    Phone1: PropTypes.string
  }).isRequired
};

export default ContactRow;
