"use client";

import type React from "react";

import { useState } from "react";
import { useAuth } from "../contexts/auth-context";
import { useMovies } from "../contexts/movie-context";
import { Button } from "../components/ui/button";
import { Input } from "../components/ui/input";
import { Label } from "../components/ui/label";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "../components/ui/card";
import {
  Tabs,
  TabsContent,
  TabsList,
  TabsTrigger,
} from "../components/ui/tabs";
import { Avatar, AvatarFallback, AvatarImage } from "../components/ui/avatar";
import { useToast } from "../hooks/use-toast";
import { MovieCard } from "../components/movie-card";

export function ProfilePage() {
  const { user } = useAuth();
  const { getUserRentals } = useMovies();
  const { toast } = useToast();

  const [name, setName] = useState(user?.name || "");
  const [email, setEmail] = useState(user?.email || "");
  const [currentPassword, setCurrentPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const rentals = user ? getUserRentals(user.id) : [];
  const activeRentals = rentals.filter((rental) => !rental.returned);
  const rentalHistory = rentals.filter((rental) => rental.returned);

  const handleUpdateProfile = (e: React.FormEvent) => {
    e.preventDefault();
    toast({
      title: "Profile updated",
      description: "Your profile has been updated successfully",
    });
  };

  const handleChangePassword = (e: React.FormEvent) => {
    e.preventDefault();

    if (newPassword !== confirmPassword) {
      toast({
        title: "Passwords do not match",
        description: "New password and confirmation must match",
        variant: "destructive",
      });
      return;
    }

    // In a real app, you would make an API call to change the password
    toast({
      title: "Password changed",
      description: "Your password has been changed successfully",
    });

    setCurrentPassword("");
    setNewPassword("");
    setConfirmPassword("");
  };

  if (!user) {
    return null; // Should be handled by ProtectedRoute
  }

  return (
    <div className="space-y-8">
      <div className="flex items-center space-x-4">
        <Avatar className="h-16 w-16">
          <AvatarImage src={user.avatar} alt={user.name} />
          <AvatarFallback>{user.name.charAt(0)}</AvatarFallback>
        </Avatar>
        <div>
          <h1 className="text-3xl font-bold">{user.name}</h1>
          <p className="text-muted-foreground">{user.email}</p>
        </div>
      </div>

      <Tabs defaultValue="profile" className="w-full">
        <TabsList className="grid w-full grid-cols-3">
          <TabsTrigger value="profile">Profile</TabsTrigger>
          <TabsTrigger value="rentals">Active Rentals</TabsTrigger>
          <TabsTrigger value="history">Rental History</TabsTrigger>
        </TabsList>

        {/* Profile Tab */}
        <TabsContent value="profile" className="space-y-6">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
            <Card>
              <CardHeader>
                <CardTitle>Personal Information</CardTitle>
                <CardDescription>
                  Update your personal information
                </CardDescription>
              </CardHeader>
              <CardContent>
                <form onSubmit={handleUpdateProfile} className="space-y-4">
                  <div className="space-y-2">
                    <Label htmlFor="name">Full Name</Label>
                    <Input
                      id="name"
                      value={name}
                      onChange={(e) => setName(e.target.value)}
                    />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="email">Email</Label>
                    <Input
                      id="email"
                      type="email"
                      value={email}
                      onChange={(e) => setEmail(e.target.value)}
                    />
                  </div>
                  <Button type="submit">Update Profile</Button>
                </form>
              </CardContent>
            </Card>

            <Card>
              <CardHeader>
                <CardTitle>Change Password</CardTitle>
                <CardDescription>Update your password</CardDescription>
              </CardHeader>
              <CardContent>
                <form onSubmit={handleChangePassword} className="space-y-4">
                  <div className="space-y-2">
                    <Label htmlFor="currentPassword">Current Password</Label>
                    <Input
                      id="currentPassword"
                      type="password"
                      value={currentPassword}
                      onChange={(e) => setCurrentPassword(e.target.value)}
                    />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="newPassword">New Password</Label>
                    <Input
                      id="newPassword"
                      type="password"
                      value={newPassword}
                      onChange={(e) => setNewPassword(e.target.value)}
                    />
                  </div>
                  <div className="space-y-2">
                    <Label htmlFor="confirmPassword">
                      Confirm New Password
                    </Label>
                    <Input
                      id="confirmPassword"
                      type="password"
                      value={confirmPassword}
                      onChange={(e) => setConfirmPassword(e.target.value)}
                    />
                  </div>
                  <Button type="submit">Change Password</Button>
                </form>
              </CardContent>
            </Card>
          </div>
        </TabsContent>

        {/* Active Rentals Tab */}
        <TabsContent value="rentals">
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
              <h2 className="text-2xl font-semibold">Your Active Rentals</h2>
              <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                {activeRentals.map((rental) => (
                  <div key={rental.id} className="space-y-2">
                    <MovieCard movie={rental.movie} />
                    <div className="text-sm text-muted-foreground">
                      <p>
                        Rented:{" "}
                        {new Date(rental.rentalDate).toLocaleDateString()}
                      </p>
                      <p>
                        Due: {new Date(rental.returnDate).toLocaleDateString()}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          )}
        </TabsContent>

        {/* Rental History Tab */}
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
              <h2 className="text-2xl font-semibold">Your Rental History</h2>
              <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
                {rentalHistory.map((rental) => (
                  <div key={rental.id} className="space-y-2">
                    <MovieCard movie={rental.movie} />
                    <div className="text-sm text-muted-foreground">
                      <p>
                        Rented:{" "}
                        {new Date(rental.rentalDate).toLocaleDateString()}
                      </p>
                      <p>
                        Returned:{" "}
                        {new Date(rental.returnDate).toLocaleDateString()}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          )}
        </TabsContent>
      </Tabs>
    </div>
  );
}
