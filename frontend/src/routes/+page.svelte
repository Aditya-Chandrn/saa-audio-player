<script lang="ts">
	import { createEventDispatcher } from 'svelte';

	type Audio = {
		id: number;
		title: string;
		artist: string;
	};

	// List of all songs
	const songs: Audio[] = [
		{ id: 1, title: 'Song A', artist: 'Artist A' },
		{ id: 2, title: 'Song B', artist: 'Artist B' },
		{ id: 3, title: 'Song C', artist: 'Artist C' }
		// Add more songs here...
	];

	const dispatch = createEventDispatcher();

	// Function to handle adding a song
	function addSong(id: number) {
		dispatch('addSong', { id });
	}
</script>

<div>
	<h1 class="text-2xl font-bold">All Songs</h1>
	<ul>
		{#each songs as song}
			<li class="flex justify-between items-center p-2 border-b">
				<span>{song.title} - {song.artist}</span>
				<button on:click={() => addSong(song.id)} class="bg-green-500 text-white py-1 px-2 rounded">
					Add to Playlist
				</button>
			</li>
		{/each}
	</ul>
</div>
