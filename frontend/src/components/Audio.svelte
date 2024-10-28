<script lang="ts">
	import type { AudioType } from "@/data/types";

	export let audio: AudioType;
	// export let playlistId: number;
	export let isEditMode: boolean;
	export let removeAudio: () => void;
</script>

<li class="flex items-center justify-between p-2 bg-gray-100 rounded">
	<div>
		<h3 class="font-semibold">{audio.title}</h3>
		<p class="text-gray-600">{audio.artist}</p>
	</div>
	<div class="flex space-x-2">
		<button class=""> Play </button>
		{#if isEditMode}
			<button class="text-red-500 hover:text-red-700" on:click={removeAudio}> Remove </button>
		{/if}
	</div>
</li>
