<script lang="ts">
	import { goto } from '$app/navigation';
	import { getUserPlaylists } from '@/apiCalls/userApiCalls';
	import type { PlaylistType } from '@/data/types';
	import CookieStorage from '@/utils/cookiesManagement';
	import { onMount } from 'svelte';

	export let data: { userslug: string };

	let playlists: PlaylistType[] = [];

	function openPlaylist(playlistId: string) {
		goto(`/${data.userslug}/playlist/${playlistId}`);
	}

	const redirectToAddPlaylist = () => {
		goto('/addPlaylist');
	};

	onMount(async () => {
		playlists = await getUserPlaylists();
		const defaultPlaylistId = CookieStorage.get("user").defaultPlaylistId
		playlists = playlists.filter((p)=>p.playlistId !== defaultPlaylistId)
	});
</script>

<!-- Playlist Section -->
<section class="p-8 text-white">
	<div class="max-w-7xl mx-auto">
		<h1 class="text-4xl font-bold mb-8 bg-clip-text text-transparent">
			Playlists for {data.userslug}
		</h1>

		<div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
			{#each playlists as playlist}
				<div
					class="group bg-gray-800/50 backdrop-blur-sm p-6 rounded-xl hover:bg-gray-700/60
                    transition-all duration-300 cursor-pointer border border-gray-700/50
                     hover:shadow-lg hover:shadow-purple-500/10
                    hover:-translate-y-1"
					role="button"
					tabindex="0"
					aria-label={`Open playlist ${playlist.name}`}
					on:click={() => openPlaylist(playlist.playlistId.toString())}
					on:keydown={(e) =>
						(e.key === 'Enter' || e.key === ' ') && openPlaylist(playlist.playlistId.toString())}
				>
					<div class="space-y-4">
						<div
							class="w-full aspect-square bg-gray-700/50 rounded-lg flex items-center justify-center
                        group-hover:bg-gray-600/50 transition-colors duration-300"
						>
							<span class="text-4xl font-bold text-white transition-colors">
								{playlist.name[0].toUpperCase()}
							</span>
						</div>
						<h2 class="text-xl font-semibold truncate transition-colors">
							{playlist.name}
						</h2>
					</div>
				</div>
			{/each}

			<button
				class="group bg-gray-800/50 backdrop-blur-sm p-6 rounded-xl hover:bg-gray-700/60
                transition-all duration-300 cursor-pointer border border-gray-700/50
                 hover:shadow-lg hover:shadow-purple-500/10
                hover:-translate-y-1"
				tabindex="0"
				aria-label="Add playlist"
				on:click={redirectToAddPlaylist}
			>
				<div class="space-y-4">
					<div
						class="w-full aspect-square bg-gray-700/50 rounded-lg flex items-center justify-center
                    group-hover:bg-gray-600/50 transition-colors duration-300"
					>
						<img
							class="w-20 h-20 object-cover rounded-full mb-4"
							src="/src/assets/add.png"
							alt="Add a new playlist"
						/>
					</div>
					<h2 class="text-xl font-semibold text-center transition-colors">Add playlist</h2>
				</div>
			</button>
		</div>
	</div>
</section>
