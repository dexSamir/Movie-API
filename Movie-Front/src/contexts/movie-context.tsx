"use client";

import { createContext, useContext, useState, useEffect } from "react";

export interface Movie {
  id: string;
  title: string;
  description: string;
  posterUrl: string;
  trailerUrl: string;
  releaseDate: string;
  genres: string[];
  duration: number;
  rating: number;
  directors: string[];
  actors: {
    name: string;
    character: string;
    photoUrl?: string;
  }[];
  rentalPrice: number;
  available: boolean;
}

export interface Review {
  id: string;
  movieId: string;
  userId: string;
  userName: string;
  userAvatar?: string;
  rating: number;
  comment: string;
  date: string;
  likes: number;
  dislikes: number;
  userLiked?: boolean;
  userDisliked?: boolean;
}

export interface Rental {
  id: string;
  userId: string;
  movieId: string;
  movie: Movie;
  rentalDate: string;
  returnDate: string;
  returned: boolean;
}

interface MovieContextType {
  movies: Movie[];
  reviews: Review[];
  rentals: Rental[];
  getMovie: (id: string) => Movie | undefined;
  getMovieReviews: (movieId: string) => Review[];
  addReview: (
    review: Omit<Review, "id" | "date" | "likes" | "dislikes">
  ) => void;
  likeReview: (reviewId: string) => void;
  dislikeReview: (reviewId: string) => void;
  rentMovie: (movieId: string, userId: string) => void;
  returnMovie: (rentalId: string) => void;
  getUserRentals: (userId: string) => Rental[];
  isLoading: boolean;
}

const MovieContext = createContext<MovieContextType | undefined>(undefined);

export function MovieProvider({ children }: { children: React.ReactNode }) {
  const [movies, setMovies] = useState<Movie[]>([]);
  const [reviews, setReviews] = useState<Review[]>([]);
  const [rentals, setRentals] = useState<Rental[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const response = await fetch(
          "https://localhost:7116/api/movies/getallmovies"
        );
        const data = await response.json();
        setMovies(data);
      } catch (error) {
        console.error("Failed to fetch movies:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchMovies();
  }, []);

  const getMovie = (id: string) => {
    return movies.find((movie) => movie.id === id);
  };

  const getMovieReviews = (movieId: string) => {
    return reviews.filter((review) => review.movieId === movieId);
  };

  const addReview = (
    review: Omit<Review, "id" | "date" | "likes" | "dislikes">
  ) => {
    const newReview: Review = {
      ...review,
      id: Date.now().toString(),
      date: new Date().toISOString(),
      likes: 0,
      dislikes: 0,
    };

    setReviews((prevReviews) => [...prevReviews, newReview]);
  };

  const likeReview = (reviewId: string) => {
    setReviews((prevReviews) =>
      prevReviews.map((review) => {
        if (review.id === reviewId) {
          if (review.userLiked) {
            return {
              ...review,
              likes: review.likes - 1,
              userLiked: false,
            };
          } else {
            const dislikesChange = review.userDisliked ? -1 : 0;
            return {
              ...review,
              likes: review.likes + 1,
              dislikes: review.dislikes + dislikesChange,
              userLiked: true,
              userDisliked: false,
            };
          }
        }
        return review;
      })
    );
  };

  const dislikeReview = (reviewId: string) => {
    setReviews((prevReviews) =>
      prevReviews.map((review) => {
        if (review.id === reviewId) {
          if (review.userDisliked) {
            return {
              ...review,
              dislikes: review.dislikes - 1,
              userDisliked: false,
            };
          } else {
            const likesChange = review.userLiked ? -1 : 0;
            return {
              ...review,
              dislikes: review.dislikes + 1,
              likes: review.likes + likesChange,
              userDisliked: true,
              userLiked: false,
            };
          }
        }
        return review;
      })
    );
  };

  const rentMovie = (movieId: string, userId: string) => {
    const movie = getMovie(movieId);
    if (!movie) return;

    const rentalDate = new Date();
    const returnDate = new Date();
    returnDate.setDate(returnDate.getDate() + 7);

    const newRental: Rental = {
      id: Date.now().toString(),
      userId,
      movieId,
      movie,
      rentalDate: rentalDate.toISOString(),
      returnDate: returnDate.toISOString(),
      returned: false,
    };

    setRentals((prevRentals) => [...prevRentals, newRental]);

    setMovies((prevMovies) =>
      prevMovies.map((movie) =>
        movie.id === movieId ? { ...movie, available: false } : movie
      )
    );
  };

  const returnMovie = (rentalId: string) => {
    setRentals((prevRentals) =>
      prevRentals.map((rental) => {
        if (rental.id === rentalId) {
          setMovies((prevMovies) =>
            prevMovies.map((movie) =>
              movie.id === rental.movieId
                ? { ...movie, available: true }
                : movie
            )
          );

          return { ...rental, returned: true };
        }
        return rental;
      })
    );
  };

  const getUserRentals = (userId: string) => {
    return rentals.filter((rental) => rental.userId === userId);
  };

  return (
    <MovieContext.Provider
      value={{
        movies,
        reviews,
        rentals,
        getMovie,
        getMovieReviews,
        addReview,
        likeReview,
        dislikeReview,
        rentMovie,
        returnMovie,
        getUserRentals,
        isLoading,
      }}
    >
      {children}
    </MovieContext.Provider>
  );
}

export function useMovies() {
  const context = useContext(MovieContext);
  if (context === undefined) {
    throw new Error("useMovies must be used within a MovieProvider");
  }
  return context;
}
