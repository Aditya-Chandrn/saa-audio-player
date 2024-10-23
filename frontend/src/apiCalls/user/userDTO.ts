// LoginRequest interface
interface LoginRequest {
    username?: string; // Optional field
    password?: string; // Optional field
  }
  
  // UserData interface inside LoginResult
  interface UserData {
    userId: number;
    username?: string; // Optional field
    email?: string; // Optional field
    image?: string; // Optional field
  }
  
  // LoginResult interface
  interface LoginResult {
    message?: string; // Optional field
    user?: UserData; // Optional field
  }
  
  // SignupRequest interface
  interface SignupRequest {
    username?: string; // Optional field
    email?: string; // Optional field
    password?: string; // Optional field
  }
  
  // SignupResult interface
  interface SignupResult {
    statusCode: number;
    message?: string; // Optional field
  }
  
  // EditUserRequest interface
  interface EditUserRequest {
    userId: number;
    username?: string; // Optional field
    password?: string; // Optional field
    email?: string; // Optional field
    image?: string; // Optional field
  }
  
  // EditUserResult interface
  interface EditUserResult {
    statusCode: number;
    message?: string; // Optional field
    user?: UserData; // Optional field
  }
  
  // DeleteUserRequest interface
  interface DeleteUserRequest {
    userId: number;
  }
  
  // DeleteUserResult interface
  interface DeleteUserResult {
    statusCode: number;
    message?: string; // Optional field
  }
  
  // Export all types
  export type {
    LoginRequest,
    LoginResult,
    SignupRequest,
    SignupResult,
    EditUserRequest,
    EditUserResult,
    DeleteUserRequest,
    DeleteUserResult,
  };
  