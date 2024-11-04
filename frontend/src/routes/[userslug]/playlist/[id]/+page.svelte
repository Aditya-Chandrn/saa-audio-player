<script lang="ts">
	import { onMount } from 'svelte';
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import { getPlaylist } from '@/apiCalls/playlistApiCalls';
	import { addAudioToPlaylist, removeAudioFromPlaylist } from '@/apiCalls/playlistApiCalls';
	import type { PlaylistType } from '@/data/types';
	import Playlist from '@/components/Playlist.svelte';
	import { derived, get } from 'svelte/store';
	import { page } from '$app/stores';

	let playlistId: number;
	let playlist: PlaylistType | null = null;
	let audioId: number = 0; // ID of the song to be added or removed

	// Fetch the playlist on mount and update the UI
	const fetchPlaylist = async () => {
		if (playlistId) {
			playlist = await getPlaylist(playlistId);
		}
	};

	onMount(() => {
		playlistId = Number(get(page).params.id);
		fetchPlaylist();
	});

	const handleRemoveSong = async (audioId: number) => {
		playlist = await removeAudioFromPlaylist(playlistId, audioId);
	};
</script>

<main class="pb-52">
	{#if playlistId && playlist}
		<Playlist {playlistId} />
	{:else}
		<p class="text-red-500">Playlist not found!</p>
	{/if}
</main>
