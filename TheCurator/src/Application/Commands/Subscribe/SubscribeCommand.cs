using Application.Common.Enums;
using MediatR;

namespace Application.Commands.Subscribe
{
    public class SubscribeCommand : IRequest<bool>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<GenresMovie> GenresMovie { get; private set; }
        public List<GenresTV> GenresSeries { get; private set; }
        public DayOfWeek Frequency { get; private set; }

        public SubscribeCommand(
            string name, 
            string email, 
            List<GenresMovie> genresMovie, 
            List<GenresTV> genresSeries, 
            DayOfWeek frequency)
        {
            Name = name;
            Email = email;
            GenresMovie = genresMovie;
            GenresSeries = genresSeries;
            Frequency = frequency;
        }
    }
}
