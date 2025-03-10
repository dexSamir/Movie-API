import { BrowserRouter as Router } from "react-router-dom"
import { ThemeProvider } from "./components/theme-provider"
import { Toaster } from "./components/ui/toaster"
import { AppRoutes } from "./routes"
import { AuthProvider } from "./contexts/auth-context"
import { MovieProvider } from "./contexts/movie-context"

function App() {
  return (
    <ThemeProvider defaultTheme="light" storageKey="movie-rental-theme">
      <AuthProvider>
        <MovieProvider>
          <Router>
            <AppRoutes />
            <Toaster />
          </Router>
        </MovieProvider>
      </AuthProvider>
    </ThemeProvider>
  )
}

export default App

