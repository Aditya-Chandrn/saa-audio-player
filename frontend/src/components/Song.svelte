<script lang="ts">
	export let song: { title: string; artist: string; url: string };
</script>

<li>
	<span>{song.title} - {song.artist}</span>
</li>
