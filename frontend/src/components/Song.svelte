<script lang="ts">
	export let audio: { id: number; title: string; artist?: string; album?: string };
	export let playlist: {
		id: number;
		name: string;
		imgUrl: string;
		audio: { title: string; artist: string; url: string }[];
	};
	export let index: number;
	export let isEditMode: boolean;
	export let onRemove: () => void;
</script>

<li class="flex items-center justify-between p-2 bg-gray-100 rounded">
	<div>
		<h3 class="font-semibold">{audio.title}</h3>
		<p class="text-gray-600">{audio.artist}</p>
	</div>
	<div class="flex space-x-2">
		<button class=""> Play </button>
		{#if isEditMode}
			<button class="text-red-500 hover:text-red-700" on:click={onRemove}> Remove </button>
		{/if}
	</div>
</li>
