using AutoMapper;
using BookStoreApp.Entities;

namespace BookStoreApp;

public class ApplicationMapper : Profile
{
    public ApplicationMapper()
    {
        // This map Books to BookModel and vice versa
        CreateMap<Books,BookModel>().ReverseMap();

        // Another way
        //CreateMap<Books,BookModel>();
        //CreateMap<BookModel,Books>();
    }
}
