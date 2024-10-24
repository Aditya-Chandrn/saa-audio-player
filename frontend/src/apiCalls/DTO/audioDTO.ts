// audioDTO.ts

export namespace AudioDTO {

  // DTO for Create Audio Request
  export interface CreateAudioRequest {
    userId: number;
    audioBase64String?: string;
    title?: string;
    album?: string;
  }

  // DTO for Create Audio Result
  export interface CreateAudioResult {
    audioId: number;
    statusCode: number;
    message?: string;
  }

  // DTO for Delete Audio Request
  export interface DeleteAudioRequest {
    audioId: number;
    userId: number;
  }

  // DTO for Delete Audio Result
  export interface DeleteAudioResult {
    statusCode: number;
    message?: string;
  }

  // DTO for Edit Audio Request
  export interface EditAudioRequest {
    audioId: number;
    userId: number;
    title?: string;
    audioBase64String?: string;
    album?: string;
  }

  // DTO for Edit Audio Result
  export interface EditAudioResult {
    statusCode: number;
    message?: string;
  }

  // DTO for Get Audio Request
  export interface GetAudioRequest {
    audioId: number;
  }

  // DTO for Get Audio Result
  export interface GetAudioResult {
    audioBase64String?: string;
    title?: string;
    album?: string;
    creator?: string;
    statusCode: number;
    message?: string;
  }
}
