import { goto } from '$app/navigation';
import { PUBLIC_SERVER_URL } from '$env/static/public';
import LocalStorage from '@/utils/localStorage';
import axios from 'axios';
import { getDefaultPlaylist } from './userApiCalls';

///////// CREATE AUDIO /////////////
async function createAudio(audioBase64String: string, title: string, album: string | null){
  const userId: number = LocalStorage.getItem("user").userId;
	const url: string = `${PUBLIC_SERVER_URL}/audio/create`;
	try{
		const response: any = await axios.post(url, {userId, audioBase64String, title, album});
    alert(response.data.message);
		return await getDefaultPlaylist();
	} catch (error: any){
		alert(error.response.data.message);
	}
}

///////// GET AUDIO /////////////
async function getAudio(audioId: number){
	const url: string = `${PUBLIC_SERVER_URL}/audio/get`;
	try {
		const response = await axios.get(url, { params: {audioId}});
		// handle base64 to audio
	} catch (error:any) {
		alert(error.response.data.message);
	}
};

///////// EDIT AUDIO /////////////
async function editAudio(audioId: number, title: string | null, album: string | null) {
	const url: string = `${PUBLIC_SERVER_URL}/audio/edit`;

	try{
		const response: any = await axios.patch(url, {audioId, title, album})
		alert(response.data.message);
		return await getDefaultPlaylist();
	} catch (error: any){
		alert(error.response.data.message);
		
	}
}

///////// DELETE AUDIO /////////////
async function deleteAudio(audioId: number) {
	const url: string = `${PUBLIC_SERVER_URL}/audio/delete`;
	try {
		const response = await axios.delete(url, {data: audioId});
		alert(response.data.message);
    return await getDefaultPlaylist();
	} catch (error:any) {
		alert(error.response.data.message);
	}
};



export {createAudio, getAudio, editAudio, deleteAudio};