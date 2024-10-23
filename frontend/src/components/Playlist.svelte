<script lang="ts">
	import Song from './Song.svelte';

	export let playlist: {
		id: string;
		name: string;
		imgUrl: string;
		songs: { title: string; artist: string; url: string }[];
	};
</script>

<div>
	<img src={playlist.imgUrl} alt={playlist.name} />
	<h2>{playlist.name}</h2>
	<p>Number of songs: {playlist.songs.length}</p>
	<ul>
		{#each playlist.songs as song}
			<Song {song} />
		{/each}
	</ul>
</div>
