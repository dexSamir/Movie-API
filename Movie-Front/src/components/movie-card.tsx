import { Link } from "react-router-dom";
import { Card, CardContent } from "../components/ui/card";
import { Badge } from "../components/ui/badge";
import { Star } from "lucide-react";
import type { Movie } from "../contexts/movie-context";

interface MovieCardProps {
  movie: Movie;
}

export function MovieCard({ movie }: MovieCardProps) {
  return (
    <Card className="overflow-hidden transition-all hover:shadow-lg">
      <Link to={`/movies/${movie.id}`}>
        <div className="relative aspect-[2/3] overflow-hidden">
          <img
            src={`https://localhost:7116/imgs/Movies/posters/${movie.posterUrl}`}
            alt={movie.title}
            className="w-full h-full object-cover transition-transform hover:scale-105"
          />
          <div className="absolute top-2 right-2">
            <Badge variant="secondary" className="flex items-center gap-1">
              <Star className="h-3 w-3 fill-current" />
              {movie.rating}
            </Badge>
          </div>
          {/* {!movie.available && (
            <div className="absolute inset-0 bg-black/60 flex items-center justify-center">
              <Badge variant="destructive" className="text-sm">
                Rented
              </Badge>
            </div>
          )} */}
        </div>
        <CardContent className="p-4">
          <h3 className="font-semibold line-clamp-1">{movie.title}</h3>
          <div className="flex justify-between items-center mt-2 text-sm text-muted-foreground">
            <span>{new Date(movie.releaseDate).getFullYear()}</span>
            <span>${movie.rentalPrice.toFixed(2)}</span>
          </div>
        </CardContent>
      </Link>
    </Card>
  );
}
