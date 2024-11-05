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
	let playbackPromise: Promise<void> | null = null;

	let currentAudio: AudioType | null = null;
	let currentIndex = -1;
	let playlist: AudioType[] = [];

	async function safePlay() {
		if (!audio) return;

		try {
			// If there's an ongoing playback promise, wait for it
			if (playbackPromise) {
				await playbackPromise;
			}
			// Start new playback
			playbackPromise = audio.play();
			await playbackPromise;
		} catch (error) {
			console.log('Playback error:', error);
			// Reset the play state in the store if playback fails
			musicStore.togglePlay();
		} finally {
			playbackPromise = null;
		}
	}

	async function safePause() {
		if (!audio) return;

		try {
			// If there's an ongoing playback promise, wait for it
			if (playbackPromise) {
				await playbackPromise;
			}
			audio.pause();
		} catch (error) {
			console.log('Pause error:', error);
		} finally {
			playbackPromise = null;
		}
	}

	// Check playlist initialization
	onMount(() => {
		// console.log('Initializing audio...');
		audio = new Audio();

		audio.addEventListener('loadedmetadata', () => {
			duration = audio.duration || 240;
		});

		audio.addEventListener('timeupdate', () => {
			if (!isDragging) {
				currentTime = audio.currentTime;
			}
		});

		audio.addEventListener('ended', async () => {
			await safePause(); // Ensure clean state before changing tracks
			if (currentIndex < playlist.length - 1 || playlist.length > 0) {
				musicStore.playNext();
			}
		});

		audio.volume = volume;
	});

	const unsubscribe = musicStore.subscribe(async (state) => {
		// console.log('State updated:', state);
		const previousUrl = currentAudio?.url;
		currentAudio = state.currentAudio;
		isPlaying = state.isPlaying;
		currentIndex = state.currentIndex;
		playlist = state.playlist;

		// console.log('playlist', playlist);

		if (audio && currentAudio?.url) {
			const isNewTrack = previousUrl !== currentAudio.url;

			if (isNewTrack) {
				await safePause();
				audio.src = currentAudio.url;
				audio.currentTime = 0;
				currentTime = 0;

				if (state.isPlaying) {
					await safePlay();
				}
			} else {
				if (state.isPlaying && audio.paused) {
					await safePlay();
				} else if (!state.isPlaying && !audio.paused) {
					await safePause();
				}
			}
		}
	});

	onDestroy(() => {
		unsubscribe();
		if (audio) {
			safePause().then(() => {
				audio.src = '';
				audio = null;
			});
		}
	});

	// Toggle Play
	async function togglePlay() {
		if (playbackPromise) await playbackPromise;
		musicStore.togglePlay();
	}

	// Updated Previous Song
	async function prevSong() {
		// console.log('Attempting to go to previous song...');
		// console.log(playlist);
		if (playlist && playlist.length > 0) {
			// console.log('Playlist exists and has length:', playlist.length);
			await safePause();
			musicStore.playPrevious();
		} else {
			// console.log(playlist);
			console.warn('Playlist is empty or undefined');
		}
	}

	// Updated Next Song
	async function nextSong() {
		// console.log('Attempting to go to next song...');
		if (playlist && playlist.length > 0) {
			// console.log('Playlist exists and has length:', playlist.length);
			await safePause();
			musicStore.playNext();
		} else {
			console.warn('Playlist is empty or undefined');
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
			if (isPlaying) {
				await safePlay();
			}
		}
	}

	function handleSliderMouseDown() {
		isDragging = true;
	}

	async function handleSliderMouseUp() {
		isDragging = false;
		if (isPlaying) {
			await safePlay();
		}
	}

	function updateVolume(e: Event) {
		const target = e.target as HTMLInputElement;
		volume = parseFloat(target.value);
		if (audio) {
			audio.volume = volume;
		}
	}

	async function handleKeydown(e: KeyboardEvent) {
		if (e.code === 'Space') {
			// Check if the focused element is an input or textarea
			const target = e.target as HTMLElement;
			if (target.tagName === 'INPUT' || target.tagName === 'TEXTAREA') return;

			e.preventDefault(); // Prevent default only if not in an input/textarea
			await togglePlay();
		} else if (e.code === 'ArrowLeft') {
			if (audio) {
				await safePause();
				audio.currentTime = Math.max(0, audio.currentTime - 5);
				if (isPlaying) {
					await safePlay();
				}
			}
		} else if (e.code === 'ArrowRight') {
			if (audio) {
				await safePause();
				audio.currentTime = Math.min(duration, audio.currentTime + 5);
				if (isPlaying) {
					await safePlay();
				}
			}
		}
	}
</script>

<!-- <script lang="ts">
	import { onMount, onDestroy } from 'svelte';
	import { musicStore } from '../stores/musicStore';
	import type { AudioType } from '@/data/types';

	let isPlaying = false;
	let currentTime = 0;
	let duration = 240;
	let volume = 1;
	let audio: HTMLAudioElement;
	let isDragging = false;
	let playbackPromise: Promise<void> | null = null;

	let currentAudio: AudioType | null = null;
	let currentIndex = -1;
	let playlist: AudioType[] = [];

	async function safePlay() {
		if (!audio) return;

		try {
			// If there's an ongoing playback promise, wait for it
			if (playbackPromise) {
				await playbackPromise;
			}
			// Start new playback
			playbackPromise = audio.play();
			await playbackPromise;
		} catch (error) {
			console.log('Playback error:', error);
			// Reset the play state in the store if playback fails
			musicStore.togglePlay();
		} finally {
			playbackPromise = null;
		}
	}

	async function safePause() {
		if (!audio) return;

		try {
			// If there's an ongoing playback promise, wait for it
			if (playbackPromise) {
				await playbackPromise;
			}
			audio.pause();
		} catch (error) {
			console.log('Pause error:', error);
		} finally {
			playbackPromise = null;
		}
	}

	const unsubscribe = musicStore.subscribe(async (state) => {
		const previousUrl = currentAudio?.url;
		currentAudio = state.currentAudio;
		isPlaying = state.isPlaying;
		currentIndex = state.currentIndex;
		playlist = state.playlist;

		if (audio && currentAudio?.url) {
			const isNewTrack = previousUrl !== currentAudio.url;

			if (isNewTrack) {
				// For track changes, pause current playback first
				await safePause();
				audio.src = currentAudio.url;
				audio.currentTime = 0;
				currentTime = 0;

				if (state.isPlaying) {
					await safePlay();
				}
			} else {
				// For play/pause toggles
				if (state.isPlaying && audio.paused) {
					await safePlay();
				} else if (!state.isPlaying && !audio.paused) {
					await safePause();
				}
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

		audio.addEventListener('ended', async () => {
			await safePause(); // Ensure clean state before changing tracks
			if (currentIndex < playlist.length - 1 || playlist.length > 0) {
				musicStore.playNext();
			}
		});

		audio.volume = volume;
	});

	onDestroy(() => {
		unsubscribe();
		if (audio) {
			safePause().then(() => {
				audio.src = '';
				audio = null;
			});
		}
	});

	async function togglePlay() {
		// Wait for any ongoing playback operations to complete
		if (playbackPromise) {
			await playbackPromise;
		}
		musicStore.togglePlay();
	}

	async function prevSong() {
		if (playlist.length > 0) {
			console.log('here');
			await safePause();
			musicStore.playPrevious();
		}
	}

	async function nextSong() {
		playlist
		if (playlist.length > 0) {
			console.log('here');
			await safePause();
			musicStore.playNext();
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
			if (isPlaying) {
				await safePlay();
			}
		}
	}

	function handleSliderMouseDown() {
		isDragging = true;
	}

	async function handleSliderMouseUp() {
		isDragging = false;
		if (isPlaying) {
			await safePlay();
		}
	}

	function updateVolume(e: Event) {
		const target = e.target as HTMLInputElement;
		volume = parseFloat(target.value);
		if (audio) {
			audio.volume = volume;
		}
	}

	async function handleKeydown(e: KeyboardEvent) {
		if (e.code === 'Space') {
			e.preventDefault();
			await togglePlay();
		} else if (e.code === 'ArrowLeft') {
			if (audio) {
				await safePause();
				audio.currentTime = Math.max(0, audio.currentTime - 5);
				if (isPlaying) {
					await safePlay();
				}
			}
		} else if (e.code === 'ArrowRight') {
			if (audio) {
				await safePause();
				audio.currentTime = Math.min(duration, audio.currentTime + 5);
				if (isPlaying) {
					await safePlay();
				}
			}
		}
	}
</script> -->

<svelte:window on:keydown={handleKeydown} />

<div
	class="bg-gray-900 text-white fixed bottom-0 left-0 w-full p-4 flex flex-col items-center h-40"
>
	<div class="flex flex-col items-center">
		<h2 class="text-xl font-semibold">{currentAudio?.title || 'No audio selected'}</h2>
		<p class="text-gray-400">{currentAudio?.artist || ''}</p>
	</div>

	<div class="w-[90vw] mt-4 flex items-center justify-between">
		<div class="flex justify-center mt-1 space-x-4 pr-4">
			<button on:click={prevSong} class="p-2" aria-label="Previous Song" disabled={!currentAudio}>
				<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
					<path d="M19 20L9 12l10-8v16z"></path>
				</svg>
			</button>

			<button on:click={ () => musicStore.togglePlay} class="p-2" aria-label="Play/Pause" disabled={!currentAudio}>
				{#if $musicStore.isPlaying}
					<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
						<path d="M10 4H6v16h4V4zm8 0h-4v16h4V4z"></path>
					</svg>
				{:else}
					<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
						<path d="M8 5v14l11-7z"></path>
					</svg>
				{/if}
			</button>

			<button on:click={nextSong} class="p-2" aria-label="Next Song" disabled={!currentAudio}>
				<svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
					<path d="M5 4l10 8-10 8V4z"></path>
				</svg>
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
