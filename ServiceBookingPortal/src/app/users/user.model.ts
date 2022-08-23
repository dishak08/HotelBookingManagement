import { Role } from "../authentication/auth.model";

export interface User {
    id: number;
    name: string;
    email: string;
    mobile: string;
    role: Role;
    registrationDate: Date;
}

export interface UserCreate {
  name: string;
  email: string;
  mobile: string;
  role: Role;
}

export interface UserResponse {
    message: string;
    payload: User;
    status: boolean;
}

export interface UserListResponse {
    message: string;
    payload: User[];
    status: boolean;
}

export interface Response {
    message: string;
    status: boolean;
}

export interface Movie {
    id:number;
    name:string;
    price:number;
    description:string;
    type:string;
}

export interface MovieResponse {
    message: string;
    payload: Movie;
    status: boolean;
}
