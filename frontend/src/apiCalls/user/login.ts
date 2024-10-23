import { PUBLIC_SERVER_URL } from '$env/static/public';
import axios from 'axios';

export async function Login(username: string, password: string) {
	try {
		const response = await axios.post(
			`${PUBLIC_SERVER_URL}/user/login`,
			{
				username,
				password
			},
			{
				headers: { 'Content-Type': 'application/json' }
			}
		);
		console.log(response.data);
	} catch (error) {
		console.error('Login Error');
	}
}
