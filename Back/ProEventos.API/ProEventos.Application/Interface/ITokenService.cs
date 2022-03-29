using ProEventos.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Interface {
    public interface ITokenService {
        Task<string> CreateToken(UserUpdateDto userUptadeDto);
    }
}
