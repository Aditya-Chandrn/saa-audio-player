<script lang="ts">
	import { onMount, onDestroy } from 'svelte';
	import { musicStore } from '../stores/musicStore';
	import type { AudioType } from '@/data/types';

	let isPlaying = false;
	let currentTime = 0;
	let duration = 240;
	let volume = 1;
	let audio: HTMLAudioElement;
	let isDragging = false;
	let currentAudio: AudioType | null = null;
	let currentIndex = -1;
	let playlist: AudioType[] = [];

	// Play and pause helper functions
	async function safePlay() {
		try {
			await audio.play();
		} catch (error) {
			console.error('Failed to play audio:', error);
		}
	}

	async function safePause() {
		try {
			audio.pause();
		} catch (error) {
			console.error('Failed to pause audio:', error);
		}
	}

	// Setup event listeners on mount
	onMount(() => {
		audio = new Audio();
		audio.addEventListener('loadedmetadata', () => {
			duration = audio.duration || 240;
		});
		audio.addEventListener('timeupdate', () => {
			if (!isDragging) currentTime = audio.currentTime;
		});
		audio.addEventListener('ended', async () => {
			await safePause();
			musicStore.playNext();
		});
		audio.volume = volume;
	});

	// Subscribe to store updates
	const unsubscribe = musicStore.subscribe(async (state) => {
		const previousUrl = currentAudio?.url;
		currentAudio = state.currentAudio;
		isPlaying = state.isPlaying;
		currentIndex = state.currentIndex;
		playlist = state.playlist;

		// Check if currentAudio has changed
		if (audio && currentAudio?.url) {
			const isNewTrack = previousUrl !== currentAudio.url;
			if (isNewTrack) {
				await safePause(); // Stop previous track
				audio.src = currentAudio.url; // Update audio source
				audio.currentTime = 0;
				currentTime = 0;
				if (isPlaying) await safePlay(); // Play only if isPlaying is true
			} else {
				// For play/pause toggles
				if (isPlaying && audio.paused) await safePlay();
				else if (!isPlaying && !audio.paused) await safePause();
			}
		}
	});

	// Clean up on destroy
	onDestroy(() => {
		unsubscribe();
		if (audio) {
			safePause().then(() => {
				audio.src = '';
				audio = null;
			});
		}
	});

	// Enhanced Previous Song
	async function prevSong() {
		if (playlist.length > 0 && currentIndex > 0) {
			await safePause();
			musicStore.playPrevious();
		} else {
			console.warn('Cannot go to the previous song: Playlist is empty or at the start');
		}
	}

	// Enhanced Next Song
	async function nextSong() {
		if (playlist.length > 0 && currentIndex < playlist.length - 1) {
			await safePause();
			musicStore.playNext();
		} else {
			console.warn('Cannot go to the next song: Playlist is empty or at the end');
		}
	}

	function formatTime(seconds: number) {
		const mins = Math.floor(seconds / 60);
		const secs = Math.floor(seconds % 60);
		return `${mins}:${secs < 10 ? '0' : ''}${secs}`;
	}

	async function updateCurrentTime(e: Event) {
		const target = e.target as HTMLInputElement;
		const newTime = parseFloat(target.value);
		currentTime = newTime;
		if (audio) {
			await safePause();
			audio.currentTime = newTime;
			if (isPlaying) await safePlay();
		}
	}

	function handleSliderMouseDown() {
		isDragging = true;
	}

	async function handleSliderMouseUp() {
		isDragging = false;
		if (isPlaying) await safePlay();
	}

	function updateVolume(e: Event) {
		const target = e.target as HTMLInputElement;
		volume = parseFloat(target.value);
		if (audio) audio.volume = volume;
	}

	async function handleKeydown(e: KeyboardEvent) {
		if (e.code === 'Space') {
			e.preventDefault();
			musicStore.togglePlay();
		} else if (e.code === 'ArrowLeft' && audio) {
			await safePause();
			audio.currentTime = Math.max(0, audio.currentTime - 5);
			if (isPlaying) await safePlay();
		} else if (e.code === 'ArrowRight' && audio) {
			await safePause();
			audio.currentTime = Math.min(duration, audio.currentTime + 5);
			if (isPlaying) await safePlay();
		}
	}
</script>

<svelte:window on:keydown={handleKeydown} />

<div
	class="bg-gray-900 text-white fixed bottom-0 left-0 w-full p-4 flex flex-col items-center h-40"
>
	<div class="flex flex-col items-center">
		<h2 class="text-3xl font-semibold">
			{#if currentAudio}
				{currentAudio.title}
			{:else}
				No songs selected
			{/if}
		</h2>
	</div>

	<div class="w-[90vw] mt-4 flex items-center justify-between">
		<div class="flex justify-center mt-1 space-x-4 pr-4">
			<button on:click={prevSong} class="p-2" aria-label="Previous Song" disabled={!currentAudio}>
				<svg
					fill="#ffffff"
					width="30px"
					height="30px"
					viewBox="0 0 32 32"
					enable-background="new 0 0 32 32"
					version="1.1"
					xml:space="preserve"
					xmlns="http://www.w3.org/2000/svg"
					xmlns:xlink="http://www.w3.org/1999/xlink"
					stroke="#ffffff"
					><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g
						id="SVGRepo_tracerCarrier"
						stroke-linecap="round"
						stroke-linejoin="round"
					></g><g id="SVGRepo_iconCarrier">
						<g id="play"></g> <g id="stop"></g> <g id="pause"></g> <g id="replay"></g>
						<g id="next"></g>
						<g id="Layer_8">
							<g>
								<g>
									<path
										d="M27.136,3.736C27.508,3.332,28,3.45,28,4v24c0,0.55-0.492,0.668-0.864,0.264L16.449,16.736 c-0.372-0.405-0.325-1.068,0.047-1.473L27.136,3.736z"
									></path>
									<path
										d="M27.602,29.504c-0.441,0-0.868-0.2-1.202-0.563L15.715,17.416c-0.718-0.781-0.697-2.022,0.044-2.829L26.401,3.058 c0.333-0.362,0.76-0.562,1.201-0.562C28.399,2.496,29,3.143,29,4v24C29,28.857,28.399,29.504,27.602,29.504z M27,5.358 l-9.77,10.584c-0.036,0.04-0.044,0.109-0.036,0.132L27,26.646V5.358z"
									></path>
								</g>
								<g>
									<path
										d="M14.297,3.736C14.669,3.332,15,3.45,15,4v24c0,0.55-0.331,0.668-0.703,0.264L3.69,16.736 c-0.372-0.405-0.365-1.068,0.007-1.473L14.297,3.736z"
									></path>
									<path
										d="M14.706,29.504c-0.286,0-0.717-0.098-1.146-0.563L2.954,17.414c-0.727-0.791-0.724-2.032,0.006-2.827l10.6-11.527 c0.428-0.466,0.859-0.563,1.146-0.563C15.329,2.496,16,2.967,16,4v24C16,29.033,15.329,29.504,14.706,29.504z M14,5.538 L4.433,15.94c-0.025,0.027-0.023,0.102-0.006,0.119L14,26.463V5.538z"
									></path>
								</g>
							</g>
						</g> <g id="search"></g> <g id="list"></g> <g id="love"></g> <g id="menu"></g>
						<g id="add"></g> <g id="headset"></g> <g id="random"></g> <g id="music"></g>
						<g id="setting"></g> <g id="Layer_17"></g> <g id="Layer_18"></g> <g id="Layer_19"></g>
						<g id="Layer_20"></g> <g id="Layer_21"></g> <g id="Layer_22"></g> <g id="Layer_23"></g>
						<g id="Layer_24"></g> <g id="Layer_25"></g> <g id="Layer_26"></g>
					</g></svg
				>
			</button>

			<button
				on:click={() => musicStore.togglePlay()}
				class="p-2"
				aria-label="Play/Pause"
				disabled={!currentAudio}
			>
				{#if $musicStore.isPlaying}
					<svg
						fill="#ffffff"
						width="30px"
						height="30px"
						viewBox="0 0 32 32"
						enable-background="new 0 0 32 32"
						version="1.1"
						xml:space="preserve"
						xmlns="http://www.w3.org/2000/svg"
						xmlns:xlink="http://www.w3.org/1999/xlink"
						stroke="#ffffff"
						><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g
							id="SVGRepo_tracerCarrier"
							stroke-linecap="round"
							stroke-linejoin="round"
						></g><g id="SVGRepo_iconCarrier">
							<g id="play"></g> <g id="stop"></g>
							<g id="pause">
								<g>
									<path
										d="M6,4C5.45,4,5,4.45,5,5v22c0,0.55,0.45,1,1,1h3c0.55,0,1-0.45,1-1V5c0-0.55-0.45-1-1-1H6z"
									></path>
									<path
										d="M9,29H6c-1.103,0-2-0.897-2-2V5c0-1.103,0.897-2,2-2h3c1.103,0,2,0.897,2,2v22C11,28.103,10.103,29,9,29z M9,27v1V27L9,27 L9,27z M6,5v22h2.997L9,5H6z"
									></path>
								</g>
								<g>
									<path
										d="M23,4c-0.55,0-1,0.45-1,1v22c0,0.55,0.45,1,1,1h3c0.55,0,1-0.45,1-1V5c0-0.55-0.45-1-1-1H23z"
									></path>
									<path
										d="M26,29h-3c-1.103,0-2-0.897-2-2V5c0-1.103,0.897-2,2-2h3c1.103,0,2,0.897,2,2v22C28,28.103,27.103,29,26,29z M26,27v1V27 L26,27L26,27z M23,5v22h2.997L26,5H23z"
									></path>
								</g>
							</g> <g id="replay"></g> <g id="next"></g> <g id="Layer_8"></g> <g id="search"></g>
							<g id="list"></g> <g id="love"></g> <g id="menu"></g> <g id="add"></g>
							<g id="headset"></g> <g id="random"></g> <g id="music"></g> <g id="setting"></g>
							<g id="Layer_17"></g> <g id="Layer_18"></g> <g id="Layer_19"></g>
							<g id="Layer_20"></g> <g id="Layer_21"></g> <g id="Layer_22"></g>
							<g id="Layer_23"></g> <g id="Layer_24"></g> <g id="Layer_25"></g>
							<g id="Layer_26"></g>
						</g></svg
					>
				{:else}
					<svg
						fill="#ffffff"
						width="30px"
						height="30px"
						viewBox="0 0 32 32"
						enable-background="new 0 0 32 32"
						version="1.1"
						xml:space="preserve"
						xmlns="http://www.w3.org/2000/svg"
						xmlns:xlink="http://www.w3.org/1999/xlink"
						stroke="#ffffff"
						><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g
							id="SVGRepo_tracerCarrier"
							stroke-linecap="round"
							stroke-linejoin="round"
						></g><g id="SVGRepo_iconCarrier">
							<g id="play">
								<g>
									<path
										d="M4.993,2.496C4.516,2.223,4,2.45,4,3v26c0,0.55,0.516,0.777,0.993,0.504l22.826-13.008 c0.478-0.273,0.446-0.719-0.031-0.992L4.993,2.496z"
									></path>
									<path
										d="M4.585,30.62L4.585,30.62C3.681,30.62,3,29.923,3,29V3c0-0.923,0.681-1.62,1.585-1.62c0.309,0,0.621,0.085,0.904,0.248 l22.794,13.007c0.559,0.319,0.878,0.823,0.878,1.382c0,0.548-0.309,1.039-0.847,1.347L5.488,30.373 C5.206,30.534,4.894,30.62,4.585,30.62z M5,3.651v24.698l21.655-12.34L5,3.651z"
									></path>
								</g>
							</g> <g id="stop"></g> <g id="pause"></g> <g id="replay"></g> <g id="next"></g>
							<g id="Layer_8"></g> <g id="search"></g> <g id="list"></g> <g id="love"></g>
							<g id="menu"></g> <g id="add"></g> <g id="headset"></g> <g id="random"></g>
							<g id="music"></g> <g id="setting"></g> <g id="Layer_17"></g> <g id="Layer_18"></g>
							<g id="Layer_19"></g> <g id="Layer_20"></g> <g id="Layer_21"></g>
							<g id="Layer_22"></g> <g id="Layer_23"></g> <g id="Layer_24"></g>
							<g id="Layer_25"></g> <g id="Layer_26"></g>
						</g></svg
					>
				{/if}
			</button>

			<button on:click={nextSong} class="p-2" aria-label="Next Song" disabled={!currentAudio}>
				<svg
					fill="#ffffff"
					width="30px"
					height="30px"
					viewBox="0 0 32 32"
					enable-background="new 0 0 32 32"
					version="1.1"
					xml:space="preserve"
					xmlns="http://www.w3.org/2000/svg"
					xmlns:xlink="http://www.w3.org/1999/xlink"
					stroke="#ffffff"
					><g id="SVGRepo_bgCarrier" stroke-width="0"></g><g
						id="SVGRepo_tracerCarrier"
						stroke-linecap="round"
						stroke-linejoin="round"
					></g><g id="SVGRepo_iconCarrier">
						<g id="play"></g> <g id="stop"></g> <g id="pause"></g> <g id="replay"></g>
						<g id="next">
							<g>
								<g>
									<path
										d="M4.561,3.728C4.184,3.328,4,3.45,4,4v24c0,0.55,0.184,0.672,0.561,0.272l10.816-11.544 c0.377-0.4,0.408-1.056,0.031-1.456L4.561,3.728z"
									></path>
									<path
										d="M4.202,29.507L4.202,29.507C4.079,29.507,3,29.465,3,28V4c0-1.465,1.079-1.507,1.202-1.507 c0.568,0,0.958,0.414,1.087,0.55l10.848,11.545c0.725,0.77,0.711,2.038-0.031,2.826L5.29,28.956 C5.16,29.094,4.771,29.507,4.202,29.507z M5.004,5.66L5,26.337l9.674-10.389L5.004,5.66z"
									></path>
								</g>
								<g>
									<path
										d="M17.561,3.728C17.184,3.328,17,3.45,17,4v24c0,0.55,0.184,0.672,0.561,0.272l10.816-11.544 c0.377-0.4,0.408-1.056,0.031-1.456L17.561,3.728z"
									></path>
									<path
										d="M17.202,29.507L17.202,29.507C17.079,29.507,16,29.465,16,28V4c0-1.465,1.079-1.507,1.202-1.507 c0.568,0,0.958,0.414,1.087,0.55l10.848,11.545c0.725,0.77,0.711,2.038-0.031,2.826L18.29,28.956 C18.16,29.094,17.771,29.507,17.202,29.507z M18.004,5.66L18,26.337l9.674-10.389L18.004,5.66z"
									></path>
								</g>
							</g>
						</g> <g id="Layer_8"></g> <g id="search"></g> <g id="list"></g> <g id="love"></g>
						<g id="menu"></g> <g id="add"></g> <g id="headset"></g> <g id="random"></g>
						<g id="music"></g> <g id="setting"></g> <g id="Layer_17"></g> <g id="Layer_18"></g>
						<g id="Layer_19"></g> <g id="Layer_20"></g> <g id="Layer_21"></g> <g id="Layer_22"></g>
						<g id="Layer_23"></g> <g id="Layer_24"></g> <g id="Layer_25"></g> <g id="Layer_26"></g>
					</g></svg
				>
			</button>
		</div>

		<div class="flex-grow mr-4">
			<input
				type="range"
				min="0"
				max={duration}
				value={currentTime}
				on:input={updateCurrentTime}
				on:mousedown={handleSliderMouseDown}
				on:mouseup={handleSliderMouseUp}
				class="w-full h-2 bg-gray-600 rounded-lg appearance-none cursor-pointer"
				disabled={!currentAudio}
			/>
			<div class="flex justify-between text-sm">
				<span>{formatTime(currentTime)}</span>
				<span>{formatTime(duration)}</span>
			</div>
		</div>

		<div class="flex items-center space-x-2 pl-4">
			<span class="text-lg">🔈</span>
			<input
				type="range"
				min="0"
				max="1"
				step="0.01"
				value={volume}
				on:input={updateVolume}
				class="w-24 h-1 bg-gray-600 rounded-lg appearance-none cursor-pointer"
			/>
		</div>
	</div>
</div>
