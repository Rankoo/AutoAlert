import { autoAlertBackend } from "../../api/AutoAlertBackend";

interface LoginCredentials {
  email: string;
  password: string;
}

export const logInAction = async (credentials: LoginCredentials) => {
  const { data } = await autoAlertBackend.post("/auth/login", credentials);
  return data;
}