using AutoMapper;
using ProjectAPI.Dto;
using ProjectAPI.Models;
using Task = ProjectAPI.Models.Task;

namespace ProjectAPI.Helper {
    public class MappingProfiles : Profile {
        public MappingProfiles() {
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
            CreateMap<Project, ProjectResponse>();

            CreateMap<Team, TeamDto>();
            CreateMap<Team, TeamResponse>();
            CreateMap<TeamDto, Team>();

            CreateMap<Client, ClientDto>();
            CreateMap<Client, ClientResponse>();
            CreateMap<ClientDto, Client>();
            
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, EmployeeResponse>();
            CreateMap<EmployeeDto, Employee>();
            
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
        }
    }
}
