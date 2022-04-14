
export interface CreateUserDto {
  userName?: string;
  name?: string;
  email?: string;
  dateOfBirth?: string;
  password?: string;
}

export interface LoginUserDto {
  userName?: string;
  password?: string;
}

export interface Token {
  access_token?: string;
  expires_in?: string;
  token_type?: string;
  scope?: string;
}
