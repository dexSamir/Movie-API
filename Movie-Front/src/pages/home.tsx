"use client";

import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { useMovies } from "../contexts/movie-context";
import { MovieCard } from "../components/movie-card";
import { Button } from "../components/ui/button";
import {
  Carousel,
  CarouselContent,
  CarouselItem,
  CarouselPrevious,
  CarouselNext,
} from "../components/ui/carousel";

export function HomePage() {
  const { movies, isLoading } = useMovies();
  const [featuredMovies, setFeaturedMovies] = useState<typeof movies>([]);
  const [newReleases, setNewReleases] = useState<typeof movies>([]);
  const [topRated, setTopRated] = useState<typeof movies>([]);

  useEffect(() => {
    if (movies.length > 0) {
      const featured = [...movies].sort(() => 0.5 - Math.random()).slice(0, 3);
      setFeaturedMovies(featured);

      const releases = [...movies]
        .sort(
          (a, b) =>
            new Date(b.releaseDate).getTime() -
            new Date(a.releaseDate).getTime()
        )
        .slice(0, 6);
      setNewReleases(releases);

      const rated = [...movies].sort((a, b) => b.rating - a.rating).slice(0, 6);
      setTopRated(rated);
    }
  }, [movies]);

  if (isLoading) {
    return (
      <div className="flex justify-center items-center h-64">Loading...</div>
    );
  }

  return (
    <div className="space-y-12">
      <section>
        <Carousel className="w-full">
          <CarouselContent>
            {featuredMovies.map((movie) => (
              <CarouselItem key={movie.id}>
                <div className="relative h-[500px] w-full overflow-hidden rounded-lg">
                  <img
                    src={movie.backdropUrl || "/placeholder.svg"}
                    alt={movie.title}
                    className="w-full h-full object-cover"
                  />
                  <div className="absolute inset-0 bg-gradient-to-t from-black/80 to-transparent" />
                  <div className="absolute bottom-0 left-0 p-8 text-white">
                    <h2 className="text-4xl font-bold mb-2">{movie.title}</h2>
                    <p className="text-lg mb-4 max-w-2xl">
                      {movie.description}
                    </p>
                    <div className="flex space-x-4">
                      <Button asChild>
                        <Link to={`/movies/${movie.id}`}>Watch Now</Link>
                      </Button>
                      <Button variant="outline">
                        <Link to={`/movies/${movie.id}`}>More Info</Link>
                      </Button>
                    </div>
                  </div>
                </div>
              </CarouselItem>
            ))}
          </CarouselContent>
          <CarouselPrevious className="left-4" />
          <CarouselNext className="right-4" />
        </Carousel>
      </section>

      <section>
        <div className="flex justify-between items-center mb-6">
          <h2 className="text-3xl font-bold">New Releases</h2>
          <Button variant="ghost" asChild>
            <Link to="/movies">View All</Link>
          </Button>
        </div>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-6">
          {newReleases.map((movie) => (
            <MovieCard key={movie.id} movie={movie} />
          ))}
        </div>
      </section>

      <section>
        <div className="flex justify-between items-center mb-6">
          <h2 className="text-3xl font-bold">Top Rated</h2>
          <Button variant="ghost" asChild>
            <Link to="/movies?sort=rating">View All</Link>
          </Button>
        </div>
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-6">
          {topRated.map((movie) => (
            <MovieCard key={movie.id} movie={movie} />
          ))}
        </div>
      </section>
    </div>
  );
}