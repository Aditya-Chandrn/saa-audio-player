<script lang="ts">
	import { onMount } from 'svelte';
	import Playlist from '@/components/Playlist.svelte';
	import { getDefaultPlaylist } from '@/apiCalls/userApiCalls';

	// Define the playlist and audio types
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

	let playlist: Playlist | undefined;

	// Function to remove a song from the playlist
	function removeSongFromPlaylist(index: number) {
		if (playlist) {
			playlist = {
				...playlist,
				audio: playlist.audio.filter((_, i) => i !== index)
			};
		}
	}

	// On mount, fetch the playlist and update the UI
	onMount(() => {
		getDefaultPlaylist();
	});
</script>

<main class="pb-52">
	{#if playlist}
		<Playlist {playlist} onSongRemove={removeSongFromPlaylist} />
	{:else}
		<p class="text-red-500">Playlist not found!</p>
	{/if}
</main>
