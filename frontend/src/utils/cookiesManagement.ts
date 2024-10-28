// src/utils/CookieStorage.ts

class CookieStorage {
  constructor() {}

  // Set a cookie with a long expiration time (e.g., 10 years)
  public static set(key: string, value: any) {
    const maxAge = 60 * 60 * 24 * 365 * 10; // 10 years in seconds
    document.cookie = `${key}=${encodeURIComponent(
      JSON.stringify(value)
    )}; path=/; max-age=${maxAge}; Secure; SameSite=Strict`;
  }

  // Get a cookie by key
  public static get(key: string): any | null {
    const name = key + "=";
    const decodedCookie = decodeURIComponent(document.cookie);
    const cookies = decodedCookie.split("; ");

    for (const cookie of cookies) {
      if (cookie.indexOf(name) === 0) {
        return JSON.parse(cookie.substring(name.length));
      }
    }
    return null;
  }

  // Remove a cookie by setting its expiration to the past
  public static remove(key: string) {
    document.cookie = `${key}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
  }

  // Clear all cookies by iterating over them and removing each
  public static removeAll() {
    const cookies = document.cookie.split("; ");
    for (const cookie of cookies) {
      const key = cookie.split("=")[0];
      this.remove(key);
    }
  }
}

export default CookieStorage;
