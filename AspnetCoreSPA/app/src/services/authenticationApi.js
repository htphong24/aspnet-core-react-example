import axios from "axios";
import { API_BASE_URL } from "../constants";

export const getAuth = async () => {
  //console.log("authenticationApi > getAuth");
  const axiosPrivate = axios.create({
    baseURL: API_BASE_URL,
    headers: { "Content-Type": "application/json" },
    // set withCredentials to true to send cookies in the request. XMLHttpRequest from a different domain cannot
    // set cookie values for their own domain unless withCredentials is set to true before making the request.
    withCredentials: true,
  });
  return await axiosPrivate.get("/auth");
};

export const login = async (data) => {
  //console.log("authenticationApi > login");
  const axiosPrivate = axios.create({
    baseURL: API_BASE_URL,
    headers: { "Content-Type": "application/json" },
    // set withCredentials to true to send cookies in the request. XMLHttpRequest from a different domain cannot
    // set cookie values for their own domain unless withCredentials is set to true before making the request.
    withCredentials: true,
  });
  return await axiosPrivate.post("/auth/login", data);
};

export const refreshToken = async () => {
  //console.log("authenticationApi > refreshToken");
  const axiosPrivate = axios.create({
    baseURL: API_BASE_URL,
    headers: { "Content-Type": "application/json" },
    // set withCredentials to true to send cookies in the request. XMLHttpRequest from a different domain cannot
    // set cookie values for their own domain unless withCredentials is set to true before making the request.
    withCredentials: true,
  });
  return await axiosPrivate.post("/auth/refresh-token");
};

export const logout = async (data) => {
  //console.log("authenticationApi > logout");
  const axiosPrivate = axios.create({
    baseURL: API_BASE_URL,
    headers: { "Content-Type": "application/json" },
    // set withCredentials to true to send cookies in the request. XMLHttpRequest from a different domain cannot
    // set cookie values for their own domain unless withCredentials is set to true before making the request.
    withCredentials: true,
  });
  return await axiosPrivate.post("/auth/logout", data);
};
