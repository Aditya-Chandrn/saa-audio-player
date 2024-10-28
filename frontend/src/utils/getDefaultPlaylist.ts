import { getPlaylist } from "@/apiCalls/playlistApiCalls";
import type { Playlist } from "@/data/types";
import CookieStorage from "./cookiesManagement";

async function getDefaultPlaylist (): Promise<Playlist> {
  const defaultPlaylistId: number = CookieStorage.get('user').defaultPlaylistId;
  return await getPlaylist(defaultPlaylistId);
}

export default getDefaultPlaylist;