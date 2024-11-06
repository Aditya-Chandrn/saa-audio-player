<script lang="ts">
	import { createAudio } from '@/apiCalls/audioApiCalls';

	let audioFile: File | null = null;
	let playlistId: number = 1;
	let title = '';
	let album: string | null = null;
	let base64Audio = '';
	let isUploading = false;

	const handleFileChange = async (event: Event) => {
		const target = event.target as HTMLInputElement;
		const files = target.files;
		if (files && files.length > 0) {
			audioFile = files[0];

			try {
				const base64 = await fileToBase64(audioFile);
				base64Audio = base64.split(',')[1];
			} catch (error) {
				console.error('Error processing audio file:', error);
			}
		}
	};

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

	const uploadAudio = async () => {
		if (!base64Audio || !title) {
			alert('Please complete all fields and select a file to upload.');
			return;
		}

		try {
			isUploading = true;
			await createAudio(playlistId, base64Audio, title, album);
			// Reset form after successful upload
			audioFile = null;
			title = '';
			album = null;
			base64Audio = '';
			const input = document.getElementById('audioFile') as HTMLInputElement;
			if (input) input.value = '';
		} catch (error) {
			console.error('Upload failed:', error);
			alert('Failed to upload audio. Please try again.');
		} finally {
			isUploading = false;
		}
	};
</script>

<div class="max-w-2xl mx-auto p-8 backdrop-blur-sm rounded-xl border border-gray-700/50 shadow-lg">
	<h2 class="text-2xl font-bold mb-6 text-black">Upload Audio</h2>

	<div class="space-y-6">
		<!-- File Upload -->
		<div class="relative">
			<label for="audioFile" class="block text-sm font-medium text-black mb-2">
				Select Audio File
			</label>
			<input
				type="file"
				id="audioFile"
				accept="audio/*"
				on:change={handleFileChange}
				class="block w-full text-sm text-gray-500
					   file:mr-4 file:py-2 file:px-4
					   file:rounded-full file:border-0
					   file:text-sm file:font-semibold
					   file:bg-violet-50 file:text-violet-700
					   hover:file:bg-violet-100"
			/>
			{#if audioFile}
				<p class="mt-2 text-sm text-black">Selected: {audioFile.name}</p>
			{/if}
		</div>

		<!-- Title Input -->
		<div>
			<label for="title" class="block text-sm font-medium text-black mb-2"> Title </label>
			<input
				type="text"
				id="title"
				placeholder="Enter title"
				bind:value={title}
				class="w-full px-4 py-2 border border-gray-600 rounded-lg text-black placeholder-gray-400 focus:ring-1 outline-none transition-colors duration-300"
			/>
		</div>

		<!-- Album Input -->
		<div>
			<label for="album" class="block text-sm font-medium text-black mb-2">
				Album (Optional)
			</label>
			<input
				type="text"
				id="album"
				placeholder="Album name"
				bind:value={album}
				class="w-full px-4 py-2 border border-gray-600 rounded-lg text-black placeholder-gray-400 focus:ring-1 outline-none transition-colors duration-300"
			/>
		</div>

		<!-- Upload Button -->
		<button
			on:click={uploadAudio}
			disabled={isUploading}
			class="w-full px-4 py-3 bg-[#86efac] text-black font-semibold rounded-lg
            focus:outline-none focus:ring-2 focus:ring-offset-2
            disabled:opacity-50 disabled:cursor-not-allowed transition-colors duration-300"
		>
			{isUploading ? 'Uploading...' : 'Upload Audio'}
		</button>
	</div>
</div>
