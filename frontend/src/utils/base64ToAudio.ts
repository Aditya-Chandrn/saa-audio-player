export function createAudioUrlFromBase64(base64String: string): string {
	const binary = atob(base64String);
	const array = Uint8Array.from(binary, (char) => char.charCodeAt(0));
	const blob = new Blob([array], { type: 'audio/mp3' });
	return URL.createObjectURL(blob);
}
