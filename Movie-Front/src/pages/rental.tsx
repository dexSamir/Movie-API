"use client";
import { useAuth } from "../contexts/auth-context";
import { useMovies } from "../contexts/movie-context";
import { Button } from "../components/ui/button";
import { Card, CardContent } from "../components/ui/card";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "../components/ui/tabs";
import { useToast } from "../hooks/use-toast";
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

export function RentalsPage() {
  const { user } = useAuth();
  const { getUserRentals, returnMovie } = useMovies();
  const { toast } = useToast();

  const rentals = user ? getUserRentals(user.id) : [];
  const activeRentals = rentals.filter((rental) => !rental.returned);
  const rentalHistory = rentals.filter((rental) => rental.returned);

  const handleReturnMovie = (rentalId: string, movieTitle: string) => {
    returnMovie(rentalId);
    toast({
      title: "Movie returned",
      description: `You have successfully returned "${movieTitle}"`,
    });
  };

  if (!user) {
    return null;
  }

  return (
    <div className="space-y-8">
      <h1 className="text-3xl font-bold">My Rentals</h1>

      <Tabs defaultValue="active" className="w-full">
        <TabsList className="grid w-full grid-cols-2">
          <TabsTrigger value="active">Active Rentals</TabsTrigger>
          <TabsTrigger value="history">Rental History</TabsTrigger>
        </TabsList>

        <TabsContent value="active">
          {activeRentals.length === 0 ? (
            <div className="text-center py-12 border rounded-lg">
              <h3 className="text-xl font-semibold mb-2">No active rentals</h3>
              <p className="text-muted-foreground mb-4">
                You don't have any active movie rentals
              </p>
              <Button asChild>
                <a href="/movies">Browse Movies</a>
              </Button>
            </div>
          ) : (
            <div className="space-y-6">
              <div className="grid grid-cols-1 gap-4">
                {activeRentals.map((rental) => (
                  <Card key={rental.id}>
                    <CardContent className="p-6">
                      <div className="flex flex-col md:flex-row gap-6">
                        <div className="w-full md:w-[150px] flex-shrink-0">
                          <img
                            src={rental.movie.posterUrl || "/placeholder.svg"}
                            alt={rental.movie.title}
                            className="w-full h-auto rounded-lg"
                          />
                        </div>
                        <div className="flex-grow">
                          <div className="flex flex-col md:flex-row justify-between items-start md:items-center mb-4">
                            <h3 className="text-xl font-semibold">
                              {rental.movie.title}
                            </h3>
                            <div className="text-sm text-muted-foreground">
                              Rental Price: $
                              {rental.movie.rentalPrice.toFixed(2)}
                            </div>
                          </div>
                          <div className="grid grid-cols-1 md:grid-cols-2 gap-4 mb-4">
                            <div>
                              <p className="text-sm text-muted-foreground">
                                Rental Date
                              </p>
                              <p>
                                {new Date(
                                  rental.rentalDate
                                ).toLocaleDateString()}
                              </p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">
                                Due Date
                              </p>
                              <p>
                                {new Date(
                                  rental.returnDate
                                ).toLocaleDateString()}
                              </p>
                            </div>
                          </div>
                          <div className="flex justify-end">
                            <AlertDialog>
                              <AlertDialogTrigger asChild>
                                <Button>Return Movie</Button>
                              </AlertDialogTrigger>
                              <AlertDialogContent>
                                <AlertDialogHeader>
                                  <AlertDialogTitle>
                                    Confirm Return
                                  </AlertDialogTitle>
                                  <AlertDialogDescription>
                                    Are you sure you want to return "
                                    {rental.movie.title}"?
                                  </AlertDialogDescription>
                                </AlertDialogHeader>
                                <AlertDialogFooter>
                                  <AlertDialogCancel>Cancel</AlertDialogCancel>
                                  <AlertDialogAction
                                    onClick={() =>
                                      handleReturnMovie(
                                        rental.id,
                                        rental.movie.title
                                      )
                                    }
                                  >
                                    Confirm
                                  </AlertDialogAction>
                                </AlertDialogFooter>
                              </AlertDialogContent>
                            </AlertDialog>
                          </div>
                        </div>
                      </div>
                    </CardContent>
                  </Card>
                ))}
              </div>
            </div>
          )}
        </TabsContent>

        <TabsContent value="history">
          {rentalHistory.length === 0 ? (
            <div className="text-center py-12 border rounded-lg">
              <h3 className="text-xl font-semibold mb-2">No rental history</h3>
              <p className="text-muted-foreground">
                You haven't rented any movies yet
              </p>
            </div>
          ) : (
            <div className="space-y-6">
              <div className="grid grid-cols-1 gap-4">
                {rentalHistory.map((rental) => (
                  <Card key={rental.id}>
                    <CardContent className="p-6">
                      <div className="flex flex-col md:flex-row gap-6">
                        <div className="w-full md:w-[150px] flex-shrink-0">
                          <img
                            src={rental.movie.posterUrl || "/placeholder.svg"}
                            alt={rental.movie.title}
                            className="w-full h-auto rounded-lg"
                          />
                        </div>
                        <div className="flex-grow">
                          <div className="flex flex-col md:flex-row justify-between items-start md:items-center mb-4">
                            <h3 className="text-xl font-semibold">
                              {rental.movie.title}
                            </h3>
                            <div className="text-sm text-muted-foreground">
                              Rental Price: $
                              {rental.movie.rentalPrice.toFixed(2)}
                            </div>
                          </div>
                          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                            <div>
                              <p className="text-sm text-muted-foreground">
                                Rental Date
                              </p>
                              <p>
                                {new Date(
                                  rental.rentalDate
                                ).toLocaleDateString()}
                              </p>
                            </div>
                            <div>
                              <p className="text-sm text-muted-foreground">
                                Return Date
                              </p>
                              <p>
                                {new Date(
                                  rental.returnDate
                                ).toLocaleDateString()}
                              </p>
                            </div>
                          </div>
                        </div>
                      </div>
                    </CardContent>
                  </Card>
                ))}
              </div>
            </div>
          )}
        </TabsContent>
      </Tabs>
    </div>
  );
}
