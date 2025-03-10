import { Routes, Route } from "react-router-dom"
import Layout from "./components/layout"
import { HomePage } from "./pages/home"
import { MoviesPage } from "./pages/movies"
import { MovieDetailPage } from "./pages/movie-detail"
import { LoginPage } from "./pages/login"
import { RegisterPage } from "./pages/register"
import { ProfilePage } from "./pages/profile"
import { RentalsPage } from "./pages/rental"
import { NotFoundPage } from "./pages/not-found"
import { ProtectedRoute } from "./components/protected-route"

export function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<HomePage />} />
        <Route path="movies" element={<MoviesPage />} />
        <Route path="movies/:id" element={<MovieDetailPage />} />
        <Route path="login" element={<LoginPage />} />
        <Route path="register" element={<RegisterPage />} />
        <Route
          path="profile"
          element={
            <ProtectedRoute>
              <ProfilePage />
            </ProtectedRoute>
          }
        />
        <Route
          path="rentals"
          element={
            <ProtectedRoute>
              <RentalsPage />
            </ProtectedRoute>
          }
        />
        <Route path="*" element={<NotFoundPage />} />
      </Route>
    </Routes>
  )
}

