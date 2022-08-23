export interface ResponseObject {
    message: string;
    payload: AuthorizationPayload;
    status: boolean;
}

export interface AuthorizationPayload {
    accessToken: string;
}

export interface AuthorizationRequest {
    email: string;
    password: string;
}

export interface JwtDecoded {
  aud: string
  exp: number
  iat: number
  iss: string
  nbf: number
  role: string
  unique_name: string
}

export enum Role {
  ADMIN,
  USER
}
