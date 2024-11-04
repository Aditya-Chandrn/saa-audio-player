<script lang="ts">
	import { goto } from '$app/navigation';
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import type { PlaylistType } from '@/data/types';
	import { onMount } from 'svelte';
	
	export let data: { userslug: string };

	let playlists: PlaylistType[] = [];

	// Handle opening playlist
	function openPlaylist(playlistId: string) {
		goto(`/${data.userslug}/playlist/${playlistId}`);
	}

	const redirectToAddPlaylist = () => {
    	goto('/addPlaylist'); 
  	};

	onMount(async () => {
		playlists = await getUserPlaylists();
	});
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
				on:click={() => openPlaylist(playlist.playlistId.toString())}
				on:keydown={(e) =>
					(e.key === 'Enter' || e.key === ' ') && openPlaylist(playlist.playlistId.toString())}
			>
				<img
					class="w-full h-40 object-cover rounded-md mb-4"
					src={playlist.imageBase64String}
					alt={playlist.name}
				/>
				<h2 class="text-xl font-semibold">{playlist.name}</h2>
			</div>
		{/each}

		<button
			class="bg-gray-800 p-4 rounded-lg hover:bg-gray-700 transition cursor-pointer flex flex-col justify-center items-center"
			tabindex="0"
			aria-label="Add playlist"
			on:click={redirectToAddPlaylist}
		>
		<img
			class="w-20 h-20 object-cover rounded-full mb-4"
			src="/src/assets/add.png"
			alt="Add a new playlist"
		/>
			<h2 class="text-xl font-semibold text-center">Add playlist</h2>
		</button>
		
	</div>
</section>
