type AudioType = {
  audioId: number,
  title: string,
  album?: string,
  audioBase64String?: string,
  artist?: string,
  genre?: string,
}

type PlaylistType = {
  playlistId: number,
  name: string,
  imageBase64String?: string,
  audios: AudioType[]
}

type UserType = {
  userId: number,
  username: string,
  password?: string
}

export type {AudioType, PlaylistType, UserType};