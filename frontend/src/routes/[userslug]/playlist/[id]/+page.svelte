<script lang="ts">
	import { onMount } from 'svelte';
	import { getPlaylist } from '@/apiCalls/playlistApiCalls';
	import { removeAudioFromPlaylist } from '@/apiCalls/playlistApiCalls';
	import type { PlaylistType } from '@/data/types';
	import Playlist from '@/components/Playlist.svelte';
	import { get } from 'svelte/store';
	import { page } from '$app/stores';
	import { musicStore } from '@/stores/musicStore';

	let playlistId: number;
	let playlist: PlaylistType | null = null;

	// Fetch the playlist on mount and update the UI
	const fetchPlaylist = async () => {
		if (playlistId) {
			playlist = await getPlaylist(playlistId);
			musicStore.loadPlaylist(playlist?.audios || []);
		}
	};

	onMount(() => {
		playlistId = Number(get(page).params.id);
		fetchPlaylist();
	});
</script>

<main class="pb-52 ">
	{#if playlistId && playlist}
		<Playlist {playlistId} />
	{:else}
		<p class="text-red-500">Playlist not found!</p>
	{/if}
</main>
