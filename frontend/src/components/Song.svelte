<script lang="ts">
	import { musicStore } from '../stores/musicStore';

	export let song: { title: string; artist: string; url: string };
	export let playlist: { songs: (typeof song)[] };
	export let index: number;

	function handleClick() {
		musicStore.playSong(song, playlist.songs, index);
	}
</script>

<li class="p-2 hover:bg-gray-300 cursor-pointer rounded transition-colors" on:click={handleClick}>
	<div class="flex items-center justify-between">
		<span>{song.title} - {song.artist}</span>
		<span class="text-gray-400">#{index + 1}</span>
	</div>
</li>
