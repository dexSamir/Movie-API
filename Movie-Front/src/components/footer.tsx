import { Link } from "react-router-dom"

export function Footer() {
  return (
    <footer className="border-t py-6 md:py-8">
      <div className="container mx-auto px-4">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          <div>
            <h3 className="text-lg font-semibold mb-4">MovieRental</h3>
            <p className="text-muted-foreground">
              Your one-stop destination for renting the latest and greatest movies.
            </p>
          </div>

          <div>
            <h3 className="text-lg font-semibold mb-4">Quick Links</h3>
            <ul className="space-y-2">
              <li>
                <Link to="/" className="text-muted-foreground hover:text-primary transition-colors">
                  Home
                </Link>
              </li>
              <li>
                <Link to="/movies" className="text-muted-foreground hover:text-primary transition-colors">
                  Movies
                </Link>
              </li>
              <li>
                <Link to="/rentals" className="text-muted-foreground hover:text-primary transition-colors">
                  My Rentals
                </Link>
              </li>
            </ul>
          </div>

          <div>
            <h3 className="text-lg font-semibold mb-4">Genres</h3>
            <ul className="space-y-2">
              <li>
                <Link to="/movies?genre=action" className="text-muted-foreground hover:text-primary transition-colors">
                  Action
                </Link>
              </li>
              <li>
                <Link to="/movies?genre=comedy" className="text-muted-foreground hover:text-primary transition-colors">
                  Comedy
                </Link>
              </li>
              <li>
                <Link to="/movies?genre=drama" className="text-muted-foreground hover:text-primary transition-colors">
                  Drama
                </Link>
              </li>
              <li>
                <Link to="/movies?genre=sci-fi" className="text-muted-foreground hover:text-primary transition-colors">
                  Sci-Fi
                </Link>
              </li>
            </ul>
          </div>

          <div>
            <h3 className="text-lg font-semibold mb-4">Contact</h3>
            <address className="not-italic text-muted-foreground">
              <p>123 Movie Street</p>
              <p>Hollywood, CA 90210</p>
              <p>Email: info@movierental.com</p>
              <p>Phone: (123) 456-7890</p>
            </address>
          </div>
        </div>

        <div className="mt-8 pt-6 border-t text-center text-muted-foreground">
          <p>&copy; {new Date().getFullYear()} MovieRental. All rights reserved.</p>
        </div>
      </div>
    </footer>
  )
}

