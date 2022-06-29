import axiosPrivate from "./axiosPrivate";
import { PAGE_SIZE } from "../constants";

// export const useGetContacts = async (filter, page) => {
//   const axiosPrivate = useAxiosPrivate();
//   let url =
//     filter === ""
//       ? API_BASE_URL +
//         "/contacts?PageNumber=" +
//         page +
//         "&RowsPerPage=" +
//         PAGE_SIZE // on load
//       : API_BASE_URL +
//         "/contacts/search?q=" +
//         filter +
//         "&PageNumber=" +
//         page +
//         "&RowsPerPage=" +
//         PAGE_SIZE; // on search

//   return await axiosPrivate.get(url);
// };

// export const useAddContact = async (data) => {
//   const axiosPrivate = useAxiosPrivate();
//   let url = API_BASE_URL + "/contacts";
//   return await axiosPrivate.post(url, data);
// };

// export const useUpdateContact = async (data) => {
//   const axiosPrivate = useAxiosPrivate();
//   let url = API_BASE_URL + "/contacts/" + data.Contact.Id;
//   return await axiosPrivate.put(url, data);
// };

// export const useDeleteContact = async (data) => {
//   const axiosPrivate = useAxiosPrivate();
//   let url = API_BASE_URL + "/contacts/" + data.Contact.Id;
//   return await axiosPrivate.delete(url, data);
// };

// export const useReloadContacts = async () => {
//   const axiosPrivate = useAxiosPrivate();
//   let url = API_BASE_URL + "/contacts/reload";
//   return await axiosPrivate.post(url);
// };

/********************************************** */
export const getContacts = async (filter, page) => {
  let url =
    filter === ""
      ? "/contacts?PageNumber=" + page + "&RowsPerPage=" + PAGE_SIZE // on load
      : "/contacts/search?q=" +
        filter +
        "&PageNumber=" +
        page +
        "&RowsPerPage=" +
        PAGE_SIZE; // on search
  return await axiosPrivate.get(url);
};

export const addContact = async (data) => {
  return await axiosPrivate.post("/contacts", data);
};

export const updateContact = async (data) => {
  let url = "/contacts/" + data.Contact.Id;
  return await axiosPrivate.put(url, data);
};

export const deleteContact = async (data) => {
  let url = "/contacts/" + data.Contact.Id;
  return await axiosPrivate.delete(url, data);
};

export const reloadContacts = async () => {
  return await axiosPrivate.post("/contacts/reload");
};
