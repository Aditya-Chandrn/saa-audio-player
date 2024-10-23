import axios from "axios";
import type { DeleteUserRequest, DeleteUserResult, EditUserRequest, EditUserResult } from "./userDTO";
import { PUBLIC_SERVER_URL } from "$env/static/public";

// Edit User API Call
export const editUser = async (editUserData: EditUserRequest): Promise<EditUserResult> => {
    try {
      const response = await axios.put<EditUserResult>(`${PUBLIC_SERVER_URL}/edit`, editUserData);
      return response.data;
    } catch (error:any) {
      throw new Error(error.response?.data?.message || 'Edit user failed');
    }
  };
  
  // Delete User API Call
  export const deleteUser = async (deleteUserData: DeleteUserRequest): Promise<DeleteUserResult> => {
    try {
      const response = await axios.delete<DeleteUserResult>(`${PUBLIC_SERVER_URL}/delete`, { data: deleteUserData });
      return response.data;
    } catch (error:any) {
      throw new Error(error.response?.data?.message || 'Delete user failed');
    }
  };

  