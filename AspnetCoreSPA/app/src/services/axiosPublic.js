import axios from "axios";
import { API_BASE_URL } from "../constants";

// export const useAxiosPublic = () => {
//   return axios.create({
//     baseURL: API_BASE_URL,
//     headers: { "Content-Type": "application/json" },
//   });
// };

const axiosPublic = axios.create({
  baseURL: API_BASE_URL,
  headers: { "Content-Type": "application/json" },
});

export default axiosPublic;
