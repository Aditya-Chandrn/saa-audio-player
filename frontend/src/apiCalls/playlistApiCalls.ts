import { goto } from '$app/navigation';
import { PUBLIC_SERVER_URL } from '$env/static/public';
import LocalStorage from '@/utils/localStorage';
import axios from 'axios';
import { getDefaultPlaylist } from './userApiCalls';

///////// CREATE PLAYLIST /////////////
async function createPlaylist(name: string){
  const userId: number = LocalStorage.getItem("user").userId;
	const url: string = `${PUBLIC_SERVER_URL}/playlist/create`;
	try{
		const response: any = await axios.post(url, {userId, name});
    alert(response.data.message);
	} catch (error: any){
		alert(error.response.data.message);
	}
}

///////// GET PLAYLIST /////////////
async function getPlaylist(playlistId: number){
	const url: string = `${PUBLIC_SERVER_URL}/playlist/get`;
	try {
		const response = await axios.get(url, { params: {playlistId}});
		return response.data.playlist;
	} catch (error:any) {
		alert(error.response.data.message);
	}
};

///////// EDIT PLAYLIST /////////////
async function editPlaylist(playlistId: number, name: string) {
	const url: string = `${PUBLIC_SERVER_URL}/playlist/edit`;

	try{
		const response: any = await axios.patch(url, {playlistId, name})
		alert(response.data.message);
		return await getDefaultPlaylist();
	} catch (error: any){
		alert(error.response.data.message);
		
	}
}

///////// DELETE PLAYLIST /////////////
async function deletePlaylist(playlistId: number) {
	const url: string = `${PUBLIC_SERVER_URL}/playlist/delete`;
	try {
		const response = await axios.delete(url, {data: playlistId});
		alert(response.data.message);
    goto("/music")
	} catch (error:any) {
		alert(error.response.data.message);
	}
};

///////// ADD AUDIO /////////////
async function addAudio(playlistId: number, audioId: number) {
	const url: string = `${PUBLIC_SERVER_URL}/playlist/add-audio`;
	try {
		const response = await axios.post(url, {playlistId, audioId});
		alert(response.data.message);
    return await getPlaylist(playlistId);
	} catch (error:any) {
		alert(error.response.data.message);
	}
};

///////// REMOVE AUDIO /////////////
async function removeAudio(playlistId: number, audioId: number) {
	const url: string = `${PUBLIC_SERVER_URL}/playlist/remove-audio`;
	try {
		const response = await axios.post(url, {playlistId, audioId});
		alert(response.data.message);
    return await getPlaylist(playlistId);
	} catch (error:any) {
		alert(error.response.data.message);
	}
};



export {createPlaylist, getPlaylist, editPlaylist, deletePlaylist, addAudio, removeAudio};