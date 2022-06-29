import axiosPrivate from "./axiosPrivate";

// export const useGetMe = async () => {
//   const axiosPrivate = useAxiosPrivate();
//   let url = API_BASE_URL + "/me";
//   return await axiosPrivate.get(url);
// };

export const getMe = async () => {
  //console.log("meApi > getMe");
  return await axiosPrivate.get("/me");
};
