<script lang="ts">
	import { goto } from '$app/navigation';
	import '@/app.css';
	import CookieStorage from '@/utils/cookiesManagement';
	import LocalStorage from '@/utils/cookiesManagement';

	interface NavOption {
		page: string;
		link: string;
	}

	let userSlug = 'suyesh';

	let navOptions: readonly NavOption[] = [
		{ page: 'Home', link: '/home' },
		{ page: 'Playlist', link: `/${userSlug}/playlist` },
	];

	function handleLogout() {
		CookieStorage.removeAll();
		goto('/account/login');
	}
</script>

<nav class="bg-theme h-[80px] flex items-center px-[4vw] bg-green-300">
	<ul class="flex flex-row gap-8">
		{#each navOptions as navOption}
			<li class="text-[1.4em]">
				<a href={navOption.link}>{navOption.page}</a>
			</li>
		{/each}
		<li class="text-[1.4em]">
			<button on:click={() => handleLogout()}>Logout</button>
		</li>
	</ul>
</nav>
