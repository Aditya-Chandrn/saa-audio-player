<script lang="ts">
	import { onMount } from 'svelte';
	import { page } from '$app/stores';
	import { get } from 'svelte/store';
	import Playlist from '@/components/Playlist.svelte';

	// Define the playlist type
	type Audio = {
		id: number;
		title: string;
		artist: string;
	};

	type Playlist = {
		id: number;
		name: string;
		imgUrl: string;
		audio: Audio[];
	};

	let playlist: Playlist | undefined;
	let params = get(page).params;

	// Sample playlists array
	let playlists: Playlist[] = [
		{
			id: 1,
			name: 'Chill Vibes',
			imgUrl: 'https://via.placeholder.com/150',
			audio: [
				{
					id: 1,
					title: 'Song A',
					artist: 'Artist A'
				},
				{
					id: 2,
					title: 'Song B',
					artist: 'Artist B'
				}
			]
		},
		{
			id: 2,
			name: 'Workout Mix',
			imgUrl: 'https://via.placeholder.com/150',
			audio: [
				{
					id: 1,
					title: 'Song C',
					artist: 'Artist C'
				},
				{
					id: 2,
					title: 'Song D',
					artist: 'Artist D'
				}
			]
		}
	];

	// Function to find a playlist by ID
	function getPlaylistById(id: number): Playlist | undefined {
		return playlists.find((playlist) => playlist.id === id);
	}

	// Function to add a new audio to the playlist
	function addAudioToPlaylist(newSong: Audio) {
		if (playlist) {
			playlist = {
				...playlist,
				audio: [...playlist.audio, newSong]
			};
			// Update the playlists array as well
			playlists = playlists.map((p) => (p.id === playlist?.id ? playlist : p));
		}
	}

	// Function to remove a audio from the playlist
	function removeSongFromPlaylist(index: number) {
		if (playlist) {
			playlist = {
				...playlist,
				audio: playlist.audio.filter((_, i) => i !== index)
			};
			// Update the playlists array as well
			playlists = playlists.map((p) => (p.id === playlist?.id ? playlist : p));
		}
	}

	onMount(() => {
		const playlistId = Number(params.id);
		playlist = getPlaylistById(playlistId);
	});
</script>

<main class="pb-52">
	{#if playlist}
		<Playlist {playlist} onAudioAdd={addAudioToPlaylist} onSongRemove={removeSongFromPlaylist} />
	{:else}
		<p class="text-red-500">Playlist not found!</p>
	{/if}
</main>
