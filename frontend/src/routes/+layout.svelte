<script>
	import Navbar from '@/components/Navbar.svelte';
	import { page } from '$app/stores';

	import '@/app.css';
	import Player from '@/components/Player.svelte';
</script>

<div class="flex flex-col min-h-screen">
	{#if $page.url.pathname == '/account/login' || $page.url.pathname == '/account/signup'}
		<slot />

	{:else}
		<header class="fixed w-full">
			<Navbar />
		</header>
		<main class="flex-grow mt-[80px] px-[10vw] py-[40px] h-full">
			<slot />
			<Player />
		</main>
	{/if}
</div>
