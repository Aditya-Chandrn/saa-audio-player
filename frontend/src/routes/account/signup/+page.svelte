<script>
	import { signup } from '@/apiCalls/userApiCalls';
	import { goto } from '$app/navigation';

	let username = '';
	let email = '';
	let password = '';
	let confirmPassword = '';

	const handleSignup = () => {
		if (password !== confirmPassword) {
			alert('Passwords do not match');
		} else if (username && email && password && confirmPassword) {
			alert(`Signing up with Username: ${username}, Email: ${email}`);

			signup(username, password);
		} else {
			alert('Please fill in all fields');
		}
	};

	function redirectToLogin() {
		goto('/account/login');
	}
</script>

<div class="signup-container">
	<div class="signup-box">
		<h2>Signup</h2>
		<input type="text" placeholder="Username" bind:value={username} />
		<input type="email" placeholder="Email" bind:value={email} />
		<input type="password" placeholder="Password" bind:value={password} />
		<input type="password" placeholder="Confirm Password" bind:value={confirmPassword} />
		<button on:click={handleSignup}>Signup</button>
		<button on:click={redirectToLogin}>Login Page</button>
	</div>
</div>

<style>
	.signup-container {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		height: 100vh;
		background-color: #f7fafc;
	}

	.signup-box {
		width: 350px;
		padding: 20px;
		background-color: #fff;
		border-radius: 10px;
		box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
	}

	input {
		width: 100%;
		padding: 10px;
		margin: 10px 0;
		border: 1px solid #ccc;
		border-radius: 5px;
	}

	button {
		width: 100%;
		padding: 10px;
		background-color: #28a745;
		color: white;
		border: none;
		border-radius: 5px;
		cursor: pointer;
		margin-top: 10px;
	}

	button:hover {
		background-color: #218838;
	}
</style>
