// import { writable, type Writable } from 'svelte/store';
// import type { AudioType } from '@/data/types';
// import { getAudio } from '@/apiCalls/audioApiCalls'; // Import the API function

// interface MusicState {
// 	playlist: AudioType[];
// 	currentAudio: AudioType | null;
// 	isPlaying: boolean;
// 	currentIndex: number;
// }

// const initialState: MusicState = {
// 	playlist: [],
// 	currentAudio: null,
// 	isPlaying: false,
// 	currentIndex: -1
// };

// export const musicStore: Writable<MusicState> = (() => {
// 	const { subscribe, set, update } = writable<MusicState>(initialState);

// 	// Helper function to fetch audio data and update state
// 	const fetchAudioAndUpdateState = async (audioId: number, index: number) => {
// 		const audioData = await getAudio(audioId);
// 		if (audioData) {
// 			update((state) => ({
// 				...state,
// 				currentIndex: index,
// 				currentAudio: {
// 					...state.playlist[index],
// 					title: audioData.title ?? state.playlist[index].title,
// 					artist: audioData.artist ?? state.playlist[index].artist,
// 					url: audioData.url // Ensure the `url` is set correctly from `audioData`
// 				},
// 				isPlaying: true
// 			}));
// 		} else {
// 			console.warn('Audio data could not be retrieved.');
// 		}
// 	};

// 	return {
// 		subscribe,

// 		loadPlaylist: (audios: AudioType[]) => {
// 			update((state) => ({
// 				...state,
// 				playlist: audios,
// 				currentIndex: audios.length > 0 ? 0 : -1,
// 				currentAudio: audios.length > 0 ? audios[0] : null,
// 				isPlaying: audios.length > 0 ? state.isPlaying : false
// 			}));
// 		},

// 		playNext: () => {
// 			update((state) => {
// 				const { playlist, currentIndex } = state;
// 				if (playlist.length > 0) {
// 					const nextIndex = (currentIndex + 1) % playlist.length;
// 					fetchAudioAndUpdateState(playlist[nextIndex].audioId, nextIndex);
// 				}
// 				return state;
// 			});
// 		},

// 		playPrevious: () => {
// 			update((state) => {
// 				const { playlist, currentIndex } = state;
// 				if (playlist.length > 0) {
// 					const prevIndex = currentIndex === 0 ? playlist.length - 1 : currentIndex - 1;
// 					fetchAudioAndUpdateState(playlist[prevIndex].audioId, prevIndex);
// 				}
// 				return state;
// 			});
// 		},

// 		togglePlay: () =>
// 			update((state) => ({
// 				...state,
// 				isPlaying: state.currentAudio ? !state.isPlaying : state.isPlaying
// 			})),

// 		setCurrentAudio: (audioData: AudioType) =>
// 			update((state) => ({
// 				...state,
// 				currentIndex: state.playlist.findIndex((audio) => audio.audioId === audioData.audioId),
// 				currentAudio: audioData,
// 				isPlaying: true
// 			})),

// 		stopPlayback: () =>
// 			update((state) => ({
// 				...state,
// 				isPlaying: false
// 			}))
// 	};
// })();

import { writable, type Writable } from 'svelte/store';
import type { AudioType } from '@/data/types';
import { getAudio } from '@/apiCalls/audioApiCalls'; // Import the API function

interface MusicState {
	playlist: AudioType[];
	currentAudio: AudioType | null;
	isPlaying: boolean;
	currentIndex: number;
}

// Define the custom type for musicStore with additional methods
interface MusicStore extends Writable<MusicState> {
	loadPlaylist: (audios: AudioType[]) => void;
	playNext: () => void;
	playPrevious: () => void;
	togglePlay: () => void;
	setCurrentAudio: (audioData: AudioType) => void;
	stopPlayback: () => void;
}

const initialState: MusicState = {
	playlist: [],
	currentAudio: null,
	isPlaying: false,
	currentIndex: -1
};

export const musicStore: MusicStore = (() => {
	const { subscribe, set, update } = writable<MusicState>(initialState);

	// Helper function to fetch audio data and update state
	const fetchAudioAndUpdateState = async (audioId: number, index: number) => {
		const audioData = await getAudio(audioId);
		if (audioData) {
			update((state) => ({
				...state,
				currentIndex: index,
				currentAudio: {
					...state.playlist[index],
					title: audioData.title ?? state.playlist[index].title,
					artist: audioData.artist ?? state.playlist[index].artist,
					url: audioData.url // Ensure the `url` is set correctly from `audioData`
				},
				isPlaying: true
			}));
		} else {
			console.warn('Audio data could not be retrieved.');
		}
	};

	return {
		subscribe,
		set, // Expose `set` for Writable<MusicState> compatibility
		update, // Expose `update` for Writable<MusicState> compatibility

		loadPlaylist: (audios: AudioType[]) => {
			update((state) => ({
				...state,
				playlist: audios,
				currentIndex: audios.length > 0 ? 0 : -1,
				currentAudio: audios.length > 0 ? audios[0] : null,
				isPlaying: audios.length > 0 ? state.isPlaying : false
			}));
		},

		playNext: () => {
			update((state) => {
				const { playlist, currentIndex } = state;
				if (playlist.length > 0) {
					const nextIndex = (currentIndex + 1) % playlist.length;
					fetchAudioAndUpdateState(playlist[nextIndex].audioId, nextIndex);
				}
				return state;
			});
		},

		playPrevious: () => {
			update((state) => {
				const { playlist, currentIndex } = state;
				if (playlist.length > 0) {
					const prevIndex = currentIndex === 0 ? playlist.length - 1 : currentIndex - 1;
					fetchAudioAndUpdateState(playlist[prevIndex].audioId, prevIndex);
				}
				return state;
			});
		},

		togglePlay: () =>
			update((state) => ({
				...state,
				isPlaying: state.currentAudio ? !state.isPlaying : state.isPlaying
			})),

		setCurrentAudio: (audioData: AudioType) =>
			update((state) => ({
				...state,
				currentIndex: state.playlist.findIndex((audio) => audio.audioId === audioData.audioId),
				currentAudio: audioData,
				isPlaying: true
			})),

		stopPlayback: () =>
			update((state) => ({
				...state,
				isPlaying: false
			}))
	};
})();
