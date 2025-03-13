"use client";

import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { useMovies } from "../contexts/movie-context";
import { useAuth } from "../contexts/auth-context";
import { Button } from "../components/ui/button";
import { Badge } from "../components/ui/badge";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "../components/ui/tabs";
import { Textarea } from "../components/ui/textarea";
import { Avatar, AvatarFallback, AvatarImage } from "../components/ui/avatar";
import {
  Star,
  ThumbsUp,
  ThumbsDown,
  Clock,
  Calendar,
  Film,
  Users,
} from "lucide-react";
import type { Review } from "../contexts/movie-context";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "../components/ui/dialog";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from "../components/ui/alert-dialog";
import { useToast } from "../hooks/use-toast";
import { DialogTrigger } from "@radix-ui/react-dialog";
import { AlertDialogTrigger } from "@radix-ui/react-alert-dialog";

export interface Movie {
  id: string;
  title: string;
  description: string;
  posterUrl: string;
  trailerUrl: string;
  releaseDate: string;
  genres: { genreId: number; name: string }[];
  duration: number;
  rating: number;
  directorName: string;
  actors: {
    fullname: string;
    actorId: number;
    profilePhotoUrl: string;
  }[];
  rentalPrice: number;
  available: boolean;
}

export function MovieDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { toast } = useToast();
  const {
    getMovie,
    getMovieReviews,
    addReview,
    likeReview,
    dislikeReview,
    rentMovie,
  } = useMovies();
  const { user } = useAuth();

  const [movie, setMovie] = useState<Movie | null>(null);
  const [reviewText, setReviewText] = useState("");
  const [userRating, setUserRating] = useState(5);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [averageRating, setAverageRating] = useState<number | null>(null);
  const [totalWatchCount, setTotalWatchCount] = useState<number | null>(null);
  const [actors, setActors] = useState([]);
  const [reactions, setReactions] = useState<{
    likes: number;
    dislikes: number;
  }>({
    likes: 0,
    dislikes: 0,
  });

  useEffect(() => {
    fetch("https://localhost:7116/api/movies/getmoviebyid/" + id)
      .then((res) => res.json())
      .then((data: Movie) => setMovie(data))
      .catch((error) => console.error("Failed to fetch movie:", error));
  }, [id]);

  useEffect(() => {
    fetch("https://localhost:7116/api/actors/getall")
      .then((res) => res.json())
      .then((data) => setActors(data))
      .catch((error) => console.error("Failed to fetch actor:", error));
  }, [id]);

  const reviews = getMovieReviews(id || "");

  const fetchAverageRating = async () => {
    try {
      const response = await fetch(
        `https://localhost:7116/api/movies/getaveragerating/${id}/average-rating`
      );
      const data = await response.json();
      setAverageRating(data.averageRating);
    } catch (error) {
      console.error("Failed to fetch average rating:", error);
    }
  };

  const fetchTotalWatchCount = async () => {
    try {
      const response = await fetch(
        `https://localhost:7116/api/movies/gettotalwatchcount/${id}/total-watch-count`
      );
      const data = await response.json();
      setTotalWatchCount(data.totalWatchCount);
    } catch (error) {
      console.error("Failed to fetch total watch count:", error);
    }
  };

  const fetchMovieReactions = async () => {
    try {
      const response = await fetch(
        `https://localhost:7116/api/movies/getmoviereactions/${id}/reactions`
      );
      const data = await response.json();
      setReactions(data);
    } catch (error) {
      console.error("Failed to fetch movie reactions:", error);
    }
  };

  useEffect(() => {
    if (id) {
      fetchAverageRating();
      fetchTotalWatchCount();
      fetchMovieReactions();
    }
  }, [id]);

  const handleLikeMovie = async () => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to like movies",
        variant: "destructive",
      });
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7116/api/movies/likemovie/${id}/like`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ userId: user.id }),
        }
      );

      if (response.ok) {
        toast({
          title: "Liked",
          description: "You liked this movie",
        });
        fetchMovieReactions();
      } else {
        throw new Error("Failed to like movie");
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to like movie",
        variant: "destructive",
      });
    }
  };

  const handleDislikeMovie = async () => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to dislike movies",
        variant: "destructive",
      });
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7116/api/movies/dislikemovie/${id}/dislike`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ userId: user.id }),
        }
      );

      if (response.ok) {
        toast({
          title: "Disliked",
          description: "You disliked this movie",
        });
        fetchMovieReactions();
      } else {
        throw new Error("Failed to dislike movie");
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to dislike movie",
        variant: "destructive",
      });
    }
  };

  const handleUndoLikeMovie = async () => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to undo like",
        variant: "destructive",
      });
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7116/api/movies/undolikemovie/${id}/undo-like`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ userId: user.id }),
        }
      );

      if (response.ok) {
        toast({
          title: "Like Removed",
          description: "You removed your like from this movie",
        });
        fetchMovieReactions();
      } else {
        throw new Error("Failed to undo like");
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to undo like",
        variant: "destructive",
      });
    }
  };

  const handleUndoDislikeMovie = async () => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to undo dislike",
        variant: "destructive",
      });
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7116/api/movies/undodislikemovie/${id}/undo-dislike`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ userId: user.id }),
        }
      );

      if (response.ok) {
        toast({
          title: "Dislike Removed",
          description: "You removed your dislike from this movie",
        });
        fetchMovieReactions();
      } else {
        throw new Error("Failed to undo dislike");
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to undo dislike",
        variant: "destructive",
      });
    }
  };

  const handleSubmitReview = async () => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to submit a review",
        variant: "destructive",
      });
      return;
    }

    if (!reviewText.trim()) {
      toast({
        title: "Review required",
        description: "Please enter your review",
        variant: "destructive",
      });
      return;
    }

    setIsSubmitting(true);

    try {
      const response = await fetch(
        "https://localhost:7116/api/reviews/addreview",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            movieId: id,
            userId: user.id,
            userName: user.name,
            userAvatar: user.avatar,
            rating: userRating,
            comment: reviewText,
          }),
        }
      );

      if (response.ok) {
        toast({
          title: "Review submitted",
          description: "Thank you for your feedback!",
        });
        setReviewText("");
        setUserRating(5);
        getMovieReviews(id || "");
      } else {
        throw new Error("Failed to submit review");
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to submit review",
        variant: "destructive",
      });
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleLikeReview = async (reviewId: string) => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to like reviews",
        variant: "destructive",
      });
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7116/api/reviews/likereview/${reviewId}/like`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ userId: user.id }),
        }
      );

      if (response.ok) {
        toast({
          title: "Liked",
          description: "You liked this review",
        });
        getMovieReviews(id || "");
      } else {
        throw new Error("Failed to like review");
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to like review",
        variant: "destructive",
      });
    }
  };

  const handleDislikeReview = async (reviewId: string) => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to dislike reviews",
        variant: "destructive",
      });
      return;
    }

    try {
      const response = await fetch(
        `https://localhost:7116/api/reviews/dislikereview/${reviewId}/dislike`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ userId: user.id }),
        }
      );

      if (response.ok) {
        toast({
          title: "Disliked",
          description: "You disliked this review",
        });
        getMovieReviews(id || "");
      } else {
        throw new Error("Failed to dislike review");
      }
    } catch (error) {
      toast({
        title: "Error",
        description: "Failed to dislike review",
        variant: "destructive",
      });
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString("en-US", {
      year: "numeric",
      month: "long",
      day: "numeric",
    });
  };

  const formatRuntime = (minutes: number) => {
    const hours = Math.floor(minutes / 60);
    const mins = minutes % 60;
    return `${hours}h ${mins}m`;
  };

  if (!movie) {
    return (
      <div className="text-center py-16">
        <h2 className="text-2xl font-bold mb-4">Movie not found</h2>
        <Button onClick={() => navigate("/movies")}>Back to Movies</Button>
      </div>
    );
  }

  return (
    <div className="space-y-8">
      <div className="space-y-8">
        <div className="h-[500px] overflow-hidden rounded-lg w-11/12 mx-auto ">
          <video
            src={`https://localhost:7116/imgs/Movies/trailers/${movie.trailerUrl}`}
            controls
            className="w-full h-full object-cover"
            poster={`https://localhost:7116/imgs/Movies/posters/${movie.posterUrl}`} 
          />
        </div>

        <div className="p-8">
          <h1 className="text-4xl font-bold mb-2">{movie.title}</h1>
          <div className="flex items-center space-x-4 mb-4">
            <div className="flex items-center">
              <Star className="h-5 w-5 text-yellow-400 fill-yellow-400 mr-1" />
              <span>{averageRating?.toFixed(1) || "N/A"}/10</span>
            </div>
            <span>•</span>
            <span>{formatDate(movie.releaseDate)}</span>
            <span>•</span>
            <span>{formatRuntime(movie.duration)}</span>
          </div>
          <div className="flex flex-wrap gap-2 mb-6">
            {movie.genres.map((genre) => (
              <Badge key={genre.genreId} variant="secondary">
                {genre.name}
              </Badge>
            ))}
          </div>
          <div className="flex space-x-4">
            {movie.available ? (
              <AlertDialog>
                <AlertDialogTrigger asChild>
                  <Button>Rent for ${movie.rentalPrice.toFixed(2)}</Button>
                </AlertDialogTrigger>
                <AlertDialogContent>
                  <AlertDialogHeader>
                    <AlertDialogTitle>Confirm Rental</AlertDialogTitle>
                    <AlertDialogDescription>
                      You are about to rent "{movie.title}" for $
                      {movie.rentalPrice}. The rental period is 7 days.
                    </AlertDialogDescription>
                  </AlertDialogHeader>
                  <AlertDialogFooter>
                    <AlertDialogCancel>Cancel</AlertDialogCancel>
                    <AlertDialogAction
                      onClick={() => rentMovie(movie.id, user?.id || "")}
                    >
                      Confirm
                    </AlertDialogAction>
                  </AlertDialogFooter>
                </AlertDialogContent>
              </AlertDialog>
            ) : (
              <Button disabled>Currently Unavailable</Button>
            )}
            <Dialog>
              <DialogTrigger asChild>
                <Button variant="outline">Write a Review</Button>
              </DialogTrigger>
              <DialogContent>
                <DialogHeader>
                  <DialogTitle>Write a Review</DialogTitle>
                  <DialogDescription>
                    Share your thoughts about {movie.title}
                  </DialogDescription>
                </DialogHeader>
                <div className="space-y-4 py-4">
                  <div className="flex items-center justify-center space-x-1">
                    {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((rating) => (
                      <button
                        key={rating}
                        type="button"
                        onClick={() => setUserRating(rating)}
                        className={`p-1 ${
                          rating <= userRating
                            ? "text-yellow-400"
                            : "text-gray-300"
                        }`}
                      >
                        <Star
                          className={`h-6 w-6 ${
                            rating <= userRating ? "fill-yellow-400" : ""
                          }`}
                        />
                      </button>
                    ))}
                  </div>
                  <Textarea
                    placeholder="Write your review here..."
                    value={reviewText}
                    onChange={(e) => setReviewText(e.target.value)}
                    rows={5}
                  />
                </div>
                <DialogFooter>
                  <Button
                    onClick={handleSubmitReview}
                    disabled={isSubmitting || !reviewText.trim()}
                  >
                    {isSubmitting ? "Submitting..." : "Submit Review"}
                  </Button>
                </DialogFooter>
              </DialogContent>
            </Dialog>
            <div className="flex items-center space-x-4">
              <Button variant="ghost" size="lg" onClick={handleLikeMovie}>
                <ThumbsUp className="h-5 w-5 mr-1" />{" "}
                <span>{reactions.likes}</span>
              </Button>
              <Button variant="ghost" size="lg" onClick={handleDislikeMovie}>
                <ThumbsDown className="h-5 w-5 mr-1 " />{" "}
                <span>{reactions.dislikes}</span>
              </Button>
            </div>
          </div>
        </div>
      </div>
      <Tabs defaultValue="overview" className="w-full">
        <TabsList className="grid w-full grid-cols-3">
          <TabsTrigger value="overview">Overview</TabsTrigger>
          <TabsTrigger value="cast">Cast & Crew</TabsTrigger>
          <TabsTrigger value="reviews">Reviews</TabsTrigger>
        </TabsList>

        <TabsContent value="overview" className="space-y-6">
          <div className="grid grid-cols-1 md:grid-cols-[300px_1fr] gap-8">
            <div>
              <img
                src={`https://localhost:7116/imgs/Movies/posters/${movie.posterUrl}`}
                alt={movie.title}
                className="w-full rounded-lg shadow-lg"
              />
            </div>
            <div className="space-y-6">
              <div>
                <h2 className="text-2xl font-semibold mb-4">Synopsis</h2>
                <p className="text-muted-foreground">{movie.description}</p>
              </div>

              <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
                <div className="flex items-center">
                  <Calendar className="h-5 w-5 mr-2 text-muted-foreground" />
                  <div>
                    <p className="text-sm text-muted-foreground">
                      Release Date
                    </p>
                    <p>{formatDate(movie.releaseDate)}</p>
                  </div>
                </div>
                <div className="flex items-center">
                  <Clock className="h-5 w-5 mr-2 text-muted-foreground" />
                  <div>
                    <p className="text-sm text-muted-foreground">Runtime</p>
                    <p>{formatRuntime(movie.duration)}</p>
                  </div>
                </div>
                <div className="flex items-center">
                  <Film className="h-5 w-5 mr-2 text-muted-foreground" />
                  <div>
                    <p className="text-sm text-muted-foreground">Director</p>
                    <p>{movie.directorName}</p>
                  </div>
                </div>
                <div className="flex items-center">
                  <Users className="h-5 w-5 mr-2 text-muted-foreground" />
                  <div>
                    <p className="text-sm text-muted-foreground">Genres</p>
                    <p>{movie.genres.map((genre) => genre.name).join(", ")}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </TabsContent>

        <TabsContent value="cast" className="space-y-6">
          <div>
            <h2 className="text-2xl font-semibold mb-6">Director</h2>
            <div className="flex flex-wrap gap-6">
              <div className="text-center">
                <Avatar className="h-24 w-24 mb-2">
                  <AvatarImage
                    src="/placeholder.svg?height=96&width=96"
                    alt={movie.directorName}
                  />
                  <AvatarFallback>{movie.directorName}</AvatarFallback>
                </Avatar>
                <p className="font-medium">{movie.directorName}</p>
                <p className="text-sm text-muted-foreground">Director</p>
              </div>
            </div>
          </div>

          <div>
            <h2 className="text-2xl font-semibold mb-6">Cast</h2>
            <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-6">
              {movie.actors.map((data) => (
                <div key={data.fullname} className="text-start">
                  <Avatar className="h-24 w-24 mb-2">
                    <AvatarFallback>{data.fullname}</AvatarFallback>
                    <AvatarImage
                      src={`https://localhost:7116/imgs/actors/${data.profilePhotoUrl}`}
                      alt={data.profilePhotoUrl}
                    />
                  </Avatar>
                  <p className="font-medium inline">{data.fullname}</p>
                </div>
              ))}
            </div>
          </div>
        </TabsContent>

        <TabsContent value="reviews" className="space-y-6">
          <div className="flex justify-between items-center">
            <h2 className="text-2xl font-semibold">User Reviews</h2>
            <Dialog>
              <DialogContent>
                <DialogHeader>
                  <DialogTitle>Write a Review</DialogTitle>
                  <DialogDescription>
                    Share your thoughts about {movie.title}
                  </DialogDescription>
                </DialogHeader>
                <div className="space-y-4 py-4">
                  <div className="flex items-center justify-center space-x-1">
                    {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((rating) => (
                      <button
                        key={rating}
                        type="button"
                        onClick={() => setUserRating(rating)}
                        className={`p-1 ${
                          rating <= userRating
                            ? "text-yellow-400"
                            : "text-gray-300"
                        }`}
                      >
                        <Star
                          className={`h-6 w-6 ${
                            rating <= userRating ? "fill-yellow-400" : ""
                          }`}
                        />
                      </button>
                    ))}
                  </div>
                  <Textarea
                    placeholder="Write your review here..."
                    value={reviewText}
                    onChange={(e) => setReviewText(e.target.value)}
                    rows={5}
                  />
                </div>
                <DialogFooter>
                  <Button
                    onClick={handleSubmitReview}
                    disabled={isSubmitting || !reviewText.trim()}
                  >
                    {isSubmitting ? "Submitting..." : "Submit Review"}
                  </Button>
                </DialogFooter>
              </DialogContent>
            </Dialog>
          </div>

          {reviews.length === 0 ? (
            <div className="text-center py-12 border rounded-lg">
              <h3 className="text-xl font-semibold mb-2">No reviews yet</h3>
              <p className="text-muted-foreground mb-4">
                Be the first to review this movie
              </p>
              <Dialog>
                <DialogTrigger asChild>
                  <Button>Write a Review</Button>
                </DialogTrigger>
                <DialogContent>
                  <DialogHeader>
                    <DialogTitle>Write a Review</DialogTitle>
                    <DialogDescription>
                      Share your thoughts about {movie.title}
                    </DialogDescription>
                  </DialogHeader>
                  <div className="space-y-4 py-4">
                    <div className="flex items-center justify-center space-x-1">
                      {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map((rating) => (
                        <button
                          key={rating}
                          type="button"
                          onClick={() => setUserRating(rating)}
                          className={`p-1 ${
                            rating <= userRating
                              ? "text-yellow-400"
                              : "text-gray-300"
                          }`}
                        >
                          <Star
                            className={`h-6 w-6 ${
                              rating <= userRating ? "fill-yellow-400" : ""
                            }`}
                          />
                        </button>
                      ))}
                    </div>
                    <Textarea
                      placeholder="Write your review here..."
                      value={reviewText}
                      onChange={(e) => setReviewText(e.target.value)}
                      rows={5}
                    />
                  </div>
                  <DialogFooter>
                    <Button
                      onClick={handleSubmitReview}
                      disabled={isSubmitting || !reviewText.trim()}
                    >
                      {isSubmitting ? "Submitting..." : "Submit Review"}
                    </Button>
                  </DialogFooter>
                </DialogContent>
              </Dialog>
            </div>
          ) : (
            <div className="space-y-6">
              {reviews.map((review: Review) => (
                <div key={review.id} className="border rounded-lg p-6">
                  <div className="flex justify-between items-start mb-4">
                    <div className="flex items-center">
                      <Avatar className="h-10 w-10 mr-3">
                        <AvatarImage
                          src={review.userAvatar}
                          alt={review.userName}
                        />
                        <AvatarFallback>{review.userName}</AvatarFallback>
                      </Avatar>
                      <div>
                        <p className="font-medium">{review.userName}</p>
                        <div className="flex items-center">
                          <div className="flex mr-2">
                            {[...Array(10)].map((_, i) => (
                              <Star
                                key={i}
                                className={`h-4 w-4 ${
                                  i < review.rating
                                    ? "text-yellow-400 fill-yellow-400"
                                    : "text-gray-300"
                                }`}
                              />
                            ))}
                          </div>
                          <span className="text-sm text-muted-foreground">
                            {new Date(review.date).toLocaleDateString()}
                          </span>
                        </div>
                      </div>
                    </div>
                  </div>
                  <p className="mb-4">{review.comment}</p>
                  <div className="flex items-center space-x-4">
                    <Button
                      variant="ghost"
                      size="sm"
                      className={review.userLiked ? "text-primary" : ""}
                      onClick={() => handleLikeReview(review.id)}
                    >
                      <ThumbsUp className="h-4 w-4 mr-1" />
                      <span>{review.likes}</span>
                    </Button>
                    <Button
                      variant="ghost"
                      size="sm"
                      className={review.userDisliked ? "text-primary" : ""}
                      onClick={() => handleDislikeReview(review.id)}
                    >
                      <ThumbsDown className="h-4 w-4 mr-1" />
                      <span>{review.dislikes}</span>
                    </Button>
                  </div>
                </div>
              ))}
            </div>
          )}
        </TabsContent>
      </Tabs>
    </div>
  );
}
