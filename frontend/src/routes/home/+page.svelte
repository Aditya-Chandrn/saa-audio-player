<script lang="ts">
	import { deleteAudio } from '@/apiCalls/audioApiCalls';
	import { addAudioToPlaylist } from '@/apiCalls/playlistApiCalls';
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import Audio from '@/components/Audio.svelte';
	import type { PlaylistType, AudioType } from '@/data/types';
	import { musicStore } from '@/stores/musicStore';
	import getDefaultPlaylist from '@/utils/getDefaultPlaylist';
	import { onMount } from 'svelte';

	let defaultPlaylist: PlaylistType;
	let allPlaylists: PlaylistType[] = [];
	let showModal = false;
	let selectedAudioId: number | null = null;
	let isEditMode: boolean = false;

	// Remove audio with confirmation prompt
	const removeAudio = async (audioId: number) => {
		const confirmed = confirm('Are you sure you want to remove this audio?');
		if (!confirmed) return; // Exit if user cancels

		try {
			await deleteAudio(audioId);
			defaultPlaylist = await getDefaultPlaylist(); // Refresh default playlist after deletion
		} catch (error) {
			console.error('Error deleting audio:', error);
		}
	};

	const controlModal = (audioId: number) => {
		selectedAudioId = audioId; // Store selected audio ID
		showModal = true;
	};

	const handleAddToPlaylist = async (playlistId: number) => {
		if (selectedAudioId === null) return;

		try {
			await addAudioToPlaylist(playlistId, selectedAudioId);
			showModal = false;
			defaultPlaylist = await getDefaultPlaylist(); // Refresh default playlist after adding
		} catch (error) {
			console.error('Error adding audio to playlist:', error);
		} finally {
			selectedAudioId = null; // Clear selected audio ID after operation
		}
	};

	onMount(async () => {
		defaultPlaylist = await getDefaultPlaylist();
		musicStore.loadPlaylist(defaultPlaylist.audios);
		allPlaylists = await getUserPlaylists();
	});
</script>

<h1 class="text-2xl font-bold mb-4">All Songs</h1>
<div class="flex items-center justify-center">
	<div class="w-[70vw]">
		<ul class="flex flex-col space-y-4">
			{#if defaultPlaylist}
				{#each defaultPlaylist.audios as audio (audio.audioId)}
					<div class="flex justify-between">
						<Audio bind:audio {isEditMode} />
						<button
							class="text-blue-700 hover:text-blue-900 m-5"
							on:click={() => controlModal(audio.audioId)}
						>
							Add
						</button>
						<button
							class="text-red-500 hover:text-red-700 m-5"
							on:click={() => removeAudio(audio.audioId)}
						>
							Remove
						</button>
					</div>
				{/each}
			{/if}
		</ul>
	</div>
</div>

{#if showModal}
	<script>
		export let allPlaylists = [];
		export let handleAddToPlaylist;
		export let showModal;
	</script>

	<div class="fixed inset-0 z-50 flex items-center justify-center">
		<!-- Overlay with backdrop blur -->
		<!-- svelte-ignore a11y-click-events-have-key-events -->
		<!-- svelte-ignore a11y-no-static-element-interactions -->
		<div
			class="absolute inset-0 bg-black/50 backdrop-blur-sm"
			on:click={() => (showModal = false)}
		/>

		<!-- Modal Content -->
		<div class="relative w-full max-w-md mx-4 bg-white rounded-lg shadow-xl">
			<!-- Header -->
			<div class="flex items-center justify-between p-6 border-b">
				<h2 class="text-xl font-semibold text-gray-900">Select Playlist</h2>
				<button
					on:click={() => (showModal = false)}
					class="inline-flex items-center justify-center rounded-full p-2 text-gray-400 hover:text-gray-500 hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-blue-500"
				>
					<svg
						xmlns="http://www.w3.org/2000/svg"
						class="w-5 h-5"
						viewBox="0 0 24 24"
						fill="none"
						stroke="currentColor"
						stroke-width="2"
					>
						<path d="M18 6L6 18M6 6l12 12" />
					</svg>
				</button>
			</div>

			<!-- Playlist List -->
			<div class="p-6">
				<ul class="divide-y divide-gray-200">
					{#each allPlaylists as playlist (playlist.playlistId)}
						<li>
							<button
								on:click={() => handleAddToPlaylist(playlist.playlistId)}
								class="w-full px-4 py-3 text-left text-sm hover:bg-gray-50 rounded-lg transition-colors duration-150 focus:outline-none focus:ring-2 focus:ring-blue-500"
							>
								<span class="font-medium text-gray-900">{playlist.name}</span>
							</button>
						</li>
					{/each}
				</ul>
			</div>

			<!-- Footer -->
			<div class="px-6 py-4 bg-gray-50 rounded-b-lg border-t">
				<button
					on:click={() => (showModal = false)}
					class="w-full px-4 py-2 text-sm font-medium text-gray-700 bg-white border border-gray-300 rounded-md shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500"
				>
					Cancel
				</button>
			</div>
		</div>
	</div>
{/if}
