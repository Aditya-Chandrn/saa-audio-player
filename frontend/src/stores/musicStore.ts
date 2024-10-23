// stores/musicStore.ts
import { writable } from 'svelte/store';

interface Song {
	title: string;
	artist: string;
	url: string;
}

interface PlayerState {
	currentSong: Song | null;
	currentPlaylist: Song[] | null;
	currentIndex: number;
	isPlaying: boolean;
}

function createMusicStore() {
	const { subscribe, set, update } = writable<PlayerState>({
		currentSong: null,
		currentPlaylist: null,
		currentIndex: -1,
		isPlaying: false
	});

	return {
		subscribe,
		playSong: (song: Song, playlist: Song[], index: number) => {
			update((state) => ({
				...state,
				currentSong: song,
				currentPlaylist: playlist,
				currentIndex: index,
				isPlaying: true
			}));
		},
		playNext: () => {
			update((state) => {
				if (!state.currentPlaylist || state.currentIndex === -1) return state;

				const nextIndex = (state.currentIndex + 1) % state.currentPlaylist.length;
				return {
					...state,
					currentSong: state.currentPlaylist[nextIndex],
					currentIndex: nextIndex,
					isPlaying: true
				};
			});
		},
		playPrevious: () => {
			update((state) => {
				if (!state.currentPlaylist || state.currentIndex === -1) return state;

				const prevIndex =
					state.currentIndex === 0 ? state.currentPlaylist.length - 1 : state.currentIndex - 1;
				return {
					...state,
					currentSong: state.currentPlaylist[prevIndex],
					currentIndex: prevIndex,
					isPlaying: true
				};
			});
		},
		togglePlay: () => {
			update((state) => ({
				...state,
				isPlaying: !state.isPlaying
			}));
		},
		reset: () => {
			set({
				currentSong: null,
				currentPlaylist: null,
				currentIndex: -1,
				isPlaying: false
			});
		}
	};
}

export const musicStore = createMusicStore();