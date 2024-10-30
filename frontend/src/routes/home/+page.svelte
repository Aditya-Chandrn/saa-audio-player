<script lang="ts">
	import { deleteAudio } from '@/apiCalls/audioApiCalls';
	import { addAudioToPlaylist } from '@/apiCalls/playlistApiCalls';
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import Audio from '@/components/Audio.svelte';
	import type { PlaylistType, AudioType } from '@/data/types';
	import getDefaultPlaylist from '@/utils/getDefaultPlaylist';
	import { onMount } from 'svelte';

	let defaultPlaylist: PlaylistType;
	let allPlaylists: PlaylistType[] = []; // Store all playlists
	let showModal = false;
	let selectedAudio: AudioType | null = null;

	const removeAudio = async (audioId: number) => {
		try {
			defaultPlaylist = await deleteAudio(audioId);
		} catch (error) {
			console.error('Error deleting audio:', error);
		}
	};

	const addToPlaylist = (audio: AudioType) => {
		selectedAudio = audio;
		showModal = true;
	};

	const handleAddToPlaylist = async (playlistId: number, audioId: number) => {
		defaultPlaylist = await addAudioToPlaylist(playlistId, audioId);
		showModal = false;
	};

	onMount(async () => {
		defaultPlaylist = await getDefaultPlaylist();
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
						<Audio bind:audio />
						<button
							class="text-blue-700 hover:text-blue-900 m-5"
							on:click={() => addToPlaylist(audio)}
						>
							Add
						</button>
						<button
							class="text-red-500 hover:text-red-700 m-5"
							on:click={() => removeAudio(audio.audioId)}
						>
							Remove
						</button>

						{#if showModal}
							<div class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
								<div class="bg-white p-6 rounded shadow-lg">
									<h2 class="text-lg font-bold mb-4">Select Playlist</h2>
									<ul class="flex flex-col space-y-4">
										{#each allPlaylists as playlist (playlist.playlistId)}
											<li>
												<button
													class="text-blue-700"
													on:click={() => handleAddToPlaylist(playlist.playlistId, audio.audioId)}
												>
													{playlist.name}
												</button>
											</li>
										{/each}
									</ul>
									<button class="mt-4 text-red-500" on:click={() => (showModal = false)}>
										Close
									</button>
								</div>
							</div>
						{/if}
					</div>
				{/each}
			{/if}
		</ul>
	</div>
</div>
