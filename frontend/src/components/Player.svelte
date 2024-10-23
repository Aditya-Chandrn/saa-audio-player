<script lang="ts">
	import { onMount, onDestroy } from 'svelte';
	import { musicStore } from '../stores/musicStore';

	// Types
	interface Song {
		title: string;
		artist: string;
		url?: string;
	}

	// Props and state
	export let song: Song = {
		title: 'Song Title',
		artist: 'Artist Name',
		url: 'https://example.com/song.mp3' // Replace with actual song URL
	};

	let isPlaying = false;
	let currentTime = 0;
	let duration = 240;
	let volume = 1;
	let audio: HTMLAudioElement;
	let isDragging = false;

	let currentSong: { title: string; artist: string; url: string } | null = null;

	const unsubscribe = musicStore.subscribe((state) => {
		currentSong = state.currentSong;
		isPlaying = state.isPlaying;

		if (audio && state.currentSong) {
			if (audio.src !== state.currentSong.url) {
				audio.src = state.currentSong.url;
				if (state.isPlaying) audio.play();
			}
			if (state.isPlaying && audio.paused) {
				audio.play();
			} else if (!state.isPlaying && !audio.paused) {
				audio.pause();
			}
		}
	});

	onMount(() => {
		audio = new Audio();

		audio.addEventListener('loadedmetadata', () => {
			duration = audio.duration || 240;
		});

		audio.addEventListener('timeupdate', () => {
			if (!isDragging) {
				currentTime = audio.currentTime;
			}
		});

		audio.addEventListener('ended', () => {
			musicStore.playNext();
		});

		audio.volume = volume;
	});

	onDestroy(() => {
		unsubscribe();
		if (audio) {
			audio.pause();
			audio.src = '';
			audio.removeEventListener('loadedmetadata', () => {});
			audio.removeEventListener('timeupdate', () => {});
			audio.removeEventListener('ended', () => {});
		}
	});

	function togglePlay() {
		musicStore.togglePlay();
	}

	function prevSong() {
		musicStore.playPrevious();
	}

	function nextSong() {
		musicStore.playNext();
	}

	function formatTime(seconds: number) {
		const mins = Math.floor(seconds / 60);
		const secs = Math.floor(seconds % 60);
		return `${mins}:${secs < 10 ? '0' : ''}${secs}`;
	}

	function updateCurrentTime(e: Event) {
		const target = e.target as HTMLInputElement;
		const newTime = parseFloat(target.value);
		currentTime = newTime;
		if (audio) {
			audio.currentTime = newTime;
		}
	}

	function handleSliderMouseDown() {
		isDragging = true;
	}

	function handleSliderMouseUp() {
		isDragging = false;
	}

	function updateVolume(e: Event) {
		const target = e.target as HTMLInputElement;
		volume = parseFloat(target.value);
		if (audio) {
			audio.volume = volume;
		}
	}

	// Keyboard controls
	function handleKeydown(e: KeyboardEvent) {
		if (e.code === 'Space') {
			e.preventDefault();
			togglePlay();
		} else if (e.code === 'ArrowLeft') {
			if (audio) {
				audio.currentTime = Math.max(0, audio.currentTime - 5);
			}
		} else if (e.code === 'ArrowRight') {
			if (audio) {
				audio.currentTime = Math.min(duration, audio.currentTime + 5);
			}
		}
	}
</script>

<svelte:window on:keydown={handleKeydown} />

<div
	class="bg-gray-900 text-white fixed bottom-0 left-0 w-full p-4 flex flex-col items-center h-40"
>
	<div class="flex flex-col items-center">
		<h2 class="text-xl font-semibold">{currentSong?.title || 'No song selected'}</h2>
		<p class="text-gray-400">{currentSong?.artist || ''}</p>
	</div>

	<div class="w-[90vw] mt-4 flex items-center justify-between">
		<!-- Controls -->
		<div class="flex justify-center mt-1 space-x-4 pr-4">
			<button on:click={prevSong} class="p-2" aria-label="Previous Song">
				<!-- Previous icon -->
				<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
					<path d="M19 20L9 12l10-8v16z"></path>
				</svg>
			</button>

			<button on:click={togglePlay} class="p-2" aria-label="Play/Pause">
				{#if isPlaying}
					<!-- Pause icon -->
					<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
						<path d="M10 4H6v16h4V4zm8 0h-4v16h4V4z"></path>
					</svg>
				{:else}
					<!-- Play icon -->
					<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
						<path d="M8 5v14l11-7z"></path>
					</svg>
				{/if}
			</button>

			<button on:click={nextSong} class="p-2" aria-label="Next Song">
				<!-- Next icon -->
				<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
					<path d="M5 4l10 8-10 8V4z"></path>
				</svg>
			</button>
		</div>
		<!-- Progress Bar -->
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
			/>
			<div class="flex justify-between text-sm">
				<span>{formatTime(currentTime)}</span>
				<span>{formatTime(duration)}</span>
			</div>
		</div>

		<!-- Volume Control -->
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
