<script lang="ts">
	import { onMount } from 'svelte';
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import type { PlaylistType } from '@/data/types';
	import Playlist from '@/components/Playlist.svelte';
	import { derived, get } from 'svelte/store';
	import { page } from '$app/stores';

	let playlistId: number;

	// On mount, fetch the playlist and update the UI
	onMount(() => {
		playlistId = Number(get(page).params.id);
		console.log(playlistId)
	})
</script>

<main class="pb-52">
	{#if playlistId}
		<Playlist {playlistId} />
	{:else}
		<p class="text-red-500">Playlist not found!</p>
	{/if}
</main>
