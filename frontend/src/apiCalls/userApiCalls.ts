import { goto } from '$app/navigation';
import { PUBLIC_SERVER_URL } from '$env/static/public';
import LocalStorage from '@/utils/localStorage';
import axios from 'axios';

///////// LOGIN /////////////
async function login(username: string, password: string) {
	const url: string = `${PUBLIC_SERVER_URL}/user/Login`;
	try {
		const response: any = await axios.post(url, { username, password });
		LocalStorage.setItem('user', response.data.user);
		await getDefaultPlaylist();
		goto('/music');
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// SIGNUP /////////////
async function signup(username: string, password: string) {
	const url: string = `${PUBLIC_SERVER_URL}/user/signup`;

	try {
		const response: any = await axios.post(url, { username, password });
		LocalStorage.setItem('user', response.data.user);
		goto('/music');
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// EDIT USER /////////////
async function editUser(password: string | null) {
	const userId: number = LocalStorage.getItem('user').userId;
	const url: string = `${PUBLIC_SERVER_URL}/user/edit`;
	try {
		const response = await axios.patch(url, { userId, password });
		LocalStorage.setItem('user', response.data.user);
		alert(response.data.message);
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// DELETE USER /////////////
async function deleteUser() {
	const userId: number = LocalStorage.getItem('user').userId;
	const url: string = `${PUBLIC_SERVER_URL}/user/delete`;
	try {
		const response = await axios.delete(url, { data: userId });
		alert(response.data.message);
		goto('/login');
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// GET USER DEFAULT PLAYLIST /////////////
async function getDefaultPlaylist() {
	const userId: number = LocalStorage.getItem('user').userId;
	const url: string = `${PUBLIC_SERVER_URL}/user/get-playlists`;
	try {
		const response = await axios.get(url, { params: { userId } });
		LocalStorage.setItem('playlists', response.data.playlists);
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

export { login, signup, editUser, deleteUser, getDefaultPlaylist };
