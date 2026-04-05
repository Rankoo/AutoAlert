import { autoAlertBackend } from "../../api/AutoAlertBackend";

interface LoginCredentials {
  email: string;
  password: string;
}

export const logInAction = async (credentials: LoginCredentials) => {
  const { data } = await autoAlertBackend.post("/auth/login", credentials);
  return data;
}

export interface CurrentUserInfo {
  user:      UserInfo;
  expiresIn: number;
}

export interface UserInfo {
  id:          string;
  names:       string;
  lastNames:   string;
  email:       string;
  role:        string;
  permissions: any[];
}

export const getCurrentUserInfoAction = async ():Promise<CurrentUserInfo> => {
  const { data } = await autoAlertBackend.get<Promise<CurrentUserInfo>>("/auth/me");
  return data;
}