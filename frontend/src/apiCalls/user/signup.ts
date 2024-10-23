import { PUBLIC_SERVER_URL } from '$env/static/public';
import axios from 'axios';
import type { SignupResult } from './userDTO';

export async function Signup(username: string, email: string, password: string) {
	const response: SignupResult = await axios.post(`${PUBLIC_SERVER_URL}/user/signup`, {username, email, password});

	console.log(response.message);
}
