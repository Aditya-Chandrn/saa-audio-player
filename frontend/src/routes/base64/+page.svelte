<script lang="ts">
    import { onDestroy } from 'svelte';
    import { musicStore } from "@/stores/musicStore";
    
    interface Song {
        id: number;
        title: string;
        artist: string;
        url: string;
    }
    
    let audioFile: File | null = null;
    let base64Audio = '';
    let audioUrl = '';
    
    // Function to handle file selection
    const handleFileChange = async (event: Event) => {
        const target = event.target as HTMLInputElement;
        const files = target.files;
        if (files && files.length > 0) {
            audioFile = files[0];
            console.log('Selected file:', audioFile.name);
            
            try {
                const base64 = await fileToBase64(audioFile);
                base64Audio = base64.split(',')[1];
                console.log('Audio converted to base64');
                
                createMp3BlobAndPlay(base64Audio, audioFile.name);
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
    
    // Function to convert Base64 to a Blob, create a blob URL, and play the audio
    const createMp3BlobAndPlay = (base64: string, fileName: string) => {
        // Convert base64 string to binary data
        const binary = atob(base64);
        const arrayBuffer = new Uint8Array(binary.length);
        for (let i = 0; i < binary.length; i++) {
            arrayBuffer[i] = binary.charCodeAt(i);
        }
    
        // Create a Blob from the binary data
        const blob = new Blob([arrayBuffer], { type: 'audio/mpeg' });
    
        // Revoke the previous blob URL if it exists
        if (audioUrl) {
            URL.revokeObjectURL(audioUrl);
        }
    
        // Create a Blob URL for the audio file
        audioUrl = URL.createObjectURL(blob);
    
        // Create the song object
        const newSong: Song = {
            id: Date.now(),
            title: fileName,
            artist: 'Local Audio',
            url: audioUrl
        };
    
        // Update the music store
        musicStore.loadPlaylist([newSong]);
        musicStore.togglePlay();
    };
    
    // Cleanup function to revoke blob URL when component is destroyed
    onDestroy(() => {
        if (audioUrl) {
            URL.revokeObjectURL(audioUrl);
        }
    });
    </script>
    
    <div class="file-input">
        <label for="audioFile" class="block text-sm font-medium text-gray-700">Select an Audio File:</label>
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
    
        {#if base64Audio}
            <p class="mt-4 text-sm font-medium text-gray-700">Base64 String:</p>
            <textarea 
                rows="4" 
                cols="50" 
                readonly 
                class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm"
            >{base64Audio}</textarea>
        {/if}
    </div>