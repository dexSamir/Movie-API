import type { Movie } from "../contexts/movie-context";

export const mockMovies: Movie[] = [
  {
    id: "1",
    title: "Inception",
    description:
      "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O.",
    posterUrl: "/placeholder.svg?height=450&width=300",
    backdropUrl: "/placeholder.svg?height=720&width=1280",
    releaseDate: "2010-07-16",
    genres: ["Action", "Adventure", "Sci-Fi"],
    duration: 148,
    rating: 8.8,
    directors: ["Christopher Nolan"],
    actors: [
      {
        name: "Leonardo DiCaprio",
        character: "Cobb",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Joseph Gordon-Levitt",
        character: "Arthur",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Ellen Page",
        character: "Ariadne",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Tom Hardy",
        character: "Eames",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
    ],
    rentalPrice: 4.99,
    available: true,
  },
  {
    id: "2",
    title: "The Shawshank Redemption",
    description:
      "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
    posterUrl: "/placeholder.svg?height=450&width=300",
    backdropUrl: "/placeholder.svg?height=720&width=1280",
    releaseDate: "1994-10-14",
    genres: ["Drama"],
    duration: 142,
    rating: 9.3,
    directors: ["Frank Darabont"],
    actors: [
      {
        name: "Tim Robbins",
        character: "Andy Dufresne",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Morgan Freeman",
        character: "Ellis Boyd 'Red' Redding",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Bob Gunton",
        character: "Warden Norton",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
    ],
    rentalPrice: 3.99,
    available: true,
  },
  {
    id: "3",
    title: "The Dark Knight",
    description:
      "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
    posterUrl: "/placeholder.svg?height=450&width=300",
    backdropUrl: "/placeholder.svg?height=720&width=1280",
    releaseDate: "2008-07-18",
    genres: ["Action", "Crime", "Drama"],
    duration: 152,
    rating: 9.0,
    directors: ["Christopher Nolan"],
    actors: [
      {
        name: "Christian Bale",
        character: "Bruce Wayne",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Heath Ledger",
        character: "Joker",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Aaron Eckhart",
        character: "Harvey Dent",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Michael Caine",
        character: "Alfred",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
    ],
    rentalPrice: 4.99,
    available: true,
  },
  {
    id: "4",
    title: "Pulp Fiction",
    description:
      "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.",
    posterUrl: "/placeholder.svg?height=450&width=300",
    backdropUrl: "/placeholder.svg?height=720&width=1280",
    releaseDate: "1994-10-14",
    genres: ["Crime", "Drama"],
    duration: 154,
    rating: 8.9,
    directors: ["Quentin Tarantino"],
    actors: [
      {
        name: "John Travolta",
        character: "Vincent Vega",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Samuel L. Jackson",
        character: "Jules Winnfield",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Uma Thurman",
        character: "Mia Wallace",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Bruce Willis",
        character: "Butch Coolidge",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
    ],
    rentalPrice: 3.99,
    available: true,
  },
  {
    id: "5",
    title: "The Lord of the Rings: The Fellowship of the Ring",
    description:
      "A meek Hobbit from the Shire and eight companions set out on a journey to destroy the powerful One Ring and save Middle-earth from the Dark Lord Sauron.",
    posterUrl: "/placeholder.svg?height=450&width=300",
    backdropUrl: "/placeholder.svg?height=720&width=1280",
    releaseDate: "2001-12-19",
    genres: ["Adventure", "Drama", "Fantasy"],
    duration: 178,
    rating: 8.8,
    directors: ["Peter Jackson"],
    actors: [
      {
        name: "Elijah Wood",
        character: "Frodo Baggins",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Ian McKellen",
        character: "Gandalf",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Viggo Mortensen",
        character: "Aragorn",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
    ],
    rentalPrice: 4.99,
    available: true,
  },
  {
    id: "6",
    title: "Forrest Gump",
    description:
      "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
    posterUrl: "/placeholder.svg?height=450&width=300",
    backdropUrl: "/placeholder.svg?height=720&width=1280",
    releaseDate: "1994-07-06",
    genres: ["Drama", "Romance"],
    duration: 142,
    rating: 8.8,
    directors: ["Robert Zemeckis"],
    actors: [
      {
        name: "Tom Hanks",
        character: "Forrest Gump",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Robin Wright",
        character: "Jenny Curran",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
      {
        name: "Gary Sinise",
        character: "Lieutenant Dan Taylor",
        photoUrl: "/placeholder.svg?height=100&width=100",
      },
    ],
    rentalPrice: 3.99,
    available: true,
  },
];
