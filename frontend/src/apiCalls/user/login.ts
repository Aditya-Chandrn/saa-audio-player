import { PUBLIC_SERVER_URL } from '$env/static/public';
import axios from 'axios';

export async function Login(username: string, email: string, password: string) {
	const url: string = `${PUBLIC_SERVER_URL}/user/login`;
	console.log(url);
	const response = await axios.post(url, {username, password});
	console.log(response);
}
