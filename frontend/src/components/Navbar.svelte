<script lang="ts">
	import '@/app.css';
	import { Login } from '@/apiCalls/user/login';

	interface NavOption {
		page: string;
		link: string;
	}

	let userSlug = 'suyesh';

	let navOptions: readonly NavOption[] = [
		{ page: 'Music', link: '/music' },
		{ page: 'Playlist', link: `/${userSlug}/playlist` }
	];

	const handleBtnClick = () => {
		Login('Suyesh', '123');
	};
</script>

<nav class="bg-theme h-[80px] flex items-center px-[4vw] bg-green-300">
	<ul class="flex flex-row gap-8">
		{#each navOptions as navOption}
			<li class="text-[1.4em]">
				<a href={navOption.link}>{navOption.page}</a>
			</li>
		{/each}
		<button on:click={handleBtnClick}>Login</button>
	</ul>
</nav>
