<!-- AddToPlaylistModal.svelte -->
<script lang="ts">
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import type { PlaylistType } from '@/data/types';
	import { onMount } from 'svelte';
	export let audioId;
	export let isOpen = false;
	export let onClose;
	let playlists: PlaylistType[] = [];

	onMount(async () => {
		if (isOpen) {
			const response = await getUserPlaylists();
			if (response.ok) {
				playlists = await response.json();
			} else {
				console.error('Failed to fetch playlists');
			}
		}
	});

	const addToPlaylist = async (playlistId: number) => {
		// Add your logic to add the audio to the playlist here
		console.log(`Adding audio ${audioId} to playlist ${playlistId}`);
		// Optionally close the modal after adding
		onClose();
	};
</script>

<div class="modal" class:open={isOpen}>
	<div class="modal-content">
		<h2>Add to Playlist</h2>
		<button class="close-button" on:click={onClose}>Close</button>
		<ul>
			{#each playlists as playlist (playlist.playlistId)}
				<li>
					<button on:click={() => addToPlaylist(playlist.playlistId)}>
						{playlist.name}
					</button>
				</li>
			{/each}
		</ul>
	</div>
</div>

<style>
	.modal {
		position: fixed;
		top: 0;
		left: 0;
		right: 0;
		bottom: 0;
		background-color: rgba(0, 0, 0, 0.5);
		display: flex;
		justify-content: center;
		align-items: center;
		visibility: hidden;
		opacity: 0;
		transition:
			visibility 0s,
			opacity 0.5s;
	}
	.modal.open {
		visibility: visible;
		opacity: 1;
	}
	.modal-content {
		background-color: white;
		padding: 20px;
		border-radius: 8px;
		box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
	}
	.close-button {
		margin-bottom: 10px;
	}
</style>
