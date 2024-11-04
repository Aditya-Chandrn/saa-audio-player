<script lang="ts">
	import { getAudio } from '@/apiCalls/audioApiCalls';
	import { removeAudioFromPlaylist } from '@/apiCalls/playlistApiCalls';
	import type { AudioType } from '@/data/types';
	import { musicStore } from '@/stores/musicStore';
	import { createEventDispatcher } from 'svelte';

	export let audio: AudioType;
	export let isEditMode: boolean = false; // Updated to boolean

	const dispatch = createEventDispatcher();

	// Function to handle play action
	const playAudio = async () => {
		try {
			const audioData = await getAudio(audio.audioId);
			if (audioData) {
				musicStore.setCurrentAudio({
					audioId: audio.audioId,
					title: audioData.title || '',
					artist: audioData.artist || '',
					url: audioData.url || ''
				});
				musicStore.togglePlay();
			} else {
				alert('Audio data could not be retrieved.');
			}
		} catch (error) {
			console.error('Failed to fetch audio:', error);
		}
	};

	// Function to handle remove audio
	const removeAudio = () => {
		dispatch('removeAudio', audio.audioId);
	};
</script>

<li class="flex items-center justify-between p-2 bg-gray-100 rounded w-[100%]">
	<!-- svelte-ignore a11y-click-events-have-key-events -->
	<!-- svelte-ignore a11y-no-static-element-interactions -->
	<div on:click={playAudio}>
		<h3 class="font-semibold">{audio.title}</h3>
		<p class="text-gray-600">{audio.artist || 'Unknown Artist'}</p>
	</div>

	{#if isEditMode}
		<button class="text-red-500 hover:text-red-700 m-5" on:click={removeAudio}> Remove </button>
	{/if}
</li>
