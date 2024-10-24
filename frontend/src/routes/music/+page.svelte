<script lang="ts">
	type Audio = {
		id: number;
		title: string;
		artist?: string;
		album?: string;
	};

	// List of all songs
	const audios: Audio[] = [
		{ id: 1, title: 'Song A', artist: 'Artist A', album: 'album1' },
		{ id: 2, title: 'Song B', artist: 'Artist B' },
		{ id: 3, title: 'Song C', artist: 'Artist C' }
	];
</script>

<h1 class="text-2xl font-bold mb-4">All Songs</h1>
<div class="flex items-center justify-center">
	<div class="w-[70vw]">
		<ul class="flex flex-col space-y-4">
			{#each audios as audio}
				<!-- svelte-ignore a11y-click-events-have-key-events -->
				<!-- svelte-ignore a11y-no-noninteractive-element-interactions -->
				<li class="flex items-center justify-between p-2 bg-gray-100 rounded">
					<h3 class="font-semibold">{audio.title}</h3>
					<p class="text-gray-600">{audio.album ? audio.album : 'No Album'}</p>
					<button on:click={() => {}} class="bg-green-500 text-white py-1 px-2 rounded">
						Add to Playlist
					</button>
				</li>
			{/each}
		</ul>
	</div>
</div>
