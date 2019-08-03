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
      <td className="my-col-10">
        <i className="fas fa-edit"></i>&nbsp;&nbsp;&nbsp;&nbsp;
        <i className="fas fa-trash"></i>
      </td>
      <td className="my-col-5">{Id}</td>
      <td className="my-col-20">{First}</td>
      <td className="my-col-20">{Last}</td>
      <td className="my-col-25">{Email}</td>
      <td className="my-col-15">{Phone1}</td>
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
