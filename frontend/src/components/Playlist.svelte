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

	onMount(async () => {
		playlist = await getPlaylist(playlistId);
	});

	// Function to add a new audio to the playlist
</script>

{#if !playlist}
	<div>Loading....</div>
{:else}
	<div class="p-4">
		<div class="flex items-center space-x-4 mb-4 justify-between">
			<div>
				<img
					src={playlist.imageBase64String}
					alt={playlist.name}
					class="w-32 h-32 object-cover rounded"
				/>
				<div>
					<h2 class="text-2xl font-bold">{playlist.name}</h2>
					<p>Number of audio: {playlist.audios.length}</p>
				</div>
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
				<Audio {audio} {isEditMode} />
			{/each}
		</ul>
	</div>
{/if}
