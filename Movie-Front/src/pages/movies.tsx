"use client";

import React, { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { useMovies } from "../contexts/movie-context";
import { useGenres } from "../hooks/useGenres";
import { MovieCard } from "../components/movie-card";
import { Input } from "../components/ui/input";
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
  const { genres, isLoading: isGenresLoading, error } = useGenres();
  const [searchParams, setSearchParams] = useSearchParams();
  const [filteredMovies, setFilteredMovies] = useState<Movie[]>(movies);
  const [searchTerm, setSearchTerm] = useState(searchParams.get("search") || "");
  const [selectedGenre, setSelectedGenre] = useState(searchParams.get("genre") || "");
  const [showAvailableOnly, setShowAvailableOnly] = useState(searchParams.get("available") === "true");
  const [sortBy, setSortBy] = useState(searchParams.get("sortBy") || "");
  const [sortOrder, setSortOrder] = useState(searchParams.get("sortOrder") || "asc");
  const [minDuration, setMinDuration] = useState(searchParams.get("minDuration") || "");
  const [maxDuration, setMaxDuration] = useState(searchParams.get("maxDuration") || "");
  const [releaseDate, setReleaseDate] = useState(searchParams.get("releaseDate") || "");
  const [rating, setRating] = useState(searchParams.get("rating") || "");

  useEffect(() => {
    let filtered = movies;

    if (searchTerm) {
      const lowerCaseSearchTerm = searchTerm.toLowerCase();
      filtered = filtered.filter(
        (movie) =>
          movie.title.toLowerCase().includes(lowerCaseSearchTerm)
      );
    }

    if (selectedGenre && selectedGenre !== "all") {
      filtered = filtered.filter((movie) =>
        Array.isArray(movie.genres) && movie.genres.includes(selectedGenre)
      );
    }

    if (showAvailableOnly) {
      filtered = filtered.filter((movie) => movie.available);
    }

    if (minDuration) {
      filtered = filtered.filter((movie) => movie.duration >= parseInt(minDuration));
    }

    if (maxDuration) {
      filtered = filtered.filter((movie) => movie.duration <= parseInt(maxDuration));
    }

    if (releaseDate) {
      filtered = filtered.filter((movie) => movie.releaseDate === releaseDate);
    }

    if (rating) {
      filtered = filtered.filter((movie) => movie.rating >= parseFloat(rating));
    }

    if (sortBy) {
      filtered.sort((a, b) => {
        if (sortBy === "title") {
          return sortOrder === "asc" ? a.title.localeCompare(b.title) : b.title.localeCompare(a.title);
        } else if (sortBy === "rating") {
          return sortOrder === "asc" ? a.rating - b.rating : b.rating - a.rating;
        } else if (sortBy === "releaseDate") {
          return sortOrder === "asc"
            ? new Date(a.releaseDate).getTime() - new Date(b.releaseDate).getTime()
            : new Date(b.releaseDate).getTime() - new Date(a.releaseDate).getTime();
        } else if (sortBy === "duration") {
          return sortOrder === "asc" ? a.duration - b.duration : b.duration - a.duration;
        }
        return 0;
      });
    }

    setFilteredMovies(filtered);
  }, [movies, searchTerm, selectedGenre, showAvailableOnly, sortBy, sortOrder, minDuration, maxDuration, releaseDate, rating]);

  if (isLoading || isGenresLoading) return <div className="flex justify-center items-center h-64">Loading...</div>;
  if (error) return <div className="text-red-500">Error: {error}</div>;

  return (
    <div className="flex">
      <div className="w-64 p-4 border-r">
        <h2 className="text-lg font-semibold mb-4">Filters</h2>

        <div className="space-y-4">
          <Input
            placeholder="Search movies..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />

          <Select value={selectedGenre} onValueChange={(value) => setSelectedGenre(value)}>
            <SelectTrigger>
              <SelectValue placeholder="All Genres" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">All Genres</SelectItem>
              {genres.map((genre) => (
                <SelectItem key={genre.genreId} value={genre.name}>
                  {genre.name}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>

          <div className="flex items-center space-x-2">
            <Checkbox
              checked={showAvailableOnly}
              onCheckedChange={(checked) => setShowAvailableOnly(checked as boolean)}
            />
            <Label>Available only</Label>
          </div>

          <Select value={sortBy} onValueChange={(value) => setSortBy(value)}>
            <SelectTrigger>
              <SelectValue placeholder="Sort by" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="title">Title</SelectItem>
              <SelectItem value="rating">Rating</SelectItem>
              <SelectItem value="releaseDate">Release Date</SelectItem>
              <SelectItem value="duration">Duration</SelectItem>
            </SelectContent>
          </Select>

          <Select value={sortOrder} onValueChange={(value) => setSortOrder(value)}>
            <SelectTrigger>
              <SelectValue placeholder="Order" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="asc">Ascending</SelectItem>
              <SelectItem value="desc">Descending</SelectItem>
            </SelectContent>
          </Select>

          <Input
            type="number"
            placeholder="Min Duration"
            value={minDuration}
            onChange={(e) => setMinDuration(e.target.value)}
          />

          <Input
            type="number"
            placeholder="Max Duration"
            value={maxDuration}
            onChange={(e) => setMaxDuration(e.target.value)}
          />

          <Input
            type="date"
            placeholder="Release Date"
            value={releaseDate}
            onChange={(e) => setReleaseDate(e.target.value)}
          />

          <Input
            type="number"
            placeholder="Minimum Rating"
            value={rating}
            onChange={(e) => setRating(e.target.value)}
          />
        </div>
      </div>

      <div className="flex-1 p-4">
        <h1 className="text-3xl font-bold mb-8">Browse Movies</h1>

        {filteredMovies.length === 0 ? (
          <div className="text-center py-12">
            <h3 className="text-xl font-semibold mb-2">No movies found</h3>
            <p className="text-muted-foreground">Try adjusting your search or filter criteria</p>
          </div>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
            {filteredMovies.map((movie, index) => (
              <MovieCard key={`${movie.id || index}`} movie={movie} />
            ))}
          </div>
        )}
      </div>
    </div>
  );
}