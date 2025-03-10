"use client";

import { useState } from "react";
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
  DialogTrigger,
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
  AlertDialogTrigger,
} from "../components/ui/alert-dialog";
import { useToast } from "../hooks/use-toast";

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

  const movie = getMovie(id || "");
  const reviews = getMovieReviews(id || "");

  const [reviewText, setReviewText] = useState("");
  const [userRating, setUserRating] = useState(5);
  const [isSubmitting, setIsSubmitting] = useState(false);

  if (!movie) {
    return (
      <div className="text-center py-16">
        <h2 className="text-2xl font-bold mb-4">Movie not found</h2>
        <Button onClick={() => navigate("/movies")}>Back to Movies</Button>
      </div>
    );
  }

  const handleSubmitReview = () => {
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

    // Add the review
    addReview({
      movieId: movie.id,
      userId: user.id,
      userName: user.name,
      userAvatar: user.avatar,
      rating: userRating,
      comment: reviewText,
    });

    // Reset form
    setReviewText("");
    setUserRating(5);
    setIsSubmitting(false);

    toast({
      title: "Review submitted",
      description: "Thank you for your feedback!",
    });
  };

  const handleRentMovie = () => {
    if (!user) {
      toast({
        title: "Authentication required",
        description: "Please log in to rent movies",
        variant: "destructive",
      });
      return;
    }

    rentMovie(movie.id, user.id);

    toast({
      title: "Movie rented successfully",
      description: `You have rented ${movie.title} for 7 days.`,
    });
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

  return (
    <div className="space-y-8">
      {/* Movie Hero Section */}
      <div className="relative h-[500px] overflow-hidden rounded-lg">
        <img
          src={movie.backdropUrl || "/placeholder.svg"}
          alt={movie.title}
          className="w-full h-full object-cover"
        />
        <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/50 to-transparent" />
        <div className="absolute bottom-0 left-0 p-8 text-white">
          <h1 className="text-4xl font-bold mb-2">{movie.title}</h1>
          <div className="flex items-center space-x-4 mb-4">
            <div className="flex items-center">
              <Star className="h-5 w-5 text-yellow-400 fill-yellow-400 mr-1" />
              <span>{movie.rating.toFixed(1)}/10</span>
            </div>
            <span>•</span>
            <span>{formatDate(movie.releaseDate)}</span>
            <span>•</span>
            <span>{formatRuntime(movie.duration)}</span>
          </div>
          <div className="flex flex-wrap gap-2 mb-6">
            {movie.genres.map((genre) => (
              <Badge key={genre} variant="secondary">
                {genre}
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
                      {movie.rentalPrice.toFixed(2)}. The rental period is 7
                      days.
                    </AlertDialogDescription>
                  </AlertDialogHeader>
                  <AlertDialogFooter>
                    <AlertDialogCancel>Cancel</AlertDialogCancel>
                    <AlertDialogAction onClick={handleRentMovie}>
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
          </div>
        </div>
      </div>

      {/* Movie Details Tabs */}
      <Tabs defaultValue="overview" className="w-full">
        <TabsList className="grid w-full grid-cols-3">
          <TabsTrigger value="overview">Overview</TabsTrigger>
          <TabsTrigger value="cast">Cast & Crew</TabsTrigger>
          <TabsTrigger value="reviews">Reviews</TabsTrigger>
        </TabsList>

        {/* Overview Tab */}
        <TabsContent value="overview" className="space-y-6">
          <div className="grid grid-cols-1 md:grid-cols-[300px_1fr] gap-8">
            <div>
              <img
                src={movie.posterUrl || "/placeholder.svg"}
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
                    <p>{movie.directors.join(", ")}</p>
                  </div>
                </div>
                <div className="flex items-center">
                  <Users className="h-5 w-5 mr-2 text-muted-foreground" />
                  <div>
                    <p className="text-sm text-muted-foreground">Genres</p>
                    <p>{movie.genres.join(", ")}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </TabsContent>

        {/* Cast & Crew Tab */}
        <TabsContent value="cast" className="space-y-6">
          <div>
            <h2 className="text-2xl font-semibold mb-6">Directors</h2>
            <div className="flex flex-wrap gap-6">
              {movie.directors.map((director) => (
                <div key={director} className="text-center">
                  <Avatar className="h-24 w-24 mb-2">
                    <AvatarImage
                      src="/placeholder.svg?height=96&width=96"
                      alt={director}
                    />
                    <AvatarFallback>{director.charAt(0)}</AvatarFallback>
                  </Avatar>
                  <p className="font-medium">{director}</p>
                  <p className="text-sm text-muted-foreground">Director</p>
                </div>
              ))}
            </div>
          </div>

          <div>
            <h2 className="text-2xl font-semibold mb-6">Cast</h2>
            <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-6">
              {movie.actors.map((actor) => (
                <div key={actor.name} className="text-center">
                  <Avatar className="h-24 w-24 mb-2">
                    <AvatarImage src={actor.photoUrl} alt={actor.name} />
                    <AvatarFallback>{actor.name.charAt(0)}</AvatarFallback>
                  </Avatar>
                  <p className="font-medium">{actor.name}</p>
                  <p className="text-sm text-muted-foreground">
                    {actor.character}
                  </p>
                </div>
              ))}
            </div>
          </div>
        </TabsContent>

        {/* Reviews Tab */}
        <TabsContent value="reviews" className="space-y-6">
          <div className="flex justify-between items-center">
            <h2 className="text-2xl font-semibold">User Reviews</h2>
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
                        <AvatarFallback>
                          {review.userName.charAt(0)}
                        </AvatarFallback>
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
                      onClick={() => likeReview(review.id)}
                    >
                      <ThumbsUp className="h-4 w-4 mr-1" />
                      <span>{review.likes}</span>
                    </Button>
                    <Button
                      variant="ghost"
                      size="sm"
                      className={review.userDisliked ? "text-primary" : ""}
                      onClick={() => dislikeReview(review.id)}
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
