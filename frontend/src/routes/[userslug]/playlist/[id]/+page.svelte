<script lang="ts">
	import { onMount } from 'svelte';
	import { page } from '$app/stores';
	import { get } from 'svelte/store';
	import Playlist from '@/components/Playlist.svelte';

	// Define the playlist type
	type Song = {
		title: string;
		artist: string;
		url: string;
		// imgUrl: string;
	};

	type Playlist = {
		id: string;
		name: string;
		imgUrl: string;
		songs: Song[];
	};

	let playlist: Playlist | undefined;

	let params = get(page).params;

	// Sample playlists array
	const playlists: Playlist[] = [
		{
			id: '1',
			name: 'Chill Vibes',
			imgUrl: 'https://via.placeholder.com/150',
			songs: [
				{
					title: 'Song A',
					artist: 'Artist A',
					url: 'https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3'
				},
				{
					title: 'Song B',
					artist: 'Artist B',
					url: 'https://www.soundhelix.com/examples/mp3/SoundHelix-Song-2.mp3'
				}
			]
		},
		{
			id: '2',
			name: 'Workout Mix',
			imgUrl: 'https://via.placeholder.com/150',
			songs: [
				{
					title: 'Song C',
					artist: 'Artist C',
					url: 'https://www.soundhelix.com/examples/mp3/SoundHelix-Song-3.mp3'
				},
				{
					title: 'Song D',
					artist: 'Artist D',
					url: 'https://www.soundhelix.com/examples/mp3/SoundHelix-Song-4.mp3'
				}
			]
		}
	];

	// Function to find a playlist by ID
	function getPlaylistById(id: string): Playlist | undefined {
		return playlists.find((playlist) => playlist.id === id);
	}

	onMount(() => {
		playlist = getPlaylistById(params.id);
	});
</script>

<main class="pb-52">
	{#if playlist}
		<Playlist {playlist} />
	{:else}
		<p class="text-red-500">Playlist not found!</p>
	{/if}
</main>
