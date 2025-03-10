"use client";

import type React from "react";

import { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { useMovies } from "../contexts/movie-context";
import { MovieCard } from "../components/movie-card";
import { Input } from "../components/ui/input";
import { Button } from "../components/ui/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../components/ui/select";
import { Checkbox } from "../components/ui/checkbox";
import { Label } from "../components/ui/label";
import type { Movie } from "../contexts/movie-context";

export function MoviesPage() {
  const { movies, isLoading } = useMovies();
  const [searchParams, setSearchParams] = useSearchParams();
  const [filteredMovies, setFilteredMovies] = useState<Movie[]>([]);
  const [searchTerm, setSearchTerm] = useState(
    searchParams.get("search") || ""
  );
  const [selectedGenre, setSelectedGenre] = useState(
    searchParams.get("genre") || ""
  );
  const [sortBy, setSortBy] = useState(searchParams.get("sort") || "title");
  const [showAvailableOnly, setShowAvailableOnly] = useState(
    searchParams.get("available") === "true"
  );

  // Extract unique genres from all movies
  const allGenres = movies
    .reduce((genres, movie) => {
      movie.genres.forEach((genre) => {
        if (!genres.includes(genre)) {
          genres.push(genre);
        }
      });
      return genres;
    }, [] as string[])
    .sort();

  useEffect(() => {
    if (!isLoading) {
      let filtered = [...movies];

      // Apply search filter
      if (searchTerm) {
        filtered = filtered.filter(
          (movie) =>
            movie.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
            movie.description.toLowerCase().includes(searchTerm.toLowerCase())
        );
      }

      // Apply genre filter
      if (selectedGenre) {
        filtered = filtered.filter((movie) =>
          movie.genres.includes(selectedGenre)
        );
      }

      // Apply availability filter
      if (showAvailableOnly) {
        filtered = filtered.filter((movie) => movie.available);
      }

      // Apply sorting
      switch (sortBy) {
        case "title":
          filtered.sort((a, b) => a.title.localeCompare(b.title));
          break;
        case "rating":
          filtered.sort((a, b) => b.rating - a.rating);
          break;
        case "release":
          filtered.sort(
            (a, b) =>
              new Date(b.releaseDate).getTime() -
              new Date(a.releaseDate).getTime()
          );
          break;
        case "price":
          filtered.sort((a, b) => a.rentalPrice - b.rentalPrice);
          break;
        default:
          break;
      }

      setFilteredMovies(filtered);
    }
  }, [movies, searchTerm, selectedGenre, sortBy, showAvailableOnly, isLoading]);

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    updateSearchParams();
  };

  const updateSearchParams = () => {
    const params = new URLSearchParams();
    if (searchTerm) params.set("search", searchTerm);
    if (selectedGenre) params.set("genre", selectedGenre);
    if (sortBy) params.set("sort", sortBy);
    if (showAvailableOnly) params.set("available", "true");
    setSearchParams(params);
  };

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-64">Loading...</div>
    );
  }

  return (
    <div>
      <h1 className="text-3xl font-bold mb-8">Browse Movies</h1>

      <div className="grid grid-cols-1 md:grid-cols-[250px_1fr] gap-8">
        {/* Filters Sidebar */}
        <div className="space-y-6">
          <div>
            <h3 className="text-lg font-semibold mb-4">Filters</h3>
            <form onSubmit={handleSearch} className="space-y-4">
              <div className="space-y-2">
                <Label htmlFor="search">Search</Label>
                <div className="flex space-x-2">
                  <Input
                    id="search"
                    placeholder="Search movies..."
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                  />
                  <Button type="submit">Search</Button>
                </div>
              </div>

              <div className="space-y-2">
                <Label htmlFor="genre">Genre</Label>
                <Select
                  value={selectedGenre}
                  onValueChange={(value) => {
                    setSelectedGenre(value);
                    setTimeout(updateSearchParams, 0);
                  }}
                >
                  <SelectTrigger id="genre">
                    <SelectValue placeholder="All Genres" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="all">All Genres</SelectItem>
                    {allGenres.map((genre) => (
                      <SelectItem key={genre} value={genre}>
                        {genre}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>

              <div className="space-y-2">
                <Label htmlFor="sort">Sort By</Label>
                <Select
                  value={sortBy}
                  onValueChange={(value) => {
                    setSortBy(value);
                    setTimeout(updateSearchParams, 0);
                  }}
                >
                  <SelectTrigger id="sort">
                    <SelectValue placeholder="Sort By" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="title">Title (A-Z)</SelectItem>
                    <SelectItem value="rating">Rating (High to Low)</SelectItem>
                    <SelectItem value="release">
                      Release Date (Newest)
                    </SelectItem>
                    <SelectItem value="price">Price (Low to High)</SelectItem>
                  </SelectContent>
                </Select>
              </div>

              <div className="flex items-center space-x-2">
                <Checkbox
                  id="available"
                  checked={showAvailableOnly}
                  onCheckedChange={(checked) => {
                    setShowAvailableOnly(checked as boolean);
                    setTimeout(updateSearchParams, 0);
                  }}
                />
                <Label htmlFor="available">Available for rent only</Label>
              </div>
            </form>
          </div>
        </div>

        {/* Movies Grid */}
        <div>
          {filteredMovies.length === 0 ? (
            <div className="text-center py-12">
              <h3 className="text-xl font-semibold mb-2">No movies found</h3>
              <p className="text-muted-foreground">
                Try adjusting your search or filter criteria
              </p>
            </div>
          ) : (
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
              {filteredMovies.map((movie) => (
                <MovieCard key={movie.id} movie={movie} />
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
