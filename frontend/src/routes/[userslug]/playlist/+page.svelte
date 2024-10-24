<script lang="ts">
	import { goto } from '$app/navigation';
	export let data: { userslug: string };

	// Define Playlist type
	interface PlaylistList {
		id: number;
		name: string;
		description: string;
		imageUrl: string;
	}

	// Playlist data
	// let playlists: PlaylistList[] = [
	// 	{
	// 		id: 1,
	// 		name: 'Chill Vibes',
	// 		description: 'Relax and unwind with these chilled out tracks.',
	// 		imageUrl: 'https://via.placeholder.com/150'
	// 	},
	// 	{
	// 		id: 2,
	// 		name: 'Top Hits',
	// 		description: 'The hottest tracks right now.',
	// 		imageUrl: 'https://via.placeholder.com/150'
	// 	},
	// 	{
	// 		id: 3,
	// 		name: 'Indie Anthems',
	// 		description: 'The best indie music around.',
	// 		imageUrl: 'https://via.placeholder.com/150'
	// 	},
	// 	{
	// 		id: 4,
	// 		name: 'Workout Beats',
	// 		description: 'High-energy beats to power your workout.',
	// 		imageUrl: 'https://via.placeholder.com/150'
	// 	}
	// ];

	// Handle opening playlist
	function openPlaylist(playlistId: string) {
		goto(`/${data.userslug}/playlist/${playlistId}`);
	}
</script>

<!-- Playlist Section -->
<section class="p-8 text-white">
	<h1 class="text-3xl font-bold mb-6">Playlists for {data.userslug}</h1>

	<div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
		{#each playlists as playlist}
			<div
				class="bg-gray-800 p-4 rounded-lg hover:bg-gray-700 transition cursor-pointer"
				role="button"
				tabindex="0"
				aria-label={`Open playlist ${playlist.name}`}
				on:click={() => openPlaylist(playlist.id.toString())}
				on:keydown={(e) =>
					(e.key === 'Enter' || e.key === ' ') && openPlaylist(playlist.id.toString())}
			>
				<img
					class="w-full h-40 object-cover rounded-md mb-4"
					src={playlist.imageUrl}
					alt={playlist.name}
				/>
				<h2 class="text-xl font-semibold">{playlist.name}</h2>
				<p class="text-sm text-gray-400 mt-2">{playlist.description}</p>
			</div>
		{/each}
	</div>
</section>
