"use client";

import type React from "react";
import { Link, useNavigate } from "react-router-dom";
import { Button } from "../components/ui/button";
import { Input } from "../components/ui/input";
import { useAuth } from "../contexts/auth-context";
import { ThemeToggle } from "../components/theme-toggle";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "../components/ui/dropdown-menu";
import { Avatar, AvatarFallback, AvatarImage } from "../components/ui/avatar";
import { useState } from "react";
import { Search, User, LogOut } from "lucide-react";
import axios from "axios";

export function Navbar() {
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const [searchQuery, setSearchQuery] = useState("");

  const handleSearch = async (e: React.FormEvent) => {
    e.preventDefault();
    if (searchQuery.trim()) {
      try {
        const response = await axios.get(
          `https://localhost:7116/api/Movies/GetMoviesByTitle/title/${encodeURIComponent(
            searchQuery
          )}`
        );

        if (response.data && response.data.length > 0) {
          navigate(`/movies?search=${encodeURIComponent(searchQuery)}`, {
            state: { searchResults: response.data }, 
          });
        } else {
          navigate(`/movies?search=${encodeURIComponent(searchQuery)}`, {
            state: { searchResults: [] }, 
          });
        }
      } catch (error) {
        console.error("error", error);
        navigate(`/movies?search=${encodeURIComponent(searchQuery)}`, {
          state: { searchResults: [] }, 
        });
      }
    }
  };

  return (
    <header className="border-b">
      <div className="container mx-auto px-4 py-3">
        <div className="flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <Link to="/" className="text-2xl font-bold">
              MovieRental
            </Link>
            <nav className="hidden md:flex space-x-4">
              <Link to="/" className="hover:text-primary transition-colors">
                Home
              </Link>
              <Link
                to="/movies"
                className="hover:text-primary transition-colors"
              >
                Movies
              </Link>
              {user && (
                <Link
                  to="/rentals"
                  className="hover:text-primary transition-colors"
                >
                  My Rentals
                </Link>
              )}
            </nav>
          </div>

          <div className="flex items-center space-x-4">
            <form onSubmit={handleSearch} className="relative hidden md:block">
              <Input
                type="search"
                placeholder="Search movies..."
                className="w-[200px] lg:w-[300px]"
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
              />
              <Button
                type="submit"
                variant="ghost"
                size="icon"
                className="absolute right-0 top-0"
              >
                <Search className="h-4 w-4" />
              </Button>
            </form>

            <ThemeToggle />

            {user ? (
              <DropdownMenu>
                <DropdownMenuTrigger asChild>
                  <Button
                    variant="ghost"
                    className="relative h-8 w-8 rounded-full"
                  >
                    <Avatar className="h-8 w-8">
                      <AvatarImage src={user.avatar} alt={user.Name} />
                      <AvatarFallback>{user.Name}</AvatarFallback>
                    </Avatar>
                  </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent align="end">
                  <DropdownMenuItem onClick={() => navigate("/profile")}>
                    <User className="mr-2 h-4 w-4" />
                    <span>Profile</span>
                  </DropdownMenuItem>
                  <DropdownMenuItem onClick={logout}>
                    <LogOut className="mr-2 h-4 w-4" />
                    <span>Logout</span>
                  </DropdownMenuItem>
                </DropdownMenuContent>
              </DropdownMenu>
            ) : (
              <Button onClick={() => navigate("/login")} variant="default">
                Login
              </Button>
            )}
          </div>
        </div>
      </div>
    </header>
  );
}