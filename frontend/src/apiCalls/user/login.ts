import { PUBLIC_SERVER_URL } from '$env/static/public';
import axios from 'axios';
import type { LoginResult } from './userDTO';

export async function Login(username: string, password: string) {
	const url: string = `${PUBLIC_SERVER_URL}/user/login`;
	console.log(url);
	const response:LoginResult = await axios.post(url, {username, password});
	console.log(response.message);
}
