<script lang="ts">
	import { getAudio } from '@/apiCalls/audioApiCalls';
	import type { AudioType, PlaylistType } from '@/data/types';
	import { musicStore } from '@/stores/musicStore';

	export let audio: AudioType;

	// Function to handle play action
	const playAudio = async () => {
		try {
			const audioData = await getAudio(audio.audioId);
			console.log('Fetched audio data:', audioData);

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
</script>

<!-- svelte-ignore a11y-no-noninteractive-element-interactions -->
<!-- svelte-ignore a11y-click-events-have-key-events -->
<li class="flex items-center justify-between p-2 bg-gray-100 rounded w-[100%]" on:click={playAudio}>
	<div>
		<h3 class="font-semibold">{audio.title}</h3>
		<p class="text-gray-600">{audio.artist || 'Unknown Artist'}</p>
	</div>
</li>
