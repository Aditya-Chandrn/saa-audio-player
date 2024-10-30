import { goto } from '$app/navigation';
import { PUBLIC_SERVER_URL } from '$env/static/public';
import LocalStorage from '@/utils/cookiesManagement';
import axios from 'axios';
import { getPlaylist } from './playlistApiCalls';
import CookieStorage from '@/utils/cookiesManagement';
import { createAudioUrlFromBase64 } from '@/utils/base64ToAudio';
import getDefaultPlaylist from '@/utils/getDefaultPlaylist';

///////// CREATE AUDIO /////////////
async function createAudio(
	playlistId: number,
	audioBase64String: string,
	title: string,
	album: string | null
) {
	const userId: number = CookieStorage.get('user').userId;
	const url: string = `${PUBLIC_SERVER_URL}/audio/create`;
	try {
		const response: any = await axios.post(url, { userId, audioBase64String, title, album });
		alert(response.data.message);
		return await getPlaylist(playlistId);
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// GET AUDIO /////////////
// async function getAudio(audioId: number) {
// 	const url: string = `${PUBLIC_SERVER_URL}/audio/get`;
// 	try {
// 		const response = await axios.get(url, { params: { audioId } });

// 		if (response && response.data) {
// 			const { title, artist, audioBase64String } = response.data.audio;
// 			const audio = createAudioUrlFromBase64(audioBase64String);
// 			return { title, artist, audio };
// 		} else {
// 			throw new Error('Invalid response structure');
// 		}
// 	} catch (error: any) {
// 		console.error('Error fetching audio:', error);
// 	}
// }

///////// EDIT AUDIO /////////////

async function getAudio(audioId: number) {
	const url: string = `${PUBLIC_SERVER_URL}/audio/get`;
	try {
		const response = await axios.get(url, { params: { audioId } });

		if (response && response.data) {
			const { title, artist, audioBase64String } = response.data.audio;
			const audio = createAudioUrlFromBase64(audioBase64String);
			return { title, artist, url: audio };
		} else {
			throw new Error('Invalid response structure');
		}
	} catch (error: any) {
		console.error('Error fetching audio:', error);
		return null;
	}
}

async function editAudio(audioId: number, title: string | null, album: string | null) {
	const url: string = `${PUBLIC_SERVER_URL}/audio/edit`;

	try {
		const response: any = await axios.patch(url, { audioId, title, album });
		alert(response.data.message);
		return await getPlaylist(audioId);
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// DELETE AUDIO /////////////
async function deleteAudio(audioId: number) {
	const url: string = `${PUBLIC_SERVER_URL}/audio/delete`;
	try {
		// Wrap audioId in an object
		const response = await axios.delete(url, { data: { audioId } });
		alert(response.data.message);
		return await getDefaultPlaylist();
	} catch (error: any) {
		alert(error.response?.data?.message || 'An error occurred');
	}
}

export { createAudio, getAudio, editAudio, deleteAudio };
