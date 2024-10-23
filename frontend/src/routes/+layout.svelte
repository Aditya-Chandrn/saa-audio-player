<script>
	import Navbar from '@/components/Navbar.svelte';
	import Footer from '@/components/Footer.svelte';
	import '@/app.css';
	import Player from '@/components/Player.svelte';
</script>

<div class="flex flex-col min-h-screen">
	<!-- <div class="flex flex-col min-h-screen bg-gray-900"> -->
	<!-- Navbar -->
	<header class="fixed w-full">
		<Navbar />
	</header>

	<!-- Main Content -->
	<main class="flex-grow mt-[80px] px-[10vw] py-[40px] h-full">
		<slot />
		<Player />
	</main>

	<!-- Footer -->
	<!-- <footer class="w-full">
		<Footer />
	</footer> -->
</div>
