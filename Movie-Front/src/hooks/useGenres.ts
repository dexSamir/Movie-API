import { useState, useEffect } from "react";

export interface Genre {
  genreId: string;
  name: string;
}

export function useGenres() {
  const [genres, setGenres] = useState<Genre[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function fetchGenres() {
      try {
        const response = await fetch(
          "https://localhost:7116/api/genres/getall"
        );
        if (!response.ok) {
          throw new Error("Failed to fetch genres");
        }
        const data = await response.json();
        setGenres(data);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setIsLoading(false);
      }
    }

    fetchGenres();
  }, []);
  
  return { genres, isLoading, error };
}
