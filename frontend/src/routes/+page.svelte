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
</div>
