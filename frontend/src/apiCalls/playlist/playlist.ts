import axios from 'axios';
import type { PlaylistDTO } from '../DTO/playlistDTO'; // Adjust path as needed
import { PUBLIC_SERVER_URL } from '$env/static/public';

// API Call to Create Playlist
export const createPlaylist = async (data: PlaylistDTO.CreatePlaylistRequest): Promise<PlaylistDTO.CreatePlaylistResult> => {
  try {
    const response = await axios.post(`${PUBLIC_SERVER_URL}/playlist/create`, data);
    return response.data;
  } catch (error) {
    console.error("Error creating playlist:", error);
    throw error;
  }
};

// API Call to Delete Playlist
export const deletePlaylist = async (data: PlaylistDTO.DeletePlaylistRequest): Promise<PlaylistDTO.DeletePlaylistResult> => {
  try {
    const response = await axios.delete(`${PUBLIC_SERVER_URL}/playlist/delete`, { data });
    return response.data;
  } catch (error) {
    console.error("Error deleting playlist:", error);
    throw error;
  }
};

// API Call to Edit Playlist
export const editPlaylist = async (data: PlaylistDTO.EditPlaylistRequest): Promise<PlaylistDTO.EditPlaylistResult> => {
  try {
    const response = await axios.put(`${PUBLIC_SERVER_URL}/playlist/edit`, data);
    return response.data;
  } catch (error) {
    console.error("Error editing playlist:", error);
    throw error;
  }
};

// API Call to Get Playlist
export const getPlaylist = async (params: PlaylistDTO.GetRequest): Promise<PlaylistDTO.GetResult> => {
  try {
    const response = await axios.get(`${PUBLIC_SERVER_URL}/playlist/get`, { params });
    return response.data;
  } catch (error) {
    console.error("Error getting playlist:", error);
    throw error;
  }
};

// API Call to Add Audio to Playlist
export const addAudioToPlaylist = async (data: PlaylistDTO.AddAudioToPlaylistRequest): Promise<PlaylistDTO.AddAudioToPlaylistResult> => {
  try {
    const response = await axios.post(`${PUBLIC_SERVER_URL}/playlist/add-audio`, data);
    return response.data;
  } catch (error) {
    console.error("Error adding audio to playlist:", error);
    throw error;
  }
};

// API Call to Remove Audio from Playlist
export const removeAudioFromPlaylist = async (data: PlaylistDTO.RemoveAudioRequest): Promise<PlaylistDTO.RemoveAudioResult> => {
  try {
    const response = await axios.post(`${PUBLIC_SERVER_URL}/playlist/remove-audio`, data);
    return response.data;
  } catch (error) {
    console.error("Error removing audio from playlist:", error);
    throw error;
  }
};
