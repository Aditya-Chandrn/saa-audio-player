<script lang="ts">
	import '@/app.css';

	interface NavOption {
		page: string;
		link: string;
	}

	let userSlug = 'suyesh';

	let navOptions: readonly NavOption[] = [
		{ page: 'Music', link: '/music' },
		{ page: 'Playlist', link: `/${userSlug}/playlist` },
		{ page: 'Login', link: '/login' }
	];
</script>

<nav class="bg-theme h-[80px] flex items-center px-[4vw] bg-green-300">
	<ul class="flex flex-row gap-8">
		{#each navOptions as navOption}
			<li class="text-[1.4em]">
				<a href={navOption.link}>{navOption.page}</a>
			</li>
		{/each}
	</ul>
</nav>
