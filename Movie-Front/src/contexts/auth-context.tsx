"use client";

import { createContext, useContext, useState, useEffect } from "react";

interface User {
  Id: string;
  Name: string;
  Email: string;
  avatar?: string;
}

interface AuthContextType {
  user: User | null;
  login: (
    UsernameOrEmail: string,
    Password: string,
    RememberMe: boolean
  ) => Promise<void>;
  register: (
    ProfileUrl: File | null,
    UserName: string,
    Password: string,
    Email: string,
    Name: string,
    Surname: string,
    BirthDate: string,
    IsMale: boolean
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

  const login = async (
    UsernameOrEmail: string,
    Password: string,
    RememberMe: boolean
  ) => {
    setIsLoading(true);
    setError(null);

    try {
      const response = await fetch(
        "https://localhost:7116/api/Auths/Login/login",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ UsernameOrEmail, Password, RememberMe }),
        }
      );

      if (!response.ok) {
        throw new Error("Invalid username/email or password");
      }

      const data = await response.json();
      const user = {
        Id: data.Id,
        Name: data.Name,
        Email: data.Email,
        avatar: data.avatar,
      };
      setUser(user);
      if (RememberMe) {
        localStorage.setItem("user", JSON.stringify(user));
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : "An error occurred");
    } finally {
      setIsLoading(false);
    }
  };
  const register = async (
    ProfileUrl: File | null,
    UserName: string,
    Password: string,
    Email: string,
    Name: string,
    Surname: string,
    BirthDate: string,
    IsMale: boolean
  ) => {
    setIsLoading(true);
    setError(null);

    try {
      const formData = new FormData();
      if (ProfileUrl) {
        formData.append("ProfileUrl", ProfileUrl);
      }
      formData.append("UserName", UserName);
      formData.append("Password", Password);
      formData.append("Email", Email);
      formData.append("Name", Name);
      formData.append("Surname", Surname);
      formData.append("BirthDate", BirthDate);
      formData.append("IsMale", IsMale.toString());

      const response = await fetch(
        "https://localhost:7116/api/Auths/Register/register",
        {
          method: "POST",
          body: formData,
        }
      );

      if (!response.ok) {
        throw new Error("Registration failed");
      }

      const data = await response.json();
      const user = {
        Id: data.Id,
        Name: data.Name,
        Email: data.Email,
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
    <AuthContext.Provider
      value={{ user, login, register, logout, isLoading, error }}
    >
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
