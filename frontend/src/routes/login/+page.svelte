<script>
  import { login } from '@/apiCalls/userApiCalls';
  import { goto } from '$app/navigation';

  let username = '';
  let password = '';

  const handleLogin = async () => {
    if (username && password) {
      try {
        await login(username, password)
      } catch (error) {
        console.error('Login failed:', error);
        alert('Login failed. Please try again.');
      }
    } else {
      alert('Please fill in all fields');
    }
  };

  function redirectToSignup() {
    goto('/signup');
  }
</script>

<style>
.login-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
  background-color: #f0f4f8;
}

.login-box {
  width: 300px;
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
  background-color: #007BFF;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 10px;
}

button:hover {
  background-color: #0056b3;
}
</style>

<div class="login-container">
<div class="login-box">
  <h2>Login</h2>
  <input type="text" placeholder="Username" bind:value={username} />
  <input type="password" placeholder="Password" bind:value={password} />
  <button on:click={handleLogin}>Login</button>
  <button on:click={redirectToSignup}>Sign Up Page</button>
</div>
</div>
