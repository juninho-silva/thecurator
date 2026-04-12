using Application.Common.Enums;
using MediatR;

namespace Application.Commands.Subscribe
{
    public class SubscribeCommand : IRequest<bool>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public List<GenresMovie> GenresMovie { get; private set; }
        public List<GenresTV> GenresTVs { get; private set; }
        public DayOfWeek PrefferenceDay { get; private set; }

        public SubscribeCommand(
            string name, 
            string email, 
            List<GenresMovie> gendersMovie, 
            List<GenresTV> genresTVs, 
            DayOfWeek prefferenceDay)
        {
            Name = name;
            Email = email;
            GenresMovie = gendersMovie;
            GenresTVs = genresTVs;
            PrefferenceDay = prefferenceDay;
        }
    }
}
