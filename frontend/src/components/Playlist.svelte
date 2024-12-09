<script lang="ts">
	import type { PlaylistType } from '@/data/types';
	import Audio from './Audio.svelte';
	import { getPlaylist, removeAudioFromPlaylist } from '@/apiCalls/playlistApiCalls';
	import { onMount } from 'svelte';

	// Props
	let playlist: PlaylistType;
	export let playlistId;

	// State
	let isEditMode = false;

	// Function to toggle edit mode
	function toggleEditMode() {
		isEditMode = !isEditMode;
	}

	// Handle audio removal with confirmation
	const handleRemoveAudio = async (event: CustomEvent) => {
		const audioId = event.detail;

		// Show confirmation prompt
		const confirmed = confirm('Are you sure you want to remove this song from the playlist?');
		if (!confirmed) return; // Exit if user cancels

		try {
			// Proceed with removal if confirmed
			playlist = await removeAudioFromPlaylist(playlistId, audioId);
		} catch (error) {
			console.error('Failed to remove audio:', error);
		}
	};

	onMount(async () => {
		playlist = await getPlaylist(playlistId);
	});
</script>

{#if !playlist}
	<div>Loading....</div>
{:else}
	<div class="p-4">
		<div class="flex items-center space-x-4 mb-4 justify-between">
			<div>
				<h2 class="text-2xl font-bold">{playlist.name}</h2>
				<p>Number of audio: {playlist.audios.length}</p>
			</div>
			<div class="flex space-x-2">
				<button
					class="bg-gray-500 hover:bg-gray-700 text-white font-bold py-1 px-3 rounded"
					on:click={toggleEditMode}
				>
					{isEditMode ? 'Done' : 'Edit'}
				</button>
			</div>
		</div>

		<ul class="space-y-2">
			{#each playlist.audios as audio}
				<Audio {audio} {isEditMode} on:removeAudio={handleRemoveAudio} />
			{/each}
		</ul>
	</div>
{/if}
