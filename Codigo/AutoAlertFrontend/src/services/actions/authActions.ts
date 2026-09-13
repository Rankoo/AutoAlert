import { autoAlertBackend } from "../../api/AutoAlertBackend";
import type { Permission } from "../../utils/permissions";

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
  permissions: Permission[];
}

export const getCurrentUserInfoAction = async ():Promise<CurrentUserInfo> => {
  const { data } = await autoAlertBackend.get<CurrentUserInfo>("/auth/me");
  return data;
}

export const logOutAction = async () => {
  const { data } = await autoAlertBackend.post("/auth/logOut");
  return data;
}