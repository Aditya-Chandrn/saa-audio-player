import { goto } from '$app/navigation';
import { PUBLIC_SERVER_URL } from '$env/static/public';
// import type { Playlist } from '@/data/types';
import CookieStorage from '@/utils/cookiesManagement';
import axios from 'axios';

///////// LOGIN /////////////
async function login(username: string, password: string) {
	const url: string = `${PUBLIC_SERVER_URL}/user/Login`;
	try{
		const response: any = await axios.post(url, {username, password})
		CookieStorage.set("user", response.data.user)
		goto("/home");
	} catch (error: any){
		alert(error.response.data.message);
	}
}

///////// SIGNUP /////////////
async function signup(username: string, password: string) {
	const url: string = `${PUBLIC_SERVER_URL}/user/signup`;

	try {
		const response: any = await axios.post(url, { username, password });
		CookieStorage.set('user', response.data.user);
		goto('/home');
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// EDIT USER /////////////
async function editUser(password: string | null) {
	const userId: number = CookieStorage.get("user").userId;
	const url: string = `${PUBLIC_SERVER_URL}/user/edit`;
	try {
		const response = await axios.patch(url, { userId, password });
		CookieStorage.set('user', response.data.user);
		alert(response.data.message);
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

///////// DELETE USER /////////////
async function deleteUser(){
	const userId: number = CookieStorage.get("user").userId;
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
async function getUserPlaylists(){
	const userId: number = CookieStorage.get("user").userId;
	const url: string = `${PUBLIC_SERVER_URL}/user/get-playlists`;
	try {
		const response = await axios.get(url, { params: { userId } });
		return response.data.playlists;
	} catch (error: any) {
		alert(error.response.data.message);
	}
}

export { login, signup, editUser, deleteUser, getUserPlaylists };
