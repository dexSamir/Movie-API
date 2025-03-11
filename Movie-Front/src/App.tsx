import { BrowserRouter as Router } from "react-router-dom"
import { ThemeProvider } from "./components/theme-provider"
import { Toast } from "./components/ui/toast"
import { AppRoutes } from "./routes"
import { AuthProvider } from "./contexts/auth-context"
import { MovieProvider } from "./contexts/movie-context"
import { ToastProvider } from "@radix-ui/react-toast";

function App() {
  return (
    <ThemeProvider defaultTheme="light" storageKey="movie-rental-theme">
      <AuthProvider>
        <MovieProvider>
          <Router>
            <AppRoutes />
            
            <ToastProvider >
                <Toast />
            </ToastProvider>
          </Router>
        </MovieProvider>
      </AuthProvider>
    </ThemeProvider>
  )
}

export default App

