import axios from 'axios';
import type { AudioDTO } from './audioDTO';
import { PUBLIC_SERVER_URL } from '$env/static/public';

// Base URL for the API
// API Call to Create Audio
export const createAudio = async (data: AudioDTO.CreateAudioRequest): Promise<AudioDTO.CreateAudioResult> => {
  try {
    const response = await axios.post(`${PUBLIC_SERVER_URL}/audio/create`, data);
    return response.data; // Assuming the response structure matches CreateAudioResult
  } catch (error) {
    console.error("Error creating audio:", error);
    throw error; // Handle this appropriately in your UI
  }
};

// API Call to Delete Audio
export const deleteAudio = async (data: AudioDTO.DeleteAudioRequest): Promise<AudioDTO.DeleteAudioResult> => {
  try {
    const response = await axios.delete(`${PUBLIC_SERVER_URL}/audio/delete`, { data }); // Sending body in DELETE request
    return response.data; // Assuming the response structure matches DeleteAudioResult
  } catch (error) {
    console.error("Error deleting audio:", error);
    throw error;
  }
};

// API Call to Edit Audio
export const editAudio = async (data: AudioDTO.EditAudioRequest): Promise<AudioDTO.EditAudioResult> => {
  try {
    const response = await axios.put(`${PUBLIC_SERVER_URL}/audio/edit`, data);
    return response.data; // Assuming the response structure matches EditAudioResult
  } catch (error) {
    console.error("Error editing audio:", error);
    throw error;
  }
};

// API Call to Get Audio by ID
export const getAudio = async (audioId: number): Promise<AudioDTO.GetAudioResult> => {
  try {
    const response = await axios.get(`${PUBLIC_SERVER_URL}/audio/get`, { params: { audioId } });
    return response.data; // Assuming the response structure matches GetAudioResult
  } catch (error) {
    console.error("Error getting audio:", error);
    throw error;
  }
};
