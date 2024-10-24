<script lang="ts">
	import { addSong } from '@/apiCalls/audio/playlist';
	import Song from './Song.svelte';

	// Define types
	type Audio = {
		id: number;
		title: string;
		artist?: string;
		album?: string;
	};

	type Playlist = {
		id: number;
		name: string;
		imgUrl: string;
		audio: Audio[];
	};

	// Props
	export let playlist: Playlist;
	export let onSongRemove: (index: number) => void;

	// State
	let isEditMode = false;

	// Function to toggle edit mode
	function toggleEditMode() {
		isEditMode = !isEditMode;
	}

	// Function to add a new audio to the playlist
</script>

<div class="p-4">
	<div class="flex items-center space-x-4 mb-4 justify-between">
		<div>
			<img src={playlist.imgUrl} alt={playlist.name} class="w-32 h-32 object-cover rounded" />
			<div>
				<h2 class="text-2xl font-bold">{playlist.name}</h2>
				<p>Number of audio: {playlist.audio.length}</p>
			</div>
		</div>
		<div class="flex space-x-2">
			<button
				class="bg-gray-500 hover:bg-gray-700 text-white font-bold py-1 px-3 rounded"
				on:click={toggleEditMode}
			>
				{isEditMode ? 'Done' : 'Edit'}
			</button>
			{#if isEditMode}
				<button
					class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-3 rounded"
					on:click={() => {}}
				>
					+ Add Song
				</button>
			{/if}
		</div>
	</div>

	<ul class="space-y-2">
		{#each playlist.audio as audio, index}
			<Song {audio} {playlist} {index} {isEditMode} onRemove={() => onSongRemove(index)} />
		{/each}
	</ul>
</div>
