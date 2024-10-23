<script lang="ts">
	import Song from './Song.svelte';

	export let playlist: {
		id: string;
		name: string;
		imgUrl: string;
		songs: { title: string; artist: string; url: string }[];
	};
</script>

<div class="p-4">
	<div class="flex items-center space-x-4 mb-4">
		<img src={playlist.imgUrl} alt={playlist.name} class="w-32 h-32 object-cover rounded" />
		<div>
			<h2 class="text-2xl font-bold">{playlist.name}</h2>
			<p>Number of songs: {playlist.songs.length}</p>
		</div>
	</div>

	<ul class="space-y-2">
		{#each playlist.songs as song, index}
			<Song {song} {playlist} {index} />
		{/each}
	</ul>
</div>
