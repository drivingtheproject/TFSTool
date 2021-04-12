using AutoMapper;
using ExternalTfsService.Models;

namespace TFSTool.UI.Model.Mappings
{
    public class UserModelMappingProfile : Profile
    {
        public UserModelMappingProfile()
        {
            CreateMap<TfsUser, UserModel>();
        }
    }
}
