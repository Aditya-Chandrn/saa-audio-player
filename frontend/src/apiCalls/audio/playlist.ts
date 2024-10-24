import { PUBLIC_SERVER_URL } from '$env/static/public';
import axios from 'axios';

interface Audio {
	id: number;
	title: string;
	artist: string;
	url: string;
}

interface Playlist {
	id: number;
	name: string;
	imgUrl: string;
	audio: Audio[];
}

export async function getPlaylist(playlistId: number): Promise<Playlist> {
	try {
		const response = await axios.get(`${PUBLIC_SERVER_URL}/playlist/get`);
		return response.data;
	} catch (error) {
		console.error('Error fetching playlist:', error);
		throw error;
	}
}

export async function addSong(playlistId: number, audio: Audio): Promise<Audio> {
	try {
		const response = await axios.post(`${PUBLIC_SERVER_URL}/playlist/add-audio`);
		return response.data;
	} catch (error) {
		console.error('Error adding audio:', error);
		throw error;
	}
}

export async function removeSong(playlistId: number, audioIndex: number): Promise<void> {
	try {
		await axios.delete(`${PUBLIC_SERVER_URL}/playlist/remove-audio`);
	} catch (error) {
		console.error('Error removing audio:', error);
		throw error;
	}
}
