<script lang="ts">
	import { createPlaylist } from '@/apiCalls/playlistApiCalls';

	let playlistName = '';
	let message = '';

	const handleAddPlaylist = async () => {
		if (playlistName) {
			try {
				// Call the API to create the playlist
				await createPlaylist(playlistName);
				message = 'Playlist created successfully!';
				playlistName = ''; // Reset the field after success
			} catch (error) {
				console.error('Error creating playlist:', error);
				message = 'Failed to create playlist.';
			}
		} else {
			message = 'Please enter the playlist name.';
		}
	};
</script>

<div class="container">
	<h2>Add a New Playlist</h2>
	<input class="input-field" type="text" placeholder="Playlist Name" bind:value={playlistName} />
	<button class="button" on:click={handleAddPlaylist}>Add Playlist</button>

	{#if message}
		<div class="message">{message}</div>
	{/if}
</div>

<style>
	.container {
		max-width: 500px;
		margin: 0 auto;
		padding: 20px;
		text-align: center;
		background-color: #f9f9f9;
		border-radius: 10px;
		box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
	}
	.input-field {
		width: 100%;
		padding: 10px;
		margin: 10px 0;
		border: 1px solid #ccc;
		border-radius: 5px;
	}
	.button {
		width: 100%;
		padding: 10px;
		background-color: #86efac;
		color: black;
		border: none;
		border-radius: 5px;
		cursor: pointer;
		margin-top: 10px;
	}
	.button:hover {
		background-color: #65ec96;
	}
	.message {
		margin-top: 15px;
		font-weight: bold;
	}
</style>
