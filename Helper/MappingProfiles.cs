using AutoMapper;
using ProjectAPI.Dto;
using ProjectAPI.Models;

namespace ProjectAPI.Helper {
    public class MappingProfiles : Profile {
        public MappingProfiles() {
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();

            CreateMap<Team, TeamDto>();
            CreateMap<TeamDto, Team>();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Client, ClientResponse>();
            CreateMap<Team, TeamResponse>();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
        }
    }
}
