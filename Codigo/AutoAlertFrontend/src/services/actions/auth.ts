import { autoAlertBackend } from "../../api/AutoAlertBackend";

interface LoginCredentials {
  email: string;
  password: string;
}

export const logIn = async (credentials: LoginCredentials) => {
  autoAlertBackend.post("/auth/login", credentials)
    .then((response) => {
      console.log("Login successful:", response.data);
      // Handle successful login, e.g., store token, redirect, etc.
    })
    .catch((error) => {
      console.error("Login failed:", error);
      // Handle login failure, e.g., show error message to user
    });
}