// playlistDTO.ts

export namespace PlaylistDTO {

    // DTO for Add Audio to Playlist
    export interface AddAudioToPlaylistRequest {
      playlistId: number;
      audioId: number;
    }
  
    export interface AddAudioToPlaylistResult {
      statusCode: number;
      message?: string;
    }
  
    // DTO for Create Playlist
    export interface CreatePlaylistRequest {
      userId: number;
      title?: string;
      description?: string;
    }
  
    export interface CreatePlaylistResult {
      playlistId: number;
      statusCode: number;
      message?: string;
    }
  
    // DTO for Delete Playlist
    export interface DeletePlaylistRequest {
      playlistId: number;
      userId: number;
    }
  
    export interface DeletePlaylistResult {
      statusCode: number;
      message?: string;
    }
  
    // DTO for Edit Playlist
    export interface EditPlaylistRequest {
      playlistId: number;
      userId: number;
      title?: string;
      description?: string;
    }
  
    export interface EditPlaylistResult {
      statusCode: number;
      message?: string;
    }
  
    // DTO for Get Playlist
    export interface GetRequest {
      playlistId?: number;
      userId?: number;
    }
  
    export interface GetResult {
      playlist?: {
        playlistId: number;
        name?: string;
        audios: number[];
      };
      statusCode: number;
      message?: string;
    }
  
    // DTO for Remove Audio from Playlist
    export interface RemoveAudioRequest {
      playlistId: number;
      audioId: number;
    }
  
    export interface RemoveAudioResult {
      statusCode: number;
      message?: string;
    }
  }
  