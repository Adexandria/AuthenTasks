using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList_API.Entity;
using TodoList_API.ViewModel;

namespace TodoList_API.Profiles
{
    public class ListProfile :Profile
    {
        public ListProfile()
        {
            CreateMap<ListsCreate, TodoList>()
                .ForMember(s => s.Name, opt => opt.MapFrom(dest => dest.Name))
                .ForMember(s => s.LongNote, opt => opt.MapFrom(dest => dest.LongNote))
                .ForMember(s => s.DueDate, opt => opt.MapFrom(dest => dest.DueDate))
                .ForMember(s => s.TasksUpdate, opt => opt.MapFrom(dest => dest.TasksUpdate));
            CreateMap<ListUpdate, TodoList>()
              .ForMember(s => s.Name, opt => opt.MapFrom(dest => dest.Name))
              .ForMember(s => s.LongNote, opt => opt.MapFrom(dest => dest.LongNote))
              .ForMember(s => s.DueDate, opt => opt.MapFrom(dest =>dest.DueDate))
              .ForMember(s => s.TasksUpdate, opt => opt.MapFrom(dest => dest.TasksUpdate));

            CreateMap<TodoList,ListsView>()
                .ForMember(s => s.Id, opt => opt.MapFrom(dest => dest.Id))
               .ForMember(s => s.Name, opt => opt.MapFrom(dest => dest.Name))
               .ForMember(s => s.LongNote, opt => opt.MapFrom(dest => dest.LongNote))
               .ForMember(s => s.DueDate, opt => opt.MapFrom(dest => dest.DueDate))
               .ForMember(s => s.TasksUpdate, opt => opt.MapFrom(dest => dest.TasksUpdate.ToString()));
           
            CreateMap<TodoList, ListView>()
             .ForMember(s => s.Name, opt => opt.MapFrom(dest => dest.Name))
             .ForMember(s => s.LongNote, opt => opt.MapFrom(dest => dest.LongNote))
             .ForMember(s => s.DueDate, opt => opt.MapFrom(dest => dest.DueDate))
             .ForMember(s => s.TasksUpdate, opt => opt.MapFrom(dest => dest.TasksUpdate.ToString()));

        }
    }
}
