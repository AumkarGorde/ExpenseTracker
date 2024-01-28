export interface AuthResponse {
  success: boolean;
  message: string;
  data: {
    token: string;
    firstName: string;
    userName: string;
    userId: string;
  };
}
