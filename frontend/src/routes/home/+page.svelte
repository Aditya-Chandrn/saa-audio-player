<script lang="ts">
	import { getAudio } from '@/apiCalls/audioApiCalls';
	import type { Playlist } from '@/data/types';
	import getDefaultPlaylist from '@/utils/getDefaultPlaylist';
	import { onMount } from 'svelte';

	let defaultPlaylist: Playlist;

	onMount(async () => {
		defaultPlaylist = await getDefaultPlaylist();
	});
</script>

<h1 class="text-2xl font-bold mb-4">All Songs</h1>
<div class="flex items-center justify-center">
	<div class="w-[70vw]">
		<ul class="flex flex-col space-y-4">
			{#if defaultPlaylist}
				{#each defaultPlaylist.audios as audio}
					<!-- svelte-ignore a11y-no-noninteractive-element-interactions -->
					<!-- svelte-ignore a11y-click-events-have-key-events -->
					<li
						class="flex items-center justify-between p-2 bg-gray-100 rounded"
						on:click={() => getAudio(audio.audioId)}
					>
						<h3 class="font-semibold">{audio.title}</h3>
						<p class="text-gray-600">{audio.album ? audio.album : 'No Album'}</p>
					</li>
				{/each}
			{/if}
		</ul>
	</div>
</div>
