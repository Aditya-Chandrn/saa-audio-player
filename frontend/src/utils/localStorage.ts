class LocalStorage{
constructor(){}

  public static setItem(key: string, value: any) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  // Get an item from localStorage
  public static getItem(key: string): any | null {
    const value = localStorage.getItem(key);
    return value ? JSON.parse(value) : null;
  }

  // Remove an item from localStorage
  public static removeItem(key: string) {
    localStorage.removeItem(key);
  }

  // Clear all items from localStorage
  public static clearLocalStorage() {
    localStorage.clear();
  }
}

export default LocalStorage;