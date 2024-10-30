<script lang="ts">
	import { createAudio } from '@/apiCalls/audioApiCalls';

	let audioFile: File | null = null;
	let playlistId: number = 1; // Set your default playlistId or pass dynamically
	let title = '';
	let album: string | null = null;
	let base64Audio = '';

	// Function to handle file selection and convert to base64
	const handleFileChange = async (event: Event) => {
		const target = event.target as HTMLInputElement;
		const files = target.files;
		if (files && files.length > 0) {
			audioFile = files[0];

			try {
				const base64 = await fileToBase64(audioFile);
				base64Audio = base64.split(',')[1]; // Remove prefix
			} catch (error) {
				console.error('Error processing audio file:', error);
			}
		}
	};

	// Promise-based file to base64 conversion
	const fileToBase64 = (file: File): Promise<string> => {
		return new Promise((resolve, reject) => {
			const reader = new FileReader();
			reader.onload = () => {
				if (typeof reader.result === 'string') {
					resolve(reader.result);
				} else {
					reject(new Error('Failed to convert file to base64'));
				}
			};
			reader.onerror = () => reject(reader.error);
			reader.readAsDataURL(file);
		});
	};

	// Function to upload audio to the endpoint
	const uploadAudio = async () => {
		if (base64Audio && title) {
			await createAudio(playlistId, base64Audio, title, album);
		} else {
			alert('Please complete all fields and select a file to upload.');
		}
	};
</script>

<div class="file-upload">
	<label for="audioFile" class="block text-sm font-medium text-gray-700">Select Audio File:</label>
	<input
		type="file"
		id="audioFile"
		accept="audio/*"
		on:change={handleFileChange}
		class="mt-1 block w-full text-sm text-gray-500
               file:mr-4 file:py-2 file:px-4
               file:rounded-full file:border-0
               file:text-sm file:font-semibold
               file:bg-violet-50 file:text-violet-700
               hover:file:bg-violet-100"
	/>

	<!-- svelte-ignore a11y-label-has-associated-control -->
	<label class="block text-sm font-medium text-gray-700 mt-4">Title:</label>
	<input
		type="text"
		placeholder="Enter title"
		bind:value={title}
		class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
	/>
	<!-- svelte-ignore a11y-label-has-associated-control -->
	<label class="block text-sm font-medium text-gray-700 mt-4">Album (Optional):</label>
	<input
		type="text"
		placeholder="Album name"
		bind:value={album}
		class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
	/>

	<button
		on:click={uploadAudio}
		class="mt-4 px-4 py-2 bg-blue-500 text-white font-semibold rounded-lg hover:bg-blue-600"
	>
		Upload Audio
	</button>
</div>
