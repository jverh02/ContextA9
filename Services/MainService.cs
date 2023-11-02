using ContextExample.Data;
using System;
using System.ComponentModel;

namespace ContextExample.Services;

/// <summary>
///     You would need to inject your interfaces here to execute the methods in Invoke()
///     See the commented out code as an example
/// </summary>
public class MainService : IMainService
{
    private readonly IContext _context;

    public MainService(IContext context)
    {
        _context = context;
    }

    public void Invoke()
    {
        // provide an option to the user to 
        // 1. select by id
        // 2. select by title 
        // 3. find movie by title
        Console.WriteLine("1. Select by ID\n2. Select by Title\n3. Search movies by Title");
        Console.Write("What would you like to do?\n>");
        var input = Console.ReadLine();
        switch (input) {
            case "1":
                Console.Write("Please input the movie's ID.\n>");
                var isValid = Int32.TryParse(Console.ReadLine(), out int userID);
                if (isValid)
                {
                    var movie = _context.GetById(userID);

                    Console.WriteLine($"Your movie is {movie.Title}");
                }
                else
                {
                    Console.WriteLine("ERROR: ID must be a number.");
                }
                break;
            case "2":
                Console.Write("Please input the movie's name.\n>");
                var movieTitle = _context.GetByTitle(Console.ReadLine());
                if (movieTitle == null)
                {
                    Console.Write("There are no movies by that title. Make sure you entered the full name correctly."); 
                    break;
                }
                Console.WriteLine($"{movieTitle.Title} successfully found.");
                break;
            case "3":
                Console.Write("Please input the search term.\n>");
                var search = Console.ReadLine();
                var movies = _context.FindMovie(search);
                foreach(var mov in movies)
                {
                    Console.WriteLine(mov.Title);
                }
                break;
            default:
                break;
        }
       
    }
}
