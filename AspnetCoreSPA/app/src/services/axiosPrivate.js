import axios from "axios";
import { API_BASE_URL } from "../constants";
import { useEffect } from "react";
import { refreshToken } from "./authenticationApi";
import { useHistory } from "react-router-dom";

// export const useAxiosPrivate = () => {
//   var axiosPrivate = axios.create({
//     baseURL: API_BASE_URL,
//     headers: { "Content-Type": "application/json" },
//     // set withCredentials to true to send cookies in the request. XMLHttpRequest from a different domain cannot
//     // set cookie values for their own domain unless withCredentials is set to true before making the request.
//     withCredentials: true,
//   });

//   const onRequest = async (config) => {
//     return config;
//   };

//   const onRequestError = (error) => {
//     return Promise.reject(error);
//   };

//   const onResponse = (response) => {
//     return response;
//   };

//   const onResponseError = async (error) => {
//     console.log("useAxiosPrivate.js > onResponseError > error");
//     console.log(error);
//     const prevRequest = error == null ? null : error.config; // original config
//     if (prevRequest.url !== "/auth/login" && error.response) {
//       // Access Token was expired
//       if (
//         [401, 403].includes(error.response.status) &&
//         //error.response.data.message === "jwt expired" && //???????
//         !prevRequest.retry
//       ) {
//         prevRequest.retry = true;

//         try {
//           const accessToken = await refreshToken();
//           return axiosPrivate(prevRequest);
//         } catch (_error) {
//           return Promise.reject(_error);
//         }
//       }
//     }
//     return Promise.reject(error);
//   };

//   useEffect(() => {
//     const requestIntercept = axiosPrivate.interceptors.request.use(
//       onRequest,
//       onRequestError
//     );

//     const responseIntercept = axiosPrivate.interceptors.response.use(
//       onResponse,
//       onResponseError
//     );

//     return () => {
//       axiosPrivate.interceptors.request.eject(requestIntercept);
//       axiosPrivate.interceptors.response.eject(responseIntercept);
//     };
//   }, [refreshToken]);

//   return axiosPrivate;
// };

const axiosPrivate = axios.create({
  baseURL: API_BASE_URL,
  headers: { "Content-Type": "application/json" },
  // set withCredentials to true to send cookies in the request. XMLHttpRequest from a different domain cannot
  // set cookie values for their own domain unless withCredentials is set to true before making the request.
  withCredentials: true,
});

const onResponse = (response) => {
  return response;
};

const onResponseError = async (error) => {
  //console.log("axiosPrivate > onResponseError > error:\n", error);
  const prevRequest = error == null ? null : error.config; // original config
  if (prevRequest.url !== "/auth/login" && error.response) {
    // Access Token was expired
    if (
      [401, 403].includes(error.response.status) &&
      //error.response.data.message === "jwt expired" && //???????
      !prevRequest.retry
    ) {
      prevRequest.retry = true;

      try {
        const response = await refreshToken();
        console.log(
          "axiosPrivate > onResponseError > after refreshToken > response:\n",
          response
        );

        if (
          [
            "Error retrieving refresh token",
            "No user found with token",
            "Refresh token is no longer active",
          ].includes(response.data.AuthTokenRefresh.Message) &&
          window.location.pathname !== "/auth/login"
        ) {
          window.location.href = "/auth/login";
        }
        return axiosPrivate(prevRequest);
      } catch (refreshError) {
        return Promise.reject(refreshError.response.data.ErrorMessage);
      }
    }
  }
  return Promise.reject(error.response.data.ErrorMessage);
};

axiosPrivate.interceptors.response.use(onResponse, onResponseError);

export default axiosPrivate;
