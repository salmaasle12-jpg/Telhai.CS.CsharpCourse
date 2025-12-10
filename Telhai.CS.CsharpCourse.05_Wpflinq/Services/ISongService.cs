using System.Collections.Generic;
using Telhai.CS.CsharpCourse._05_WpfLinq.Models;

namespace Telhai.CS.CsharpCourse._05_WpfLinq.Services
{
    public interface ISongService
    {
        List<Song> GenerateSongs(int count);
    }
}
