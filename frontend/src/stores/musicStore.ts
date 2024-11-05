import { writable } from 'svelte/store';
import type { AudioType } from '@/data/types';

interface MusicState {
	playlist: AudioType[];
	currentAudio: AudioType | null;
	isPlaying: boolean;
	currentIndex: number;
}

const initialState: MusicState = {
	playlist: [],
	currentAudio: null,
	isPlaying: false,
	currentIndex: -1
};

export const musicStore = (() => {
	const { subscribe, set, update } = writable<MusicState>(initialState);

	return {
		subscribe,

		loadPlaylist: (audios: AudioType[]) => {
			// console.log('Loading playlist:', audios);
			update((state) => {
				const newState = {
					...state,
					playlist: audios,
					currentIndex: audios.length > 0 ? 0 : -1,
					currentAudio: audios.length > 0 ? audios[0] : null
				};
				// console.log('Updated state after loading playlist:', newState);
				return newState;
			});
		},

		playNext: () =>
			update((state) => {
				if (state.playlist.length === 0) {
					// console.log('No audios in the playlist to play next.');
					return state;
				}
				const nextIndex = (state.currentIndex + 1) % state.playlist.length;
				const newState = {
					...state,
					currentIndex: nextIndex,
					currentAudio: state.playlist[nextIndex],
					isPlaying: true
				};
				// console.log('Playing next audio:', newState.currentAudio, 'at index:', nextIndex);
				return newState;
			}),

		playPrevious: () =>
			update((state) => {
				if (state.playlist.length === 0) {
					// console.log('No audios in the playlist to play previous.');
					return state;
				}
				const prevIndex =
					state.currentIndex === 0 ? state.playlist.length - 1 : state.currentIndex - 1;
				const newState = {
					...state,
					currentIndex: prevIndex,
					currentAudio: state.playlist[prevIndex],
					isPlaying: true
				};
				// console.log('Playing previous audio:', newState.currentAudio, 'at index:', prevIndex);
				return newState;
			}),

			togglePlay: () =>
				update((state) => {
				  const newState = {
					...state,
					isPlaying: state.currentAudio ? !state.isPlaying : state.isPlaying
				  };
				  console.log('Toggle Play, isPlaying:', newState.isPlaying);
				  return newState;
				}),
			  
			  setCurrentAudio: (audioData: { audioId: number; title: string; artist: string; url: string }) =>
				update((state) => {
				  const audioIndex = state.playlist.findIndex((audio) => audio.audioId === audioData.audioId);
				  const newState = {
					...state,
					currentIndex: audioIndex !== -1 ? audioIndex : state.currentIndex,
					currentAudio: audioData,
					isPlaying: true // Directly set to playing
				  };
				  console.log('Set Current Audio, isPlaying:', newState.isPlaying);
				  return newState;
				}),

		stopPlayback: () => {
			// console.log('Stopping playback and resetting state.');
			set(initialState);
		}
	};
})();
