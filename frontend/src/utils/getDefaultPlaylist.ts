import { getPlaylist } from '@/apiCalls/playlistApiCalls';
import type { PlaylistType } from '@/data/types';
import CookieStorage from './cookiesManagement';

async function getDefaultPlaylist(): Promise<PlaylistType> {
	const defaultPlaylistId: number = CookieStorage.get('user').defaultPlaylistId;
	return await getPlaylist(defaultPlaylistId);
}

export default getDefaultPlaylist;
