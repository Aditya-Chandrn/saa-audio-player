<script>
	import Navbar from '@/components/Navbar.svelte';
	import { page } from '$app/stores';

	import '@/app.css';
	import Player from '@/components/Player.svelte';
</script>

<div class="flex flex-col min-h-screen">
	{#if !['/accounts/login', '/accounts/signup'].includes($page.url.pathname)}
		<header class="fixed w-full">
			<Navbar />
		</header>
	{/if}
	<main class="flex-grow mt-[80px] px-[10vw] py-[40px] h-full">
		<slot />
		
		{#if !['/accounts/login', '/accounts/signup'].includes($page.url.pathname)}
		<Player />
		{/if}
	</main>
</div>