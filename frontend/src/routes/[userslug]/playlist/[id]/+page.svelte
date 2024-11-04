<script lang="ts">
	import { onMount } from 'svelte';
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import { getPlaylist } from '@/apiCalls/playlistApiCalls';
	import { addAudioToPlaylist, removeAudioFromPlaylist } from '@/apiCalls/playlistApiCalls';
	import type { PlaylistType } from '@/data/types';
	import Playlist from '@/components/Playlist.svelte';
	import { derived, get } from 'svelte/store';
	import { page } from '$app/stores';

	let playlistId: number;
	let playlist: PlaylistType | null = null;
	let audioId: number = 0; // ID of the song to be added or removed

	// Fetch the playlist on mount and update the UI
	const fetchPlaylist = async () => {
		if (playlistId) {
			playlist = await getPlaylist(playlistId);
		}
	};

	onMount(() => {
		playlistId = Number(get(page).params.id);
		fetchPlaylist();
	});

	const handleAddSong = async () => {
		if (audioId) {
			playlist = await addAudioToPlaylist(playlistId, audioId);
		} else {
			alert("Please enter a valid audio ID to add.");
		}
	};

	const handleRemoveSong = async (audioId: number) => {
		playlist = await removeAudioFromPlaylist(playlistId, audioId);
	};
</script>

<main class="pb-52">
	{#if playlistId && playlist}
		<Playlist {playlistId} />

		<!-- Input to add a song by audioId -->
		<div class="add-song-container">
			<input
				type="number"
				bind:value={audioId}
				placeholder="Enter Audio ID to Add"
				class="input-field"
			/>
			<button on:click={handleAddSong} class="add-button">Add Song</button>
		</div>

		<!-- Display playlist songs with a remove button for each -->
		<div class="song-list">
			{#each playlist.audios as song}
				<div class="song-item">
					<p>{song.title}</p>
					<button on:click={() => handleRemoveSong(song.audioId)} class="remove-button">Remove</button>
				</div>
			{/each}
		</div>
	{:else}
		<p class="text-red-500">Playlist not found!</p>
	{/if}
</main>

<style>
	.add-song-container {
		display: flex;
		gap: 10px;
		margin-top: 20px;
	}
	.input-field {
		padding: 8px;
		border: 1px solid #ccc;
		border-radius: 5px;
		width: 150px;
	}
	.add-button, .remove-button {
		background-color: #4CAF50;
		color: white;
		padding: 8px 12px;
		border: none;
		border-radius: 5px;
		cursor: pointer;
	}
	.add-button:hover {
		background-color: #45a049;
	}
	.remove-button {
		background-color: #ff4c4c;
	}
	.remove-button:hover {
		background-color: #d93636;
	}
	.song-list {
		margin-top: 20px;
	}
	.song-item {
		display: flex;
		justify-content: space-between;
		align-items: center;
		margin: 5px 0;
		padding: 10px;
		background-color: #f2f2f2;
		border-radius: 5px;
	}
</style>
