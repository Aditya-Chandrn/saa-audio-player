<script lang="ts">
	let isPlaying = false;
	let currentTime = 0;
	let duration = 240;
	let song = {
		title: 'Song Title',
		artist: 'Artist Name'
	};

	function togglePlay() {
		isPlaying = !isPlaying;
	}

	function prevSong() {}

	function nextSong() {}

	function formatTime(seconds: number) {
		const mins = Math.floor(seconds / 60);
		const secs = Math.floor(seconds % 60);

		return `${mins}:${secs < 10 ? '0' : ''}${secs}`;
	}

	function updateCurrentTime(e: Event) {
		const target = e.target as HTMLInputElement;
		currentTime = parseFloat(target.value);
	}
</script>

<div
	class="bg-gray-900 text-white fixed bottom-0 left-0 w-full p-4 flex flex-col items-center h-48"
>
	<div class="flex flex-col items-center">
		<h2 class="text-xl font-semibold">{song.title}</h2>
		<p class="text-gray-400">{song.artist}</p>
	</div>
	<div class="w-[90vw] mt-4">
		<input
			type="range"
			min="0"
			max={duration}
			value={currentTime}
			on:input={updateCurrentTime}
			class="w-full"
		/>
		<div class="flex justify-between text-sm">
			<span>{formatTime(currentTime)}</span>
			<span>{formatTime(duration)}</span>
		</div>
	</div>
	<div class="flex justify-center mt-1 space-x-4">
		<button on:click={prevSong} class="text-white text-2xl">⏮️</button>
		<button on:click={togglePlay} class="text-white text-2xl">
			{#if isPlaying}
				⏸️
			{:else}
				▶️
			{/if}
		</button>
		<button on:click={nextSong} class="text-white text-2xl">⏭️</button>
	</div>
</div>
