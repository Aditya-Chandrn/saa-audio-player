import { writable } from 'svelte/store';

interface Audio {
	id: number;
	title: string;
	artist?: string;
	album?: string;
}

// Define the structure of the music player's state
interface MusicState {
	playlist: Song[]; // A list of all songs available
	currentSong: Song | null; // The currently playing song
	isPlaying: boolean; // Whether the song is currently playing
	currentIndex: number; // The index of the current song in the playlist
}

// Define the initial state of the music player
const initialState: MusicState = {
	playlist: [], // Initially, no songs are in the playlist
	currentSong: null, // No song is selected initially
	isPlaying: false, // Player starts in paused state
	currentIndex: -1 // No song index selected yet
};

export const musicStore = (() => {
	const { subscribe, set, update } = writable<MusicState>(initialState);

	return {
		subscribe,

		// Function to load a new playlist (array of songs)
		loadPlaylist: (songs: Song[]) => {
			update((state) => {
				return {
					...state,
					playlist: songs,
					currentIndex: songs.length > 0 ? 0 : -1, // If there are songs, set the index to 0
					currentSong: songs.length > 0 ? songs[0] : null
				};
			});
		},

		// Play the next song in the playlist
		playNext: () =>
			update((state) => {
				const nextIndex = (state.currentIndex + 1) % state.playlist.length; // Loop to start if at the end
				return {
					...state,
					currentIndex: nextIndex,
					currentSong: state.playlist[nextIndex],
					isPlaying: true
				};
			}),

		// Play the previous song in the playlist
		playPrevious: () =>
			update((state) => {
				const prevIndex =
					state.currentIndex === 0 ? state.playlist.length - 1 : state.currentIndex - 1; // Loop to end if at the start
				return {
					...state,
					currentIndex: prevIndex,
					currentSong: state.playlist[prevIndex],
					isPlaying: true
				};
			}),

		// Toggle between play and pause
		togglePlay: () =>
			update((state) => {
				return {
					...state,
					isPlaying: !state.isPlaying
				};
			}),

		// Set the current song by its index in the playlist
		setCurrentSong: (index: number) =>
			update((state) => {
				if (index >= 0 && index < state.playlist.length) {
					return {
						...state,
						currentIndex: index,
						currentSong: state.playlist[index],
						isPlaying: true
					};
				}
				return state; // If the index is invalid, return the state unchanged
			}),

		// Stop playback and reset player state
		stopPlayback: () => set(initialState)
	};
})();
