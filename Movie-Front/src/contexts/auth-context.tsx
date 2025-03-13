"use client";

import { createContext, useContext, useState, useEffect } from "react";

interface User {
  id: string;
  name: string;
  email: string;
  avatar?: string;
}

interface AuthContextType {
  user: User | null;
  login: (usernameOrEmail: string, password: string, rememberMe: boolean) => Promise<void>;
  register: (
    profileUrl: File | null,
    userName: string,
    password: string,
    email: string,
    name: string,
    surname: string,
    birthDate: string,
    isMale: boolean
  ) => Promise<void>;
  logout: () => void;
  isLoading: boolean;
  error: string | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const login = async (usernameOrEmail: string, password: string, rememberMe: boolean) => {
    setIsLoading(true);
    setError(null);

    try {
      const response = await fetch("https://localhost:7116/api/Auths/Login/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ usernameOrEmail, password, rememberMe }),
      });

      if (!response.ok) {
        throw new Error("Invalid username/email or password");
      }

      const data = await response.json();
      const user = {
        id: data.id,
        name: data.name,
        email: data.email,
        avatar: data.avatar,
      };
      setUser(user);
      if (rememberMe) {
        localStorage.setItem("user", JSON.stringify(user));
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
    } finally {
      setIsLoading(false);
    }
  };

  const register = async (
    profileUrl: File | null,
    userName: string,
    password: string,
    email: string,
    name: string,
    surname: string,
    birthDate: string,
    isMale: boolean
  ) => {
    setIsLoading(true);
    setError(null);

    try {
      const formData = new FormData();
      if (profileUrl) {
        formData.append("ProfileUrl", profileUrl);
      }
      formData.append("UserName", userName);
      formData.append("Password", password);
      formData.append("Email", email);
      formData.append("Name", name);
      formData.append("Surname", surname);
      formData.append("BirthDate", birthDate);
      formData.append("IsMale", isMale.toString());

      const response = await fetch("https://localhost:7116/api/Auths/Register/register", {
        method: "POST",
        body: formData,
      });

      if (!response.ok) {
        throw new Error("Registration failed");
      }

      const data = await response.json();
      const user = {
        id: data.id,
        name: data.name,
        email: data.email,
        avatar: data.avatar,
      };
      setUser(user);
      localStorage.setItem("user", JSON.stringify(user));
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
    } finally {
      setIsLoading(false);
    }
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem("user");
  };

  return (
    <AuthContext.Provider value={{ user, login, register, logout, isLoading, error }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}