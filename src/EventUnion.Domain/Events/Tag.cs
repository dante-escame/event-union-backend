// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Local

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace EventUnion.Domain.Events;

public class Tag
{
    [NotMapped] public const int NameMaxLength = 50;
    
    public int TagId { get; private set; }
    public string Name { get; private set; }

    public Tag(int tagId, string name)
    {
        TagId = tagId;
        Name = name;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public enum TagEnum
    {
        Programming = 1,
        Music,
        Art,
        Sports,
        Technology,
        Education,
        Health,
        Business,
        Travel,
        Food,
        Science,
        Environment,
        Fashion,
        Literature,
        Gaming,
        Film,
        Photography,
        History,
        Charity,
        Networking,
        Workshops,
        Conferences,
        Webinars,
        Meetups,
        Festivals,
        Competitions
    }
    
    public static readonly Tag Programming = new((int)TagEnum.Programming, "Programação");
    public static readonly Tag Music = new((int)TagEnum.Music, "Música");
    public static readonly Tag Art = new((int)TagEnum.Art, "Arte");
    public static readonly Tag Sports = new((int)TagEnum.Sports, "Esportes");
    public static readonly Tag Technology = new((int)TagEnum.Technology, "Tecnologia");
    public static readonly Tag Education = new((int)TagEnum.Education, "Educação");
    public static readonly Tag Health = new((int)TagEnum.Health, "Saúde");
    public static readonly Tag Business = new((int)TagEnum.Business, "Negócios");
    public static readonly Tag Travel = new((int)TagEnum.Travel, "Viagem");
    public static readonly Tag Food = new((int)TagEnum.Food, "Culinária");
    public static readonly Tag Science = new((int)TagEnum.Science, "Ciência");
    public static readonly Tag Environment = new((int)TagEnum.Environment, "Meio Ambiente");
    public static readonly Tag Fashion = new((int)TagEnum.Fashion, "Moda");
    public static readonly Tag Literature = new((int)TagEnum.Literature, "Literatura");
    public static readonly Tag Gaming = new((int)TagEnum.Gaming, "Jogos");
    public static readonly Tag Film = new((int)TagEnum.Film, "Cinema");
    public static readonly Tag Photography = new((int)TagEnum.Photography, "Fotografia");
    public static readonly Tag History = new((int)TagEnum.History, "História");
    public static readonly Tag Charity = new((int)TagEnum.Charity, "Caridade");
    public static readonly Tag Networking = new((int)TagEnum.Networking, "Networking");
    public static readonly Tag Workshops = new((int)TagEnum.Workshops, "Workshops");
    public static readonly Tag Conferences = new((int)TagEnum.Conferences, "Conferências");
    public static readonly Tag Webinars = new((int)TagEnum.Webinars, "Webinars");
    public static readonly Tag Meetups = new((int)TagEnum.Meetups, "Meetups");
    public static readonly Tag Festivals = new((int)TagEnum.Festivals, "Festivais");
    public static readonly Tag Competitions = new((int)TagEnum.Competitions, "Competições");

    public static readonly IReadOnlyList<Tag> All = new List<Tag>
    {
        Programming,
        Music,
        Art,
        Sports,
        Technology,
        Education,
        Health,
        Business,
        Travel,
        Food,
        Science,
        Environment,
        Fashion,
        Literature,
        Gaming,
        Film,
        Photography,
        History,
        Charity,
        Networking,
        Workshops,
        Conferences,
        Webinars,
        Meetups,
        Festivals,
        Competitions
    };
    
    [ExcludeFromCodeCoverage]
    private Tag() { }
}